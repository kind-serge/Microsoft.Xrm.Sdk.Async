using System;
using System.Reflection;
using Microsoft.Xrm.Sdk.Async;
using Microsoft.Xrm.Sdk.Discovery.Async;

namespace Microsoft.Xrm.Sdk.Client.Async
{
    /// <summary>Represents a client factory for creating service configurations.</summary>
    public static class AsyncServiceConfigurationFactory
    {
        /// <summary>Creates a service configuration.</summary>
        /// <returns>Type:  <see cref="T:Microsoft.Xrm.Sdk.Client.IServiceConfiguration`1"></see>A service configuration object.</returns>
        /// <param name="serviceUri">Specifies the service’s URI.</param>
        public static IServiceConfiguration<TService> CreateConfiguration<TService>(Uri serviceUri)
        {
            return CreateConfiguration<TService>(serviceUri, false, null);
        }

        /// <summary>Creates a service configuration</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Client.IServiceConfiguration`1"></see>A service configuration object.</returns>
        /// <param name="serviceUri">Specifies the web service’s URI.</param>
        /// <param name="assembly">An assembly containing proxy types.</param>
        /// <param name="enableProxyTypes">True to enable proxy types; otherwise, False.</param>
        public static IServiceConfiguration<TService> CreateConfiguration<TService>(Uri serviceUri, bool enableProxyTypes, Assembly assembly)
        {
            if (serviceUri != null)
            {
                if (typeof(TService) == typeof(IWcfAsyncDiscoveryService))
                {
                    return new AsyncDiscoveryServiceConfiguration(serviceUri) as IServiceConfiguration<TService>;
                }
                if (typeof(TService) == typeof(IWcfAsyncOrganizationService))
                {
                    return new AsyncOrganizationServiceConfiguration(serviceUri, enableProxyTypes, assembly) as IServiceConfiguration<TService>;
                }
            }
            return ServiceConfigurationFactory.CreateConfiguration<TService>(serviceUri, enableProxyTypes, assembly);
        }


        /// <summary>Creates a service management.</summary>
        /// <returns>Type:  <see cref="T:Microsoft.Xrm.Sdk.Client.IServiceManagement`1"></see>A service management object.</returns>
        /// <param name="serviceUri">Specifies the service’s URI.</param>
        public static IServiceManagement<TService> CreateManagement<TService>(Uri serviceUri)
        {
            return CreateManagement<TService>(serviceUri, false, null);
        }

        /// <summary>Creates a service management.</summary>
        /// <returns>Type:  <see cref="T:Microsoft.Xrm.Sdk.Client.IServiceManagement`1"></see>A service management object.</returns>
        /// <param name="serviceUri">Specifies the service’s URI.</param>
        /// <param name="assembly">An assembly containing proxy types.</param>
        /// <param name="enableProxyTypes">True to enable proxy types; otherwise, False.</param>
        public static IServiceManagement<TService> CreateManagement<TService>(Uri serviceUri, bool enableProxyTypes, Assembly assembly)
        {
            if (serviceUri != null)
            {
                if (typeof(TService) == typeof(IWcfAsyncDiscoveryService))
                {
                    return new AsyncDiscoveryServiceConfiguration(serviceUri) as IServiceManagement<TService>;
                }
                if (typeof(TService) == typeof(IWcfAsyncOrganizationService))
                {
                    return new AsyncOrganizationServiceConfiguration(serviceUri, enableProxyTypes, assembly) as IServiceManagement<TService>;
                }
            }
            return ServiceConfigurationFactory.CreateManagement<TService>(serviceUri, enableProxyTypes, assembly);
        }
    }
}
