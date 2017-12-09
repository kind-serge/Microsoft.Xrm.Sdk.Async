using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Async;
using Microsoft.Xrm.Sdk.Client.Async;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Xrm.Sdk.Discovery.Async;

namespace Microsoft.Xrm.Sdk.WebServiceClient.Async
{
    /// <summary>Implements <see cref="T:Microsoft.Xrm.Sdk.Discovery.IDiscoveryService" /> and <see cref="T:Microsoft.Xrm.Sdk.Discovery.Async.IAsyncDiscoveryService" /> and provides an authenticated connection to the Discovery.svc/web endpoint. This /web endpoint is also used by web resources.</summary>
    public class DiscoveryWebProxyAsyncClient : WebProxyClient<IWcfAsyncDiscoveryService>, IWcfAsyncDiscoveryService, IAsyncDiscoveryService
    {
        /// <summary>Constructor</summary>
        /// <param name="serviceUrl">The URL of the Discovery web service.</param>
        public DiscoveryWebProxyAsyncClient(Uri serviceUrl) : base(serviceUrl, ServiceDefaults.DefaultTimeout, false)
        {
        }

        /// <summary>constructor</summary>
        /// <param name="serviceUrl">The URL of the Discovery web service.</param>
        /// <param name="timeout">The maximum amount of time a single channel operation has to complete before a timeout fault is raised on a service channel binding.</param>
        public DiscoveryWebProxyAsyncClient(Uri serviceUrl, TimeSpan timeout) : base(serviceUrl, timeout, false)
        {
        }

        /// <summary>Creates the WCF proxy client initializer which gets invoked on every SDK method call.  This method makes sure that the access token and other header values are added to the outbound call.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.WebServiceClient.WebProxyClientContextInitializer`1"></see>A web proxy client context initializer.</returns>
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

        /// <summary>Executes a discovery service message in the form of a request, and returns a response. </summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryResponse"></see>. The response from processing the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryRequest"></see>. A request instance that defines the action to be performed.</param>
        protected internal virtual DiscoveryResponse ExecuteCore(DiscoveryRequest request)
        {
            return ExecuteAction<DiscoveryResponse>(() => this.Channel.Execute(request));
        }

        /// <summary>Executes a discovery service message in the form of a request, and returns a response. </summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryResponse"></see>. The response from processing the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryRequest"></see>. A request instance that defines the action to be performed.</param>
        public DiscoveryResponse Execute(DiscoveryRequest request)
        {
            return this.ExecuteCore(request);
        }

        /// <summary>Executes a discovery service message in the form of a request, and returns a response. </summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryResponse"></see>. The response from processing the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryRequest"></see>. A request instance that defines the action to be performed.</param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        public IAsyncResult BeginExecute(DiscoveryRequest request, AsyncCallback callback, object asyncState)
            => BeginExecute(request, callback, asyncState, CancellationToken.None);

        internal IAsyncResult BeginExecute(DiscoveryRequest request, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<DiscoveryResponse>.Begin("Execute", callback, asyncState, cancellationToken,
                createContext: () => new DiscoveryWebProxyClientContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.Channel.BeginExecute(request, _callback, _state),
                endOperation: (_asynResult) => base.Channel.EndExecute(_asynResult),
                cancelOperation: (_asyncResult) => base.Abort());

        /// <summary>See IDiscoveryService.<see cref="M:Microsoft.Xrm.Sdk.Discovery.IDiscoveryService.Execute(Microsoft.Xrm.Sdk.Discovery.DiscoveryRequest)"></see></summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryResponse"></see>The response from executing the discovery service request.</returns>
        public DiscoveryResponse EndExecute(IAsyncResult result)
            => ((AsyncCallOperation<DiscoveryResponse>)result).GetResult();

        /// <summary>Executes a discovery service message in the form of a request, and returns a response. </summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryResponse"></see>. The response from processing the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryRequest"></see>. A request instance that defines the action to be performed.</param>
        /// <param name="cancellationToken"></param>
        public Task<DiscoveryResponse> ExecuteAsync(DiscoveryRequest request, CancellationToken cancellationToken)
            => Task<DiscoveryResponse>.Factory.FromAsync((callback, state) => BeginExecute(request, callback, state, cancellationToken), EndExecute, state: null);
    }
}
