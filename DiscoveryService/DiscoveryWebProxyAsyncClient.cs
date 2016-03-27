using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Async;
using Microsoft.Xrm.Sdk.Client.Async;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Xrm.Sdk.Discovery.Async;

namespace Microsoft.Xrm.Sdk.WebServiceClient.Async
{
    public class DiscoveryWebProxyAsyncClient : WebProxyClient<IWcfAsyncDiscoveryService>, IWcfAsyncDiscoveryService, IAsyncDiscoveryService
    {
        public DiscoveryWebProxyAsyncClient(Uri serviceUrl) : base(serviceUrl, ServiceDefaults.DefaultTimeout, false)
        {
        }

        public DiscoveryWebProxyAsyncClient(Uri serviceUrl, TimeSpan timeout) : base(serviceUrl, timeout, false)
        {
        }

        protected override WebProxyClientContextInitializer<IWcfAsyncDiscoveryService> CreateNewInitializer()
        {
            return new DiscoveryWebProxyClientContextInitializer(this);
        }

        private TResult ExecuteAction<TResult>(Func<TResult> action)
        {
            if (action == null) {
                throw new ArgumentNullException("action");
            }
            TResult result;
            using (this.CreateNewInitializer()) {
                result = action();
            }
            return result;
        }

        private T TryRun<T>(Func<T> func, ref bool? retry)
        {
            return func();
        }

        protected internal virtual DiscoveryResponse ExecuteCore(DiscoveryRequest request)
        {
            return ExecuteAction<DiscoveryResponse>(() => this.Channel.Execute(request));
        }

        public DiscoveryResponse Execute(DiscoveryRequest request)
        {
            return this.ExecuteCore(request);
        }

        public IAsyncResult BeginExecute(DiscoveryRequest request, AsyncCallback callback, object asyncState)
            => BeginExecute(request, callback, asyncState, CancellationToken.None);

        public IAsyncResult BeginExecute(DiscoveryRequest request, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<DiscoveryResponse>.Begin("Execute", callback, asyncState, cancellationToken,
                createContext: () => new DiscoveryWebProxyClientContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.Channel.BeginExecute(request, _callback, _state),
                endOperation: (_asynResult) => base.Channel.EndExecute(_asynResult),
                cancelOperation: (_asyncResult) => base.Abort());

        public DiscoveryResponse EndExecute(IAsyncResult result)
            => ((AsyncCallOperation<DiscoveryResponse>)result).GetResult();

        public Task<DiscoveryResponse> ExecuteAsync(DiscoveryRequest request, CancellationToken cancellationToken)
            => Task.Run(async () => await Task<DiscoveryResponse>.Factory.FromAsync((callback, state) => BeginExecute(request, callback, state, cancellationToken), EndExecute, state: null));
    }
}
