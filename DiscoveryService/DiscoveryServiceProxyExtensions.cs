using System;
using Microsoft.Xrm.Sdk.Async;

namespace Microsoft.Xrm.Sdk.Client.Async
{
    /// <summary>
    /// Extension methods for DiscoveryServiceProxy class
    /// </summary>
    public static class DiscoveryServiceProxyExtensions
    {
        private static readonly Type DiscoveryServiceConfigurationType = typeof(DiscoveryServiceProxy).Assembly.GetType("Microsoft.Xrm.Sdk.Client.DiscoveryServiceConfiguration");
        private static readonly PropertyAccessor<Uri> _primaryEndpoint;

        static DiscoveryServiceProxyExtensions()
        {
            _primaryEndpoint = ReflectionUtils.CreatePropertyAccessor<Uri>(DiscoveryServiceConfigurationType, "PrimaryEndpoint");
        }


        /// <summary>
        /// Creates the async version of the service
        /// </summary>
        /// <param name="service">Synchronous service proxy</param>
        public static AsyncDiscoveryServiceProxy ToAsyncService(this DiscoveryServiceProxy service)
        {
            if (!DiscoveryServiceConfigurationType.IsAssignableFrom(service.ServiceConfiguration.GetType()))
                throw new InvalidOperationException($"Cannot create {nameof(AsyncDiscoveryServiceProxy)} from {nameof(DiscoveryServiceProxy)}, because a customer service configuration is used. Expected service configuration type is '{DiscoveryServiceConfigurationType}', but got '{service.ServiceConfiguration.GetType()}'");

            var primaryEndpoint = _primaryEndpoint.Get(service.ServiceConfiguration);
            var asyncService = new AsyncDiscoveryServiceProxy(primaryEndpoint, service.HomeRealmUri, service.ClientCredentials, service.DeviceCredentials);
            asyncService.Timeout = service.Timeout;
            asyncService.UserPrincipalName = service.UserPrincipalName;
            asyncService.EndpointAutoSwitchEnabled = service.EndpointAutoSwitchEnabled;
            foreach (var behavior in service.ServiceConfiguration.CurrentServiceEndpoint.Behaviors)
                asyncService.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(behavior);
            return asyncService;
        }
    }
}
