using System;
using Microsoft.Xrm.Sdk.Async;

namespace Microsoft.Xrm.Sdk.Client.Async
{
    public static class OrganizationServiceProxyExtensions
    {
        private static readonly Type OrganizationServiceConfigurationType = typeof(OrganizationServiceProxy).Assembly.GetType("Microsoft.Xrm.Sdk.Client.OrganizationServiceConfiguration");
        private static readonly PropertyAccessor<Uri> _primaryEndpoint;

        static OrganizationServiceProxyExtensions()
        {
            _primaryEndpoint = ReflectionUtils.CreatePropertyAccessor<Uri>(OrganizationServiceConfigurationType, "PrimaryEndpoint");
        }


        public static AsyncOrganizationServiceProxy ToAsyncService(this OrganizationServiceProxy service)
        {
            if (!OrganizationServiceConfigurationType.IsAssignableFrom(service.ServiceConfiguration.GetType()))
                throw new InvalidOperationException($"Cannot create {nameof(AsyncOrganizationServiceProxy)} from {nameof(OrganizationServiceProxy)}, because a customer service configuration is used. Expected service configuration type is '{OrganizationServiceConfigurationType}', but got '{service.ServiceConfiguration.GetType()}'");

            var primaryEndpoint = _primaryEndpoint.Get(service.ServiceConfiguration);
            var asyncService = new AsyncOrganizationServiceProxy(primaryEndpoint, service.HomeRealmUri, service.ClientCredentials, service.DeviceCredentials);
            asyncService.Timeout = service.Timeout;
            asyncService.UserPrincipalName = service.UserPrincipalName;
            asyncService.EndpointAutoSwitchEnabled = service.EndpointAutoSwitchEnabled;
            asyncService.CallerId = service.CallerId;
            asyncService.SyncOperationType = service.SyncOperationType;
            asyncService.SdkClientVersion = service.SdkClientVersion;
#if SDK8AtLeast
            asyncService.UserType = service.UserType;
            asyncService.CallerRegardingObjectId = service.CallerRegardingObjectId;
#endif
            foreach (var behavior in service.ServiceConfiguration.CurrentServiceEndpoint.Behaviors)
                asyncService.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(behavior);
            return asyncService;
        }
    }
}
