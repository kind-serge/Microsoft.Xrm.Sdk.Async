using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Microsoft.Xrm.Sdk.Async;

namespace Microsoft.Xrm.Sdk.Client.Async
{
    internal sealed class OrganizationServiceContextInitializer : ServiceContextInitializer<IWcfAsyncOrganizationService>
    {
        private AsyncOrganizationServiceProxy OrganizationServiceProxy
        {
            get
            {
                return base.ServiceProxy as AsyncOrganizationServiceProxy;
            }
        }

        public OrganizationServiceContextInitializer(AsyncOrganizationServiceProxy proxy) : base(proxy)
        {
            this.Initialize();
        }

        private void Initialize()
        {
            var proxy = this.OrganizationServiceProxy;
            if (proxy != null)
            {
                var context = OperationContext.Current;
                var headers = context.OutgoingMessageHeaders;

                if (proxy.OfflinePlayback)
                {
                    headers.Add(MessageHeader.CreateHeader("IsOfflinePlayback", "http://schemas.microsoft.com/xrm/2011/Contracts", true));
                }
                if (proxy.CallerId != Guid.Empty)
                {
                    headers.Add(MessageHeader.CreateHeader("CallerId", "http://schemas.microsoft.com/xrm/2011/Contracts", proxy.CallerId));
                }
#if SDK8AtLeast
                if (proxy.CallerRegardingObjectId != Guid.Empty)
                {
                    headers.Add(MessageHeader.CreateHeader("CallerRegardingObjectId", "http://schemas.microsoft.com/xrm/2011/Contracts", proxy.CallerRegardingObjectId));
                }
#endif
                if (proxy.LanguageCodeOverride != 0)
                {
                    headers.Add(MessageHeader.CreateHeader("LanguageCodeOverride", "http://schemas.microsoft.com/xrm/2011/Contracts", proxy.LanguageCodeOverride));
                }
                if (proxy.SyncOperationType != null)
                {
                    headers.Add(MessageHeader.CreateHeader("OutlookSyncOperationType", "http://schemas.microsoft.com/xrm/2011/Contracts", proxy.SyncOperationType));
                }
                if (!string.IsNullOrEmpty(proxy.ClientAppName))
                {
                    headers.Add(MessageHeader.CreateHeader("ClientAppName", "http://schemas.microsoft.com/xrm/2011/Contracts", proxy.ClientAppName));
                }
                if (!string.IsNullOrEmpty(proxy.ClientAppVersion))
                {
                    headers.Add(MessageHeader.CreateHeader("ClientAppVersion", "http://schemas.microsoft.com/xrm/2011/Contracts", proxy.ClientAppVersion));
                }
                if (!string.IsNullOrEmpty(proxy.SdkClientVersion))
                {
                    headers.Add(MessageHeader.CreateHeader("SdkClientVersion", "http://schemas.microsoft.com/xrm/2011/Contracts", proxy.SdkClientVersion));
#if SDK8AtLeast
                }
                else
                {
#else
                    return;
                }
#endif
                string xrmSdkAssemblyFileVersion = proxy.GetXrmSdkAssemblyFileVersion();
                if (!string.IsNullOrEmpty(xrmSdkAssemblyFileVersion))
                {
                    headers.Add(MessageHeader.CreateHeader("SdkClientVersion", "http://schemas.microsoft.com/xrm/2011/Contracts", xrmSdkAssemblyFileVersion));
                }
#if SDK8AtLeast
                }
                headers.Add(MessageHeader.CreateHeader("UserType", "http://schemas.microsoft.com/xrm/2011/Contracts", proxy.UserType));
#endif
            }
        }
    }
}
