namespace Microsoft.Xrm.Sdk.WebServiceClient.Async
{
    public static class DiscoveryWebProxyClientExtensions
    {
        public static DiscoveryWebProxyAsyncClient ToAsyncClient(this DiscoveryWebProxyClient client)
        {
            var asyncClient = new DiscoveryWebProxyAsyncClient(client.Endpoint.Address.Uri, client.Endpoint.Binding.SendTimeout);
            asyncClient.HeaderToken = client.HeaderToken;
            asyncClient.SdkClientVersion = client.SdkClientVersion;
            return asyncClient;
        }
    }
}
