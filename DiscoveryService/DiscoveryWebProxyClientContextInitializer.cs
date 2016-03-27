using Microsoft.Xrm.Sdk.Discovery.Async;

namespace Microsoft.Xrm.Sdk.WebServiceClient.Async
{
    internal sealed class DiscoveryWebProxyClientContextInitializer : WebProxyClientContextInitializer<IWcfAsyncDiscoveryService>
    {
        public DiscoveryWebProxyClientContextInitializer(DiscoveryWebProxyAsyncClient proxy) : base(proxy)
        {
            this.Initialize();
        }

        private void Initialize()
        {
            if (base.ServiceProxy == null) {
                return;
            }
            base.AddTokenToHeaders();
            base.AddCommonHeaders();
        }
    }
}
