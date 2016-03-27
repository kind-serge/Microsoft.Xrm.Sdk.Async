using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.Xrm.Sdk.Discovery.Async
{
    public interface IAsyncDiscoveryService
    {
        Task<DiscoveryResponse> ExecuteAsync(DiscoveryRequest request, CancellationToken cancellationToken = default(CancellationToken));
    }
}
