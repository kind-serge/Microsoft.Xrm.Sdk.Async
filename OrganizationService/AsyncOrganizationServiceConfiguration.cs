using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using Microsoft.Xrm.Sdk.Client;

namespace Microsoft.Xrm.Sdk.Async
{
    internal sealed class AsyncOrganizationServiceConfiguration : IServiceConfiguration<IWcfAsyncOrganizationService>, IServiceManagement<IWcfAsyncOrganizationService>, IEndpointSwitch
    {
        private const string XrmServicesRoot = "xrmservices/";

        private ServiceConfigurationWrapper<IWcfAsyncOrganizationService> service;

        private object _lockObject = new object();

        #region IServiceManagement

        public AuthenticationProviderType AuthenticationType
        {
            get
            {
                return this.service.AuthenticationType;
            }
        }

        public CrossRealmIssuerEndpointCollection CrossRealmIssuerEndpoints
        {
            get
            {
                return this.service.CrossRealmIssuerEndpoints;
            }
        }

        public ServiceEndpoint CurrentServiceEndpoint
        {
            get
            {
                return this.service.CurrentServiceEndpoint;
            }
            set
            {
                this.service.CurrentServiceEndpoint = value;
            }
        }

        public IssuerEndpointDictionary IssuerEndpoints
        {
            get
            {
                return this.service.IssuerEndpoints;
            }
        }

        public PolicyConfiguration PolicyConfiguration
        {
            get
            {
                return this.service.PolicyConfiguration;
            }
        }

        public AuthenticationCredentials Authenticate(AuthenticationCredentials authenticationCredentials)
        {
            return this.service.Authenticate(authenticationCredentials);
        }

        public System.ServiceModel.ChannelFactory<IWcfAsyncOrganizationService> CreateChannelFactory()
        {
            return this.service.CreateChannelFactory(ClientAuthenticationType.Kerberos);
        }

        public System.ServiceModel.ChannelFactory<IWcfAsyncOrganizationService> CreateChannelFactory(ClientAuthenticationType clientAuthenticationType)
        {
            return this.service.CreateChannelFactory(clientAuthenticationType);
        }

        public System.ServiceModel.ChannelFactory<IWcfAsyncOrganizationService> CreateChannelFactory(ClientCredentials clientCredentials)
        {
            return this.service.CreateChannelFactory(clientCredentials);
        }

#if SDK7AtLeast

        public ChannelFactory<IWcfAsyncOrganizationService> CreateChannelFactory(TokenServiceCredentialType endpointType)
        {
            return this.service.CreateChannelFactory(endpointType);
        }

#endif

        public IdentityProvider GetIdentityProvider(string userPrincipalName)
        {
            return this.service.GetIdentityProvider(userPrincipalName);
        }

        #endregion

        #region IEndpointSwitch

        public Uri AlternateEndpoint
        {
            get
            {
                return this.service.AlternateEndpoint;
            }
        }

        public bool EndpointAutoSwitchEnabled
        {
            get
            {
                return this.service.EndpointAutoSwitchEnabled;
            }
            set
            {
                this.service.EndpointAutoSwitchEnabled = value;
            }
        }

        public bool IsPrimaryEndpoint
        {
            get
            {
                return this.service.IsPrimaryEndpoint;
            }
        }

        public Uri PrimaryEndpoint
        {
            get
            {
                return this.service.PrimaryEndpoint;
            }
        }

        public event EventHandler<EndpointSwitchEventArgs> EndpointSwitched
        {
            add
            {
                this.service.EndpointSwitched += value;
            }
            remove
            {
                this.service.EndpointSwitched -= value;
            }
        }

        public event EventHandler<EndpointSwitchEventArgs> EndpointSwitchRequired
        {
            add
            {
                this.service.EndpointSwitchRequired += value;
            }
            remove
            {
                this.service.EndpointSwitchRequired -= value;
            }
        }

        public bool CanSwitch(Uri currentUri)
        {
            return this.service.CanSwitch(currentUri);
        }

        public bool HandleEndpointSwitch()
        {
            return this.service.HandleEndpointSwitch();
        }

        public void SwitchEndpoint()
        {
            this.service.SwitchEndpoint();
        }

        #endregion

        #region IServiceConfiguration

        public IssuerEndpoint CurrentIssuer
        {
            get
            {
                return this.service.CurrentIssuer;
            }
            set
            {
                this.service.CurrentIssuer = value;
            }
        }

        public ServiceEndpointDictionary ServiceEndpoints
        {
            get
            {
                return this.service.ServiceEndpoints;
            }
        }

        public SecurityTokenResponse Authenticate(ClientCredentials clientCredentials)
        {
#if SDK7AtLeast
            throw new InvalidOperationException("Authentication to MSA services is not supported.");
#else
            return this.service.Authenticate(clientCredentials);
#endif
        }

        public SecurityTokenResponse Authenticate(SecurityToken securityToken)
        {
            return this.service.Authenticate(securityToken);
        }

