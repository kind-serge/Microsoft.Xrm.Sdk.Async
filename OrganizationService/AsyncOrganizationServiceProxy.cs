using System;
using System.Reflection;
using System.Security.Permissions;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Async;
using System.ServiceModel.Description;
using System.ComponentModel;

namespace Microsoft.Xrm.Sdk.Client.Async
{
    public partial class AsyncOrganizationServiceProxy : ServiceProxy<IWcfAsyncOrganizationService>, IOrganizationService
    {
        private static readonly PropertyAccessor<bool> _isAuthenticated;
        private static readonly PropertyAccessor<Uri> _homeRealmUri;
        private static readonly PropertyAccessor<ClientCredentials> _deviceCredentials;

        static AsyncOrganizationServiceProxy()
        {
            var knownAssemblyAttributeType = typeof(IOrganizationService).Assembly.GetType("Microsoft.Xrm.Sdk.KnownAssemblyAttribute");
            var knownAssemblyAttribute = (Attribute)Activator.CreateInstance(knownAssemblyAttributeType);
            TypeDescriptor.AddAttributes(typeof(IWcfAsyncOrganizationService), knownAssemblyAttribute);

            var baseType = typeof(ServiceProxy<IWcfAsyncOrganizationService>);
            _isAuthenticated = ReflectionUtils.CreatePropertyAccessor<bool>(baseType, nameof(IsAuthenticated));
            _homeRealmUri = ReflectionUtils.CreatePropertyAccessor<Uri>(baseType, nameof(HomeRealmUri));
            _deviceCredentials = ReflectionUtils.CreatePropertyAccessor<ClientCredentials>(baseType, nameof(DeviceCredentials));
        }


        private string _xrmSdkAssemblyFileVersion;

        public AsyncOrganizationServiceProxy(Uri uri, Uri homeRealmUri, ClientCredentials clientCredentials, ClientCredentials deviceCredentials)
            : base((IServiceConfiguration<IWcfAsyncOrganizationService>)new AsyncOrganizationServiceConfiguration(uri), clientCredentials)
        {
            _isAuthenticated.Set(this, false);
            _homeRealmUri.Set(this, homeRealmUri);
            _deviceCredentials.Set(this, deviceCredentials);
        }

        public AsyncOrganizationServiceProxy(IServiceConfiguration<IWcfAsyncOrganizationService> serviceConfiguration, SecurityTokenResponse securityTokenResponse)
            : base(serviceConfiguration, securityTokenResponse)
        {
        }

        public AsyncOrganizationServiceProxy(IServiceConfiguration<IWcfAsyncOrganizationService> serviceConfiguration, ClientCredentials clientCredentials)
            : base(serviceConfiguration, clientCredentials)
        {
        }

        public AsyncOrganizationServiceProxy(IServiceManagement<IWcfAsyncOrganizationService> serviceManagement, SecurityTokenResponse securityTokenResponse)
            : this(serviceManagement as IServiceConfiguration<IWcfAsyncOrganizationService>, securityTokenResponse)
        {
        }

        public AsyncOrganizationServiceProxy(IServiceManagement<IWcfAsyncOrganizationService> serviceManagement, ClientCredentials clientCredentials)
            : this(serviceManagement as IServiceConfiguration<IWcfAsyncOrganizationService>, clientCredentials)
        {
        }

        internal bool OfflinePlayback
        {
            get;
            set;
        }

        public Guid CallerId
        {
            get;
            set;
        }

#if SDK8AtLeast

        public UserType UserType
        {
            get;
            set;
        }

        public Guid CallerRegardingObjectId
        {
            get;
            set;
        }

#endif

        internal int LanguageCodeOverride
        {
            get;
            set;
        }

        public string SyncOperationType
        {
            get;
            set;
        }

        internal string ClientAppName
        {
            get;
            set;
        }

        internal string ClientAppVersion
        {
            get;
            set;
        }

        public string SdkClientVersion
        {
            get;
            set;
        }

        public void EnableProxyTypes()
        {
            var organizationServiceConfiguration = (AsyncOrganizationServiceConfiguration)base.ServiceConfiguration;
            organizationServiceConfiguration.EnableProxyTypes();
        }

        public void EnableProxyTypes(Assembly assembly)
        {
            if (assembly == null)
                throw new ArgumentNullException(nameof(assembly));

            var organizationServiceConfiguration = (AsyncOrganizationServiceConfiguration)base.ServiceConfiguration;
            organizationServiceConfiguration.EnableProxyTypes(assembly);
        }

