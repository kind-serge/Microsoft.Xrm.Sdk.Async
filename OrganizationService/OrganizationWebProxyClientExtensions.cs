using Microsoft.Xrm.Sdk.Client;

namespace Microsoft.Xrm.Sdk.WebServiceClient.Async
{
    /// <summary>
    /// Extension methods for OrganizationWebProxyClient class
    /// </summary>
    public static class OrganizationWebProxyClientExtensions
    {
        /// <summary>
        /// Creates asynchronous proxy (if possible)
        /// </summary>
        /// <param name="client">The synchronous proxy</param>
        public static OrganizationWebProxyAsyncClient ToAsyncClient(this OrganizationWebProxyClient client)
        {
            var asyncClient = new OrganizationWebProxyAsyncClient(client.Endpoint.Address.Uri, client.Endpoint.Binding.SendTimeout, useStrongTypes: false);
            var proxyTypesBehavior = client.Endpoint.Behaviors.Find<ProxyTypesBehavior>();
            if (proxyTypesBehavior != null)
                asyncClient.Endpoint.Behaviors.Add(proxyTypesBehavior);
            asyncClient.HeaderToken = client.HeaderToken;
            asyncClient.SdkClientVersion = client.SdkClientVersion;
            asyncClient.SyncOperationType = client.SyncOperationType;
            asyncClient.CallerId = client.CallerId;
#if SDK8AtLeast
            asyncClient.userType = client.userType;
            asyncClient.CallerRegardingObjectId = client.CallerRegardingObjectId;
#endif
            return asyncClient;
        }
    }
}
