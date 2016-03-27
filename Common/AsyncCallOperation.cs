using System;
using System.Threading;
using Microsoft.Xrm.Sdk.Client.Async;

namespace Microsoft.Xrm.Sdk.Async
{
    internal delegate T TryRunFunc<T>(Func<T> func, ref bool? retry);

    /// <summary>
    /// Allows to do retry on failures and to cancel an operation
    /// </summary>
    /// <typeparam name="T">Result type of the operation</typeparam>
    internal sealed class AsyncCallOperation<T> : IAsyncResult
    {
        internal static AsyncCallOperation<T> Begin(
            string operationName,
            AsyncCallback callback,
            object asyncState,
            CancellationToken cancellationToken,
            Func<IDisposable> createContext,
            TryRunFunc<IAsyncResult> tryBegin,
            TryRunFunc<T> tryEnd,
            Func<AsyncCallback, object, IAsyncResult> beginOperation,
            Func<IAsyncResult, T> endOperation,
            Action<IAsyncResult> cancelOperation)
        {
            var instance = new AsyncCallOperation<T>() {
                _operationName = operationName,
                _createContext = createContext,
                _tryBegin = tryBegin,
                _tryEnd = tryEnd,
                _beginOperation = beginOperation,
                _endOperation = endOperation,
                _cancelOperation = cancelOperation,
                _cancellationToken = cancellationToken,
                AsyncState = asyncState,
                _callback = callback,
            };

            instance.RegisterForCancellation();
            instance.StartOperation();

            return instance;
        }

        internal T GetResult()
        {
            if (_error != null)
                throw _error;
            return _result;
        }

        private int _state;
        private string _operationName;
        private ManualResetEvent _waitHandle;
        private CancellationToken _cancellationToken;
        private CancellationTokenRegistration _ctRegistration;
        private Func<IDisposable> _createContext;
        private TryRunFunc<IAsyncResult> _tryBegin;
        private TryRunFunc<T> _tryEnd;
        private Func<AsyncCallback, object, IAsyncResult> _beginOperation;
        private Func<IAsyncResult, T> _endOperation;
        private Action<IAsyncResult> _cancelOperation;
        private IAsyncResult _rpcHandle;
        private bool? _retry;
        private Exception _error;
        private T _result;
        private AsyncCallback _callback;

        private AsyncCallOperation() { }

        #region IAsyncResult

        public object AsyncState
        {
            get;
            set;
        }

        public WaitHandle AsyncWaitHandle
        {
            get
            {
                if (_waitHandle == null)
                    CreateWaitHandle();
                return _waitHandle;
            }
        }

        public bool CompletedSynchronously
        {
            get
            {
                if (!IsCompleted || _rpcHandle == null)
                    return false;
                return _rpcHandle.CompletedSynchronously;
            }
        }

        public bool IsCompleted => _state == State_Completed;

        #endregion

        private void RegisterForCancellation()
        {
            if (_cancellationToken.CanBeCanceled) {
                _cancellationToken.ThrowIfCancellationRequested();
                _ctRegistration = _cancellationToken.Register(OnCancelRequested, useSynchronizationContext: false);
            }
        }

        private bool StartOperation(bool throwOnError = true)
        {
            while (true) {
                try {
                    _rpcHandle = _tryBegin(
                        () => {
                            using (_createContext())
                                return _beginOperation(OnCompleted, AsyncState);
                        },
                        ref _retry);

                    return true;

                } catch (Exception ex) {

                    if (_retry == true) {
                        continue;
                    } else {
                        if (throwOnError && TrySetFailed(ex))
                            throw;
                        else
                            return false;
                    }
                }
            }
        }

        private void CreateWaitHandle()
        {
            bool changeStateBackToRunning = false;

            while (true) {

                if (TryChangeState(State_CreatingWaitHandle, State_Running)) {
                    changeStateBackToRunning = true;
                    break;
                }

                if (IsCompleted)
                    break;

                Thread.Yield();
            }

            if (_waitHandle == null)
                _waitHandle = new ManualResetEvent(IsCompleted);

            if (changeStateBackToRunning)
                ChangeState(State_Running);
        }

        private void OnCompleted(IAsyncResult _this)
        {
            try {
                var result = _tryEnd(() => _endOperation(_rpcHandle), ref _retry);
                TrySetResult(result);
            } catch (Exception ex) {
                while (_retry == true && !IsCompleting && !IsCompleted) {
                    if (StartOperation(throwOnError: false))
                        return;
                }

                TrySetFailed(ex);
            }
        }

        private void OnCancelRequested()
        {
            TryCancel();
        }

        private bool TrySetResult(T result)
        {
            if (AcquireCompletingState()) {
                _result = result;
                SetCompletedState();
                return true;
            }

            return false;
        }

        private bool TryCancel()
        {
            if (AcquireCompletingState()) {

                try {
                    _cancelOperation(_rpcHandle);
                } catch {
                }

                _error = new OperationCanceledException($"The CRM call '{_operationName}' has been canceled", _cancellationToken);
                SetCompletedState();
                return true;
            }

            return false;
        }

        private bool TrySetFailed(Exception error)
        {
            if (AcquireCompletingState()) {
                _error = error;
                SetCompletedState();
                return true;
            }

            return false;
        }

        #region State Management

        private const int State_Running = 0;
        private const int State_CreatingWaitHandle = 1;
        private const int State_Completing = 2;
        private const int State_Completed = 3;

        private bool IsCompleting => _state == State_Completing;

        private bool TryChangeState(int newState, int expectedState)
        {
            int lastState = Interlocked.CompareExchange(ref _state, newState, expectedState);
            return lastState == expectedState;
        }

        private int ChangeState(int newState)
        {
            return Interlocked.Exchange(ref _state, newState);
        }

        private bool AcquireCompletingState()
        {
            while (true) {

                if (TryChangeState(State_Completing, State_Running))
                    return true;

                if (IsCompleting || IsCompleted)
                    return false;

                Thread.Yield();
            }
        }

        private void SetCompletedState()
        {
            _ctRegistration.Dispose();
            _ctRegistration = default(CancellationTokenRegistration);

            ChangeState(State_Completed);

            if (_waitHandle != null)
                _waitHandle.Set();

            if (_callback != null)
                _callback(this);
        }

        #endregion
    }
}
