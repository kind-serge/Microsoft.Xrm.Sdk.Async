using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Xrm.Sdk.Async;

namespace Microsoft.Xrm.Sdk.WebServiceClient.Async
{
    internal sealed class OrganizationWebProxyClientContextInitializer : WebProxyClientContextInitializer<IWcfAsyncOrganizationService>
    {
        public OrganizationWebProxyClientContextInitializer(OrganizationWebProxyAsyncClient proxy) : base(proxy)
        {
            this.Initialize();
        }

        private void Initialize()
        {
            var baseProxy = base.ServiceProxy;
            if (baseProxy == null)
                return;

            base.AddTokenToHeaders();

            var client = baseProxy as OrganizationWebProxyAsyncClient;

            if (baseProxy != null) {

                var headers = OperationContext.Current.OutgoingMessageHeaders;

                if (client.OfflinePlayback) {
                    headers.Add(MessageHeader.CreateHeader("IsOfflinePlayback", "http://schemas.microsoft.com/xrm/2011/Contracts", true));
                }
                if (client.CallerId != Guid.Empty) {
                    headers.Add(MessageHeader.CreateHeader("CallerId", "http://schemas.microsoft.com/xrm/2011/Contracts", client.CallerId));
                }
#if SDK8AtLeast
                if (client.CallerRegardingObjectId != Guid.Empty) {
                    headers.Add(MessageHeader.CreateHeader("CallerRegardingObjectId", "http://schemas.microsoft.com/xrm/2011/Contracts", client.CallerRegardingObjectId));
                }
#endif
                if (client.LanguageCodeOverride != 0) {
                    headers.Add(MessageHeader.CreateHeader("LanguageCodeOverride", "http://schemas.microsoft.com/xrm/2011/Contracts", client.LanguageCodeOverride));
                }
                if (client.SyncOperationType != null) {
                    headers.Add(MessageHeader.CreateHeader("OutlookSyncOperationType", "http://schemas.microsoft.com/xrm/2011/Contracts", client.SyncOperationType));
                }
#if SDK8AtLeast
                headers.Add(MessageHeader.CreateHeader("UserType", "http://schemas.microsoft.com/xrm/2011/Contracts", client.userType));
#endif
                base.AddCommonHeaders();
            }
        }
    }
}
