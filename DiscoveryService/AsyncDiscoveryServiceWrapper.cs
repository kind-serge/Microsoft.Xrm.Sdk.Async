using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Xrm.Sdk.Discovery.Async
{
    internal sealed class AsyncDiscoveryServiceWrapper : IDiscoveryService, IAsyncDiscoveryService
    {
        private IDiscoveryService _service;

        public AsyncDiscoveryServiceWrapper(IDiscoveryService service)
        {
            _service = service;
        }

        public DiscoveryResponse Execute(DiscoveryRequest request)
            => _service.Execute(request);

        public Task<DiscoveryResponse> ExecuteAsync(DiscoveryRequest request, CancellationToken cancellationToken = default(CancellationToken))
            => _service.ExecuteAsync(request, cancellationToken);
    }
}
