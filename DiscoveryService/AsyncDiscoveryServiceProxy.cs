using System;
using System.ServiceModel.Description;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Async;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Xrm.Sdk.Discovery.Async;

namespace Microsoft.Xrm.Sdk.Client.Async
{
    /// <summary>Implements <see cref="T:Microsoft.Xrm.Sdk.Discovery.IDiscoveryService" /> and <see cref="T:Microsoft.Xrm.Sdk.Discovery.Async.IAsyncDiscoveryService" /> and provides an authenticated WCF channel to the discovery service endpoint.</summary>
    public partial class AsyncDiscoveryServiceProxy : ServiceProxy<IWcfAsyncDiscoveryService>, IDiscoveryService, IAsyncDiscoveryService
    {
        private static readonly PropertyAccessor<bool> _isAuthenticated;
        private static readonly PropertyAccessor<Uri> _homeRealmUri;
        private static readonly PropertyAccessor<ClientCredentials> _deviceCredentials;

        static AsyncDiscoveryServiceProxy()
        {
            var baseType = typeof(ServiceProxy<IWcfAsyncDiscoveryService>);
            _isAuthenticated = ReflectionUtils.CreatePropertyAccessor<bool>(baseType, nameof(IsAuthenticated));
            _homeRealmUri = ReflectionUtils.CreatePropertyAccessor<Uri>(baseType, nameof(HomeRealmUri));
            _deviceCredentials = ReflectionUtils.CreatePropertyAccessor<ClientCredentials>(baseType, nameof(DeviceCredentials));
        }

        /// <summary>Initializes a new instance of the DiscoveryServiceProxy class using a discovery service URI, home realm URI, and client and device credentials.</summary>
        /// <param name="deviceCredentials">Type: Returns_ClientCredentials. The  pn_Windows_Live_ID device credentials.</param>
        /// <param name="homeRealmUri">Type: Returns_URI. This parameter is set to a non-null value when a second ADFS instance is configured as an identity provider to the ADFS instance that pn_CRM_2011 has been configured with for claims authentication. The parameter value is the URI of the WS-Trust metadata endpoint of the second ADFS instance.</param>
        /// <param name="uri">Type: Returns_URI. The URI of the discovery service.</param>
        /// <param name="clientCredentials">Type: Returns_ClientCredentials. The logon credentials of the client.</param>
        public AsyncDiscoveryServiceProxy(Uri uri, Uri homeRealmUri, ClientCredentials clientCredentials, ClientCredentials deviceCredentials)
            : base((IServiceConfiguration<IWcfAsyncDiscoveryService>)new AsyncDiscoveryServiceConfiguration(uri), clientCredentials)
        {
            _isAuthenticated.Set(this, false);
            _homeRealmUri.Set(this, homeRealmUri);
            _deviceCredentials.Set(this, deviceCredentials);
        }

        /// <summary>Initializes a new instance of the DiscoveryServiceProxy class using a service configuration and a security token response.</summary>
        /// <param name="serviceConfiguration">Type: <see cref="T:Microsoft.Xrm.Sdk.Client.IServiceConfiguration`1"></see>. A service configuration.</param>
        /// <param name="securityTokenResponse">Type: <see cref="P:Microsoft.Xrm.Sdk.Client.AuthenticationCredentials.SecurityTokenResponse"></see>. A security token response.</param>
        public AsyncDiscoveryServiceProxy(IServiceConfiguration<IWcfAsyncDiscoveryService> serviceConfiguration, SecurityTokenResponse securityTokenResponse)
            : base(serviceConfiguration, securityTokenResponse)
        {
        }

        /// <summary>Initializes a new instance of the DiscoveryServiceProxy class using a service configuration and client credentials.</summary>
        /// <param name="serviceConfiguration">Type: <see cref="T:Microsoft.Xrm.Sdk.Client.IServiceConfiguration`1"></see>. A service configuration.</param>
        /// <param name="clientCredentials">Type: Returns_ClientCredentials. The logon credentials of the client.</param>
        public AsyncDiscoveryServiceProxy(IServiceConfiguration<IWcfAsyncDiscoveryService> serviceConfiguration, ClientCredentials clientCredentials)
            : base(serviceConfiguration, clientCredentials)
        {
        }

