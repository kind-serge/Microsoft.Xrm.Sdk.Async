using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Client;
#if SDK7AtLeast
using Microsoft.Xrm.Sdk.WebServiceClient;
using Microsoft.Xrm.Sdk.WebServiceClient.Async;
#endif

namespace Microsoft.Xrm.Sdk.Discovery.Async
{
    /// <summary>
    /// Extension methods for IDiscoveryService interface
    /// </summary>
    public static class IDiscoveryServiceExtensions
    {
        /// <summary>
        /// Creates an asynchronous version of the service client (if possible)
        /// </summary>
        /// <param name="service">The synchronous service</param>
        public static IAsyncDiscoveryService ToAsyncService(this IDiscoveryService service)
        {
            var asyncService = service as IAsyncDiscoveryService;
            if (asyncService != null)
                return asyncService;

            var proxy = service as DiscoveryServiceProxy;
            if (proxy != null)
                return proxy.ToAsyncService();

#if SDK7AtLeast

            var webClient = service as DiscoveryWebProxyClient;
            if (webClient != null)
                return webClient.ToAsyncClient();

#endif
            return new AsyncDiscoveryServiceWrapper(service);
        }

        /// <summary>Executes a discovery service message in the form of a request, and returns a response. </summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryResponse"></see>. The response from processing the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryRequest"></see>. A request instance that defines the action to be performed.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="service">The service instance</param>
        public static Task<DiscoveryResponse> ExecuteAsync(this IDiscoveryService service, DiscoveryRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var asyncService = service as IAsyncDiscoveryService;
            if (asyncService != null)
                return asyncService.ExecuteAsync(request, cancellationToken);

            var wcfAsyncService = service as IWcfAsyncDiscoveryService;
            if (wcfAsyncService != null)
                return Task.Run(async () => await Task<DiscoveryResponse>.Factory.FromAsync(wcfAsyncService.BeginExecute, wcfAsyncService.EndExecute, request, state: null));

            return Task.Run(() => service.Execute(request));
        }
    }
}
