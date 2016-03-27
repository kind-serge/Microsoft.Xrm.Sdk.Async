using System;
using System.IdentityModel.Tokens;
using System.Reflection;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk.Client;

namespace Microsoft.Xrm.Sdk.Async
{
    [SecuritySafeCritical]
    [SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
    internal sealed class ServiceConfigurationWrapper<TService> : IEndpointSwitch
    {
        private static readonly Func<Uri, bool, object> _ctor;
        private static readonly EventAccessor<EventHandler<EndpointSwitchEventArgs>> _endpointSwitched;
        private static readonly EventAccessor<EventHandler<EndpointSwitchEventArgs>> _endpointSwitchRequired;
        private static readonly PropertyAccessor<bool> _endpointAutoSwitchEnabled;
        private static readonly PropertyAccessor<Uri> _alternateEndpoint;
        private static readonly PropertyAccessor<Uri> _primaryEndpoint;
        private static readonly PropertyAccessor<bool> _isPrimaryEndpoint;
        private static readonly PropertyAccessor<PolicyConfiguration> _policyConfiguration;
        private static readonly PropertyAccessor<ServiceEndpoint> _currentServiceEndpoint;
        private static readonly PropertyAccessor<IssuerEndpoint> _currentIssuer;
        private static readonly PropertyAccessor<AuthenticationProviderType> _authenticationType;
        private static readonly PropertyAccessor<ServiceEndpointDictionary> _serviceEndpoints;
        private static readonly PropertyAccessor<IssuerEndpointDictionary> _issuerEndpoints;
        private static readonly PropertyAccessor<CrossRealmIssuerEndpointCollection> _crossRealmIssuerEndpoints;
        private static readonly Func<object, Uri, bool> _canSwitch;
        private static readonly Func<object, bool> _handleEndpointSwitch;
        private static readonly Func<object, bool> _switchEndpoint;
        private static readonly Func<object, string, IdentityProvider> _getIdentityProvider;
        private static readonly Func<object, ClientAuthenticationType, ChannelFactory<TService>> _createChannelFactory1;
        private static readonly Func<object, ClientCredentials, ChannelFactory<TService>> _createChannelFactory2;
#if SDK7AtLeast
        private static readonly Func<object, TokenServiceCredentialType, ChannelFactory<TService>> _createChannelFactory3;
#endif
        private static readonly Func<object, ClientCredentials, string, Uri, SecurityTokenResponse> _authenticateCrossRealm1;
        private static readonly Func<object, SecurityToken, string, Uri, SecurityTokenResponse> _authenticateCrossRealm2;
        private static readonly Func<object, ClientCredentials, SecurityTokenResponse> _authenticateDevice;
        private static readonly Func<object, AuthenticationCredentials, AuthenticationCredentials> _authenticate1;
        private static readonly Func<object, ClientCredentials, SecurityTokenResponse> _authenticate2;
        private static readonly Func<object, SecurityToken, SecurityTokenResponse> _authenticate3;
        private static readonly Func<object, ClientCredentials, SecurityTokenResponse, SecurityTokenResponse> _authenticate4;
        private static readonly Func<object, SecurityToken, Uri, string, SecurityTokenResponse> _authenticate5;
        private static readonly Func<object, ClientCredentials, Uri, string, SecurityTokenResponse> _authenticate6;

        static ServiceConfigurationWrapper()
        {
            var type = typeof(IOrganizationService).Assembly.GetType("Microsoft.Xrm.Sdk.Client.ServiceConfiguration`1");
            type = type.MakeGenericType(typeof(TService));

            var ctor = type.GetConstructor(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(Uri), typeof(bool) }, null);
            _ctor = (serviceUri, checkForSecondary) => ctor.Invoke(new object[] { serviceUri, checkForSecondary });

            _endpointSwitched = ReflectionUtils.CreateEventAccessor<EventHandler<EndpointSwitchEventArgs>>(type, nameof(EndpointSwitched));
            _endpointSwitchRequired = ReflectionUtils.CreateEventAccessor<EventHandler<EndpointSwitchEventArgs>>(type, nameof(EndpointSwitchRequired));
            _endpointAutoSwitchEnabled = ReflectionUtils.CreatePropertyAccessor<bool>(type, nameof(EndpointAutoSwitchEnabled));
            _alternateEndpoint = ReflectionUtils.CreatePropertyAccessor<Uri>(type, nameof(AlternateEndpoint));
            _primaryEndpoint = ReflectionUtils.CreatePropertyAccessor<Uri>(type, nameof(PrimaryEndpoint));
            _isPrimaryEndpoint = ReflectionUtils.CreatePropertyAccessor<bool>(type, nameof(IsPrimaryEndpoint));
            _policyConfiguration = ReflectionUtils.CreatePropertyAccessor<PolicyConfiguration>(type, nameof(PolicyConfiguration));
            _currentServiceEndpoint = ReflectionUtils.CreatePropertyAccessor<ServiceEndpoint>(type, nameof(CurrentServiceEndpoint));
            _currentIssuer = ReflectionUtils.CreatePropertyAccessor<IssuerEndpoint>(type, nameof(CurrentIssuer));
            _authenticationType = ReflectionUtils.CreatePropertyAccessor<AuthenticationProviderType>(type, nameof(AuthenticationType));
            _serviceEndpoints = ReflectionUtils.CreatePropertyAccessor<ServiceEndpointDictionary>(type, nameof(ServiceEndpoints));
            _issuerEndpoints = ReflectionUtils.CreatePropertyAccessor<IssuerEndpointDictionary>(type, nameof(IssuerEndpoints));
            _crossRealmIssuerEndpoints = ReflectionUtils.CreatePropertyAccessor<CrossRealmIssuerEndpointCollection>(type, nameof(CrossRealmIssuerEndpoints));
            _canSwitch = ReflectionUtils.CreateMethodAccessor<Uri, bool>(type, nameof(CanSwitch));
            _handleEndpointSwitch = ReflectionUtils.CreateMethodAccessor<bool>(type, nameof(HandleEndpointSwitch));
            _switchEndpoint = ReflectionUtils.CreateMethodAccessor<bool>(type, nameof(SwitchEndpoint));
            _getIdentityProvider = ReflectionUtils.CreateMethodAccessor<string, IdentityProvider>(type, nameof(GetIdentityProvider));
            _createChannelFactory1 = ReflectionUtils.CreateMethodAccessor<ClientAuthenticationType, ChannelFactory<TService>>(type, nameof(CreateChannelFactory));
            _createChannelFactory2 = ReflectionUtils.CreateMethodAccessor<ClientCredentials, ChannelFactory<TService>>(type, nameof(CreateChannelFactory));
#if SDK7AtLeast
            _createChannelFactory3 = ReflectionUtils.CreateMethodAccessor<TokenServiceCredentialType, ChannelFactory<TService>>(type, nameof(CreateChannelFactory));
#endif
            _authenticateCrossRealm1 = ReflectionUtils.CreateMethodAccessor<ClientCredentials, string, Uri, SecurityTokenResponse>(type, nameof(AuthenticateCrossRealm));
            _authenticateCrossRealm2 = ReflectionUtils.CreateMethodAccessor<SecurityToken, string, Uri, SecurityTokenResponse>(type, nameof(AuthenticateCrossRealm));
            _authenticateDevice = ReflectionUtils.CreateMethodAccessor<ClientCredentials, SecurityTokenResponse>(type, nameof(AuthenticateDevice));
            _authenticate1 = ReflectionUtils.CreateMethodAccessor<AuthenticationCredentials, AuthenticationCredentials>(type, nameof(Authenticate));
            _authenticate2 = ReflectionUtils.CreateMethodAccessor<ClientCredentials, SecurityTokenResponse>(type, nameof(Authenticate));
            _authenticate3 = ReflectionUtils.CreateMethodAccessor<SecurityToken, SecurityTokenResponse>(type, nameof(Authenticate));
            _authenticate4 = ReflectionUtils.CreateMethodAccessor<ClientCredentials, SecurityTokenResponse, SecurityTokenResponse>(type, nameof(Authenticate));
            _authenticate5 = ReflectionUtils.CreateMethodAccessor<SecurityToken, Uri, string, SecurityTokenResponse>(type, nameof(Authenticate));
            _authenticate6 = ReflectionUtils.CreateMethodAccessor<ClientCredentials, Uri, string, SecurityTokenResponse>(type, nameof(Authenticate));
        }

        private object _config;

        public ServiceConfigurationWrapper(Uri serviceUri) : this(serviceUri, true) { }

        internal ServiceConfigurationWrapper(Uri serviceUri, bool checkForSecondary)
        {
            _config = _ctor(serviceUri, checkForSecondary);
        }

        public event EventHandler<EndpointSwitchEventArgs> EndpointSwitched
        {
            add
            {
                _endpointSwitched.Add(_config, value);
            }
            remove
            {
                _endpointSwitched.Remove(_config, value);
            }
        }

        public event EventHandler<EndpointSwitchEventArgs> EndpointSwitchRequired
        {
            add
            {
                _endpointSwitchRequired.Add(_config, value);
            }
            remove
            {
                _endpointSwitchRequired.Remove(_config, value);
            }
        }

        public bool EndpointAutoSwitchEnabled
        {
            get
            {
                return _endpointAutoSwitchEnabled.Get(_config);
            }
            set
            {
                _endpointAutoSwitchEnabled.Set(_config, value);
            }
        }

        public Uri AlternateEndpoint
        {
            get
            {
                return _alternateEndpoint.Get(_config);
            }
        }

        public Uri PrimaryEndpoint
        {
            get
            {
                return _primaryEndpoint.Get(_config);
            }
        }

        public bool IsPrimaryEndpoint
        {
            get
            {
                return _isPrimaryEndpoint.Get(_config);
            }
        }

        public PolicyConfiguration PolicyConfiguration
        {
            get
            {
                return _policyConfiguration.Get(_config);
            }
            set
            {
                _policyConfiguration.Set(_config, value);
            }
        }

        public ServiceEndpoint CurrentServiceEndpoint
        {
            get
            {
                return _currentServiceEndpoint.Get(_config);
            }
            set
            {
                _currentServiceEndpoint.Set(_config, value);
            }
        }

        public IssuerEndpoint CurrentIssuer
        {
            get
            {
                return _currentIssuer.Get(_config);
            }
            set
            {
                _currentIssuer.Set(_config, value);
            }
        }

        public AuthenticationProviderType AuthenticationType
        {
            get
            {
                return _authenticationType.Get(_config);
            }
        }

        public ServiceEndpointDictionary ServiceEndpoints
        {
            get
            {
                return _serviceEndpoints.Get(_config);
            }
        }

        public IssuerEndpointDictionary IssuerEndpoints
        {
            get
            {
                return _issuerEndpoints.Get(_config);
            }
        }

        public CrossRealmIssuerEndpointCollection CrossRealmIssuerEndpoints
        {
            get
            {
                return _crossRealmIssuerEndpoints.Get(_config);
            }
        }

        public bool CanSwitch(Uri currentUri)
        {
            return _canSwitch(_config, currentUri);
        }

        public bool HandleEndpointSwitch()
        {
            return _handleEndpointSwitch(_config);
        }

        public void SwitchEndpoint()
        {
            _switchEndpoint(_config);
        }

        public IdentityProvider GetIdentityProvider(string userPrincipalName)
        {
            return _getIdentityProvider(_config, userPrincipalName);
        }

        public ChannelFactory<TService> CreateChannelFactory(ClientAuthenticationType clientAuthenticationType)
        {
            return _createChannelFactory1(_config, clientAuthenticationType);
        }

        public ChannelFactory<TService> CreateChannelFactory(ClientCredentials clientCredentials)
        {
            return _createChannelFactory2(_config, clientCredentials);
        }

#if SDK7AtLeast

        public ChannelFactory<TService> CreateChannelFactory(TokenServiceCredentialType endpointType)
        {
            return _createChannelFactory3(_config, endpointType);
        }

#endif

        public SecurityTokenResponse AuthenticateCrossRealm(ClientCredentials clientCredentials, string appliesTo, Uri crossRealmSts)
        {
            return _authenticateCrossRealm1(_config, clientCredentials, appliesTo, crossRealmSts);
        }

        public SecurityTokenResponse AuthenticateCrossRealm(SecurityToken securityToken, string appliesTo, Uri crossRealmSts)
        {
            return _authenticateCrossRealm2(_config, securityToken, appliesTo, crossRealmSts);
        }

        public SecurityTokenResponse AuthenticateDevice(ClientCredentials clientCredentials)
        {
            return _authenticateDevice(_config, clientCredentials);
        }

        public AuthenticationCredentials Authenticate(AuthenticationCredentials authenticationCredentials)
        {
            return _authenticate1(_config, authenticationCredentials);
        }

        public SecurityTokenResponse Authenticate(ClientCredentials clientCredentials)
        {
            return _authenticate2(_config, clientCredentials);
        }

        public SecurityTokenResponse Authenticate(SecurityToken securityToken)
        {
            return _authenticate3(_config, securityToken);
        }

        public SecurityTokenResponse Authenticate(ClientCredentials clientCredentials, SecurityTokenResponse deviceTokenResponse)
        {
            return _authenticate4(_config, clientCredentials, deviceTokenResponse);
        }

        internal SecurityTokenResponse Authenticate(SecurityToken securityToken, Uri uri, string keyType)
        {
            return _authenticate5(_config, securityToken, uri, keyType);
        }

        internal SecurityTokenResponse Authenticate(ClientCredentials clientCredentials, Uri uri, string keyType)
        {
            return _authenticate6(_config, clientCredentials, uri, keyType);
        }
    }
}
