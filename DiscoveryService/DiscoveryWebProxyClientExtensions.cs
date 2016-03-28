namespace Microsoft.Xrm.Sdk.WebServiceClient.Async
{
    /// <summary>
    /// Extension methods for DiscoveryWebProxyClient class
    /// </summary>
    public static class DiscoveryWebProxyClientExtensions
    {
        /// <summary>
        /// Creates the async version of the client
        /// </summary>
        /// <param name="client">Synchronous service client</param>
        public static DiscoveryWebProxyAsyncClient ToAsyncClient(this DiscoveryWebProxyClient client)
        {
            var asyncClient = new DiscoveryWebProxyAsyncClient(client.Endpoint.Address.Uri, client.Endpoint.Binding.SendTimeout);
            asyncClient.HeaderToken = client.HeaderToken;
            asyncClient.SdkClientVersion = client.SdkClientVersion;
            return asyncClient;
        }
    }
}