        public SecurityTokenResponse Authenticate(ClientCredentials clientCredentials, SecurityTokenResponse deviceSecurityTokenResponse)
        {
#if SDK7AtLeast
            throw new InvalidOperationException("Authentication to MSA services is not supported.");
#else
            return this.service.Authenticate(clientCredentials, deviceSecurityTokenResponse);
#endif
        }

        public SecurityTokenResponse Authenticate(ClientCredentials clientCredentials, Uri uri, string keyType)
        {
            return this.service.Authenticate(clientCredentials, uri, keyType);
        }

        public SecurityTokenResponse Authenticate(SecurityToken securityToken, Uri uri, string keyType)
        {
            return this.service.Authenticate(securityToken, uri, keyType);
        }

        public SecurityTokenResponse AuthenticateDevice(ClientCredentials clientCredentials)
        {
            return this.service.AuthenticateDevice(clientCredentials);
        }

        public SecurityTokenResponse AuthenticateCrossRealm(ClientCredentials clientCredentials, string appliesTo, Uri crossRealmSts)
        {
            return this.service.AuthenticateCrossRealm(clientCredentials, appliesTo, crossRealmSts);
        }

        public SecurityTokenResponse AuthenticateCrossRealm(SecurityToken securityToken, string appliesTo, Uri crossRealmSts)
        {
            return this.service.AuthenticateCrossRealm(securityToken, appliesTo, crossRealmSts);
        }

#endregion

        internal AsyncOrganizationServiceConfiguration(Uri serviceUri) : this(serviceUri, false, null)
        {
        }

        internal AsyncOrganizationServiceConfiguration(Uri serviceUri, bool enableProxyTypes, Assembly assembly)
        {
            try {
                this.service = new ServiceConfigurationWrapper<IWcfAsyncOrganizationService>(serviceUri, true);
                if (enableProxyTypes && assembly != null) {
                    this.EnableProxyTypes(assembly);
                } else if (enableProxyTypes) {
                    this.EnableProxyTypes();
                }
            } catch (InvalidOperationException ex2) {
                bool flag = true;
                System.Net.WebException ex = ex2.InnerException as System.Net.WebException;
                if (ex != null) {
                    System.Net.HttpWebResponse httpWebResponse = ex.Response as System.Net.HttpWebResponse;
                    if (httpWebResponse != null && httpWebResponse.StatusCode == System.Net.HttpStatusCode.Unauthorized) {
                        flag = !this.AdjustServiceEndpoint(serviceUri);
                    }
                }
                if (flag) {
                    throw;
                }
            }
        }

        public void EnableProxyTypes()
        {
            if (CurrentServiceEndpoint == null)
                throw new InvalidOperationException($"The '{nameof(CurrentServiceEndpoint)}' is NULL");

            lock (this._lockObject) {
                ProxyTypesBehavior proxyTypesBehavior = this.CurrentServiceEndpoint.Behaviors.Find<ProxyTypesBehavior>();
                if (proxyTypesBehavior != null) {
                    this.CurrentServiceEndpoint.Behaviors.Remove(proxyTypesBehavior);
                }
                this.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());
            }
        }

        public void EnableProxyTypes(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            if (CurrentServiceEndpoint == null)
                throw new InvalidOperationException($"The '{nameof(CurrentServiceEndpoint)}' is NULL");

            lock (this._lockObject) {
                ProxyTypesBehavior proxyTypesBehavior = this.CurrentServiceEndpoint.Behaviors.Find<ProxyTypesBehavior>();
                if (proxyTypesBehavior != null) {
                    this.CurrentServiceEndpoint.Behaviors.Remove(proxyTypesBehavior);
                }
                this.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior(assembly));
            }
        }

        private bool AdjustServiceEndpoint(Uri serviceUri)
        {
            Uri uri = RemoveOrgName(serviceUri);
            if (uri != null) {
                this.service = new ServiceConfigurationWrapper<IWcfAsyncOrganizationService>(uri);
                if (this.service != null && this.service.ServiceEndpoints != null) {
                    foreach (KeyValuePair<string, ServiceEndpoint> current in this.service.ServiceEndpoints) {
                        current.Value.Address = new System.ServiceModel.EndpointAddressBuilder(current.Value.Address) {
                            Uri = serviceUri
                        }.ToEndpointAddress();
                    }
                    return true;
                }
            }
            return false;
        }

        private static Uri RemoveOrgName(Uri serviceUri)
        {
            if (!serviceUri.AbsolutePath.StartsWith("/xrmservices/", StringComparison.OrdinalIgnoreCase)) {
                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 2; i < serviceUri.Segments.Length; i++) {
                    stringBuilder.Append(serviceUri.Segments[i]);
                }
                if (stringBuilder.Length > 0) {
                    serviceUri = new UriBuilder(serviceUri.GetComponents(UriComponents.SchemeAndServer, UriFormat.UriEscaped)) {
                        Path = stringBuilder.ToString()
                    }.Uri;
                    return serviceUri;
                }
            }
            return null;
        }
    }
}
