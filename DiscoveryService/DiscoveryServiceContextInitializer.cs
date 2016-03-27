using Microsoft.Xrm.Sdk.Discovery.Async;

namespace Microsoft.Xrm.Sdk.Client.Async
{
    internal sealed class DiscoveryServiceContextInitializer : ServiceContextInitializer<IWcfAsyncDiscoveryService>
    {
        public DiscoveryServiceContextInitializer(AsyncDiscoveryServiceProxy proxy) : base(proxy)
        {
        }
    }
}
