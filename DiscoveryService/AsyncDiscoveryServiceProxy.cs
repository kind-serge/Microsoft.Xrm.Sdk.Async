using System;
using System.ServiceModel.Description;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Async;
using Microsoft.Xrm.Sdk.Discovery;
using Microsoft.Xrm.Sdk.Discovery.Async;

namespace Microsoft.Xrm.Sdk.Client.Async
{
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

        public AsyncDiscoveryServiceProxy(Uri uri, Uri homeRealmUri, ClientCredentials clientCredentials, ClientCredentials deviceCredentials)
            : base((IServiceConfiguration<IWcfAsyncDiscoveryService>)new AsyncDiscoveryServiceConfiguration(uri), clientCredentials)
        {
            _isAuthenticated.Set(this, false);
            _homeRealmUri.Set(this, homeRealmUri);
            _deviceCredentials.Set(this, deviceCredentials);
        }

        public AsyncDiscoveryServiceProxy(IServiceConfiguration<IWcfAsyncDiscoveryService> serviceConfiguration, SecurityTokenResponse securityTokenResponse)
            : base(serviceConfiguration, securityTokenResponse)
        {
        }

        public AsyncDiscoveryServiceProxy(IServiceConfiguration<IWcfAsyncDiscoveryService> serviceConfiguration, ClientCredentials clientCredentials)
            : base(serviceConfiguration, clientCredentials)
        {
        }

        public AsyncDiscoveryServiceProxy(IServiceManagement<IWcfAsyncDiscoveryService> serviceManagement, SecurityTokenResponse securityTokenResponse)
            : this(serviceManagement as IServiceConfiguration<IWcfAsyncDiscoveryService>, securityTokenResponse)
        {
        }

        public AsyncDiscoveryServiceProxy(IServiceManagement<IWcfAsyncDiscoveryService> serviceManagement, ClientCredentials clientCredentials)
            : this(serviceManagement as IServiceConfiguration<IWcfAsyncDiscoveryService>, clientCredentials)
        {
        }

        public DiscoveryResponse Execute(DiscoveryRequest request)
        {
            return CallWithRetries(() => base.ServiceChannel.Channel.Execute(request));
        }

        public IAsyncResult BeginExecute(DiscoveryRequest request, AsyncCallback callback, object asyncState)
            => BeginExecute(request, callback, asyncState, CancellationToken.None);

        public IAsyncResult BeginExecute(DiscoveryRequest request, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<DiscoveryResponse>.Begin("Execute", callback, asyncState, cancellationToken,
                createContext: () => new DiscoveryServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginExecute(request, _callback, _state),
                endOperation: (_asynResult) => base.ServiceChannel.Channel.EndExecute(_asynResult),
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        public DiscoveryResponse EndExecute(IAsyncResult result)
            => ((AsyncCallOperation<DiscoveryResponse>)result).GetResult();

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