        /// <summary>Initializes a new instance of the DiscoveryServiceProxy class using a service management and a security token response.</summary>
        /// <param name="serviceManagement">Type: <see cref="T:Microsoft.Xrm.Sdk.Client.IServiceConfiguration`1"></see>. A service management.</param>
        /// <param name="securityTokenResponse">Type: <see cref="P:Microsoft.Xrm.Sdk.Client.AuthenticationCredentials.SecurityTokenResponse"></see>. A security token response.</param>
        public AsyncDiscoveryServiceProxy(IServiceManagement<IWcfAsyncDiscoveryService> serviceManagement, SecurityTokenResponse securityTokenResponse)
            : this(serviceManagement as IServiceConfiguration<IWcfAsyncDiscoveryService>, securityTokenResponse)
        {
        }

        /// <summary>Initializes a new instance of the DiscoveryServiceProxy class using a service management and client credentials.</summary>
        /// <param name="serviceManagement">Type: <see cref="T:Microsoft.Xrm.Sdk.Client.IServiceConfiguration`1"></see>. A service management.</param>
        /// <param name="clientCredentials">Type: Returns_ClientCredentials. The logon credentials of the client.</param>
        public AsyncDiscoveryServiceProxy(IServiceManagement<IWcfAsyncDiscoveryService> serviceManagement, ClientCredentials clientCredentials)
            : this(serviceManagement as IServiceConfiguration<IWcfAsyncDiscoveryService>, clientCredentials)
        {
        }

        /// <summary>Executes a discovery service message in the form of a request, and returns a response. </summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryResponse"></see>. The response from processing the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryRequest"></see>. A request instance that defines the action to be performed.</param>
        public DiscoveryResponse Execute(DiscoveryRequest request)
        {
            return CallWithRetries(() => base.ServiceChannel.Channel.Execute(request));
        }

        /// <summary>Executes a discovery service message in the form of a request, and returns a response. </summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryResponse"></see>. The response from processing the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryRequest"></see>. A request instance that defines the action to be performed.</param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        public IAsyncResult BeginExecute(DiscoveryRequest request, AsyncCallback callback, object asyncState)
            => BeginExecute(request, callback, asyncState, CancellationToken.None);

        internal IAsyncResult BeginExecute(DiscoveryRequest request, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<DiscoveryResponse>.Begin("Execute", callback, asyncState, cancellationToken,
                createContext: () => new DiscoveryServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginExecute(request, _callback, _state),
                endOperation: (_asynResult) => base.ServiceChannel.Channel.EndExecute(_asynResult),
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        /// <summary>See IDiscoveryService.<see cref="M:Microsoft.Xrm.Sdk.Discovery.IDiscoveryService.Execute(Microsoft.Xrm.Sdk.Discovery.DiscoveryRequest)"></see></summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryResponse"></see>The response from executing the discovery service request.</returns>
        public DiscoveryResponse EndExecute(IAsyncResult result)
            => ((AsyncCallOperation<DiscoveryResponse>)result).GetResult();

        /// <summary>Executes a discovery service message in the form of a request, and returns a response. </summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryResponse"></see>. The response from processing the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryRequest"></see>. A request instance that defines the action to be performed.</param>
        /// <param name="cancellationToken"></param>
        public Task<DiscoveryResponse> ExecuteAsync(DiscoveryRequest request, CancellationToken cancellationToken)
            => Task.Run(async () => await Task<DiscoveryResponse>.Factory.FromAsync((callback, state) => BeginExecute(request, callback, state, cancellationToken), EndExecute, state: null));

        private T TryRun<T>(Func<T> func, ref bool? retry)
        {
            bool forceClose = false;
            try {
                return func();
            } catch (System.ServiceModel.Security.MessageSecurityException messageSecurityException) {
                forceClose = true;
                retry = base.ShouldRetry(messageSecurityException, retry);
                if (!retry.GetValueOrDefault()) {
                    throw;
                }
            } catch (System.ServiceModel.EndpointNotFoundException) {
                forceClose = true;
                retry = new bool?(base.HandleFailover(retry));
                if (!retry.GetValueOrDefault()) {
                    throw;
                }
            } catch (TimeoutException) {
                forceClose = true;
                retry = new bool?(base.HandleFailover(retry));
                if (!retry.GetValueOrDefault()) {
                    throw;
                }
            } catch (System.ServiceModel.FaultException<OrganizationServiceFault> faultException) {
                forceClose = true;
                retry = base.HandleFailover(faultException.Detail, retry);
                if (!retry.GetValueOrDefault()) {
                    throw;
                }
            } catch {
                forceClose = true;
            } finally {
                this.CloseChannel(forceClose);
            }

            return default(T);
        }

        private T CallWithRetries<T>(Func<T> func)
        {
            T result = default(T);

            bool? retry = null;
            do {
                using (new DiscoveryServiceContextInitializer(this))
                    result = TryRun(func, ref retry);
            }
            while (retry == true);

            return result;
        }
    }
}