        [PermissionSet(SecurityAction.Demand, Unrestricted = true)]
        internal string GetXrmSdkAssemblyFileVersion()
        {
            if (string.IsNullOrEmpty(this._xrmSdkAssemblyFileVersion)) {
                string[] array = new string[]
                {
                    "Microsoft.Xrm.Sdk.dll"
                };
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
                string[] array2 = array;
                for (int i = 0; i < array2.Length; i++) {
                    string value = array2[i];
                    Assembly[] array3 = assemblies;
                    for (int j = 0; j < array3.Length; j++) {
                        Assembly assembly = array3[j];
                        if (assembly.ManifestModule.Name.Equals(value, StringComparison.OrdinalIgnoreCase)) {
                            this._xrmSdkAssemblyFileVersion = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location).FileVersion;
                        }
                    }
                }
            }
            return this._xrmSdkAssemblyFileVersion;
        }

        protected internal virtual Guid CreateCore(Entity entity)
        {
            bool? retry = null;
            do {
                bool forceClose = false;
                try {
                    using (new OrganizationServiceContextInitializer(this)) {
                        return base.ServiceChannel.Channel.Create(entity);
                    }
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
                    throw;
                } finally {
                    this.CloseChannel(forceClose);
                }
            }
            while (retry.HasValue && retry.Value);
            return Guid.Empty;
        }

        protected internal virtual Entity RetrieveCore(string entityName, Guid id, ColumnSet columnSet)
        {
            bool? retry = null;
            do {
                bool forceClose = false;
                try {
                    using (new OrganizationServiceContextInitializer(this)) {
                        return base.ServiceChannel.Channel.Retrieve(entityName, id, columnSet);
                    }
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
                    throw;
                } finally {
                    this.CloseChannel(forceClose);
                }
            }
            while (retry.HasValue && retry.Value);
            return null;
        }

        protected internal virtual void UpdateCore(Entity entity)
        {
            bool? retry = null;
            do {
                bool forceClose = false;
                try {
                    using (new OrganizationServiceContextInitializer(this)) {
                        base.ServiceChannel.Channel.Update(entity);
                    }
                    break;
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
                    throw;
                } finally {
                    this.CloseChannel(forceClose);
                }
            }
            while (retry.HasValue && retry.Value);
        }

        protected internal virtual void DeleteCore(string entityName, Guid id)
        {
            bool? retry = null;
            do {
                bool forceClose = false;
                try {
                    using (new OrganizationServiceContextInitializer(this)) {
                        base.ServiceChannel.Channel.Delete(entityName, id);
                    }
                    break;
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
                    throw;
                } finally {
                    this.CloseChannel(forceClose);
                }
            }
            while (retry.HasValue && retry.Value);
        }

        protected internal virtual OrganizationResponse ExecuteCore(OrganizationRequest request)
        {
            bool? retry = null;
            do {
                bool forceClose = false;
                try {
                    using (new OrganizationServiceContextInitializer(this)) {
                        return base.ServiceChannel.Channel.Execute(request);
                    }
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
                    throw;
                } finally {
                    this.CloseChannel(forceClose);
                }
            }
            while (retry.HasValue && retry.Value);
            return null;
        }

        protected internal virtual void AssociateCore(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            bool? retry = null;
            do {
                bool forceClose = false;
                try {
                    using (new OrganizationServiceContextInitializer(this)) {
                        base.ServiceChannel.Channel.Associate(entityName, entityId, relationship, relatedEntities);
                    }
                    break;
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
                    throw;
                } finally {
                    this.CloseChannel(forceClose);
                }
            }
            while (retry.HasValue && retry.Value);
        }

        protected internal virtual void DisassociateCore(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            bool? retry = null;
            do {
                bool forceClose = false;
                try {
                    using (new OrganizationServiceContextInitializer(this)) {
                        base.ServiceChannel.Channel.Disassociate(entityName, entityId, relationship, relatedEntities);
                    }
                    break;
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
                    throw;
                } finally {
                    this.CloseChannel(forceClose);
                }
            }
            while (retry.HasValue && retry.Value);
        }

        protected internal virtual EntityCollection RetrieveMultipleCore(QueryBase query)
        {
            bool? retry = null;
            do {
                bool forceClose = false;
                try {
                    using (new OrganizationServiceContextInitializer(this)) {
                        return base.ServiceChannel.Channel.RetrieveMultiple(query);
                    }
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
                    throw;
                } finally {
                    this.CloseChannel(forceClose);
                }
            }
            while (retry.HasValue && retry.Value);
            return null;
        }

        public Guid Create(Entity entity)
        {
            return this.CreateCore(entity);
        }

        public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
        {
            return this.RetrieveCore(entityName, id, columnSet);
        }

        public void Update(Entity entity)
        {
            this.UpdateCore(entity);
        }

        public void Delete(string entityName, Guid id)
        {
            this.DeleteCore(entityName, id);
        }

        public OrganizationResponse Execute(OrganizationRequest request)
        {
            return this.ExecuteCore(request);
        }

        public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.AssociateCore(entityName, entityId, relationship, relatedEntities);
        }

        public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.DisassociateCore(entityName, entityId, relationship, relatedEntities);
        }

        public EntityCollection RetrieveMultiple(QueryBase query)
        {
            return this.RetrieveMultipleCore(query);
        }

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
    }
}
