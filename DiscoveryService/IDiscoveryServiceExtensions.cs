using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Client;
#if SDK7AtLeast
using Microsoft.Xrm.Sdk.WebServiceClient;
using Microsoft.Xrm.Sdk.WebServiceClient.Async;
#endif

namespace Microsoft.Xrm.Sdk.Discovery.Async
{
    public static class IDiscoveryServiceExtensions
    {
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
