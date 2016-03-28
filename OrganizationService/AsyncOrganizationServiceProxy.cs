using System;
using System.Reflection;
using System.Security.Permissions;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Async;
using System.ServiceModel.Description;
using System.ComponentModel;

namespace Microsoft.Xrm.Sdk.Client.Async
{
    /// <summary>Implements <see cref="T:Microsoft.Xrm.Sdk.IOrganizationService"/> and <see cref="T:Microsoft.Xrm.Sdk.Async.IAsyncOrganizationService"/> and provides an authenticated WCF channel to the organization service.</summary>
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

        /// <summary>Initializes a new instance of the <see cref="T:Microsoft.Xrm.Sdk.Client.OrganizationServiceProxy"></see> class using a organization service URI, home realm URI, and client and device credentials.</summary>
        /// <param name="deviceCredentials">Type: Returns_ClientCredentials. The Windows Live ID device credentials.</param>
        /// <param name="homeRealmUri">Type: Returns_URI. This parameter is set to a non-null value when a second ADFS instance is configured as an identity provider to the ADFS instance that pn_CRM_2011 has been configured with for claims authentication. The parameter value is the URI of the WS-Trust metadata endpoint of the second ADFS instance.</param>
        /// <param name="uri">Type: Returns_URI. The URI of the organization service.</param>
        /// <param name="clientCredentials">Type: Returns_ClientCredentials. The logon credentials of the client.</param>
        public AsyncOrganizationServiceProxy(Uri uri, Uri homeRealmUri, ClientCredentials clientCredentials, ClientCredentials deviceCredentials)
            : base((IServiceConfiguration<IWcfAsyncOrganizationService>)new AsyncOrganizationServiceConfiguration(uri), clientCredentials)
        {
            _isAuthenticated.Set(this, false);
            _homeRealmUri.Set(this, homeRealmUri);
            _deviceCredentials.Set(this, deviceCredentials);
        }

        /// <summary>Initializes a new instance of the <see cref="T:Microsoft.Xrm.Sdk.Client.OrganizationServiceProxy"></see> class using a service configuration and a security token response.</summary>
        /// <param name="serviceConfiguration">Type: <see cref="T:Microsoft.Xrm.Sdk.Client.IServiceConfiguration`1"></see>&lt;<see cref="T:Microsoft.Xrm.Sdk.IOrganizationService"></see>&gt;. A service configuration.</param>
        /// <param name="securityTokenResponse">Type: <see cref="T:Microsoft.Xrm.Sdk.Client.SecurityTokenResponse"></see>. A security token response.</param>
        public AsyncOrganizationServiceProxy(IServiceConfiguration<IWcfAsyncOrganizationService> serviceConfiguration, SecurityTokenResponse securityTokenResponse)
            : base(serviceConfiguration, securityTokenResponse)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:Microsoft.Xrm.Sdk.Client.OrganizationServiceProxy"></see> class using a service configuration and client credentials.</summary>
        /// <param name="serviceConfiguration">Type: <see cref="T:Microsoft.Xrm.Sdk.Client.IServiceConfiguration`1"></see>&lt;<see cref="T:Microsoft.Xrm.Sdk.IOrganizationService"></see>&gt;. A service configuration.</param>
        /// <param name="clientCredentials">Type: Returns_ClientCredentials. The logon credentials of the client.</param>
        public AsyncOrganizationServiceProxy(IServiceConfiguration<IWcfAsyncOrganizationService> serviceConfiguration, ClientCredentials clientCredentials)
            : base(serviceConfiguration, clientCredentials)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:Microsoft.Xrm.Sdk.Client.OrganizationServiceProxy"></see> class using a service management and a security token response.</summary>
        /// <param name="serviceManagement">Type: <see cref="T:Microsoft.Xrm.Sdk.Client.IServiceManagement`1"></see>&lt;<see cref="T:Microsoft.Xrm.Sdk.IOrganizationService"></see>&gt;. A service management.</param>
        /// <param name="securityTokenResponse">Type: <see cref="T:Microsoft.Xrm.Sdk.Client.SecurityTokenResponse"></see>. A security token response.</param>
        public AsyncOrganizationServiceProxy(IServiceManagement<IWcfAsyncOrganizationService> serviceManagement, SecurityTokenResponse securityTokenResponse)
            : this(serviceManagement as IServiceConfiguration<IWcfAsyncOrganizationService>, securityTokenResponse)
        {
        }

        /// <summary>Initializes a new instance of the <see cref="T:Microsoft.Xrm.Sdk.Client.OrganizationServiceProxy"></see> class using a service configuration and client credentials.</summary>
        /// <param name="serviceManagement">Type: <see cref="T:Microsoft.Xrm.Sdk.Client.IServiceManagement`1"></see>&lt;<see cref="T:Microsoft.Xrm.Sdk.IOrganizationService"></see>&gt;. A service management.</param>
        /// <param name="clientCredentials">Type: Returns_ClientCredentials. The logon credentials of the client.</param>
        public AsyncOrganizationServiceProxy(IServiceManagement<IWcfAsyncOrganizationService> serviceManagement, ClientCredentials clientCredentials)
            : this(serviceManagement as IServiceConfiguration<IWcfAsyncOrganizationService>, clientCredentials)
        {
        }

        internal bool OfflinePlayback
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the ID of the user for whom SDK calls are made on behalf of.
        /// </summary>
        public Guid CallerId
        {
            get;
            set;
        }

#if SDK8AtLeast

        /// <summary>
        /// For internal use only.
        /// </summary>
        public UserType UserType
        {
            get;
            set;
        }

        /// <summary>
        /// For internal use only.
        /// </summary>
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

        /// <summary>
        /// For internal use only.
        /// </summary>
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

        /// <summary>
        /// Gets or sets the version of the client.
        /// </summary>
        public string SdkClientVersion
        {
            get;
            set;
        }

        /// <summary>Enables support for the early-bound entity types.</summary>
        public void EnableProxyTypes()
        {
            var organizationServiceConfiguration = (AsyncOrganizationServiceConfiguration)base.ServiceConfiguration;
            organizationServiceConfiguration.EnableProxyTypes();
        }

        /// <summary>Enables support for the early-bound entity types exposed in a specified assembly.</summary>
        /// <param name="assembly">Type: Returns_Assembly. An assembly containing early-bound entity types.</param>
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

        /// <summary>Creates a record. </summary>
        /// <returns>Type:Returns_Guid
        /// The ID of the newly created record.</returns>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>. An entity instance that contains the properties to set in the newly created record.</param>
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

        /// <summary>Retrieves a record.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>
        /// The requested entity.</returns>
        /// <param name="id">Type: Returns_Guid. The ID of the record that you want to retrieve.</param>
        /// <param name="columnSet">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.ColumnSet"></see>. A query that specifies the set of columns, or attributes, to retrieve. </param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
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

        /// <summary>Updates an existing record.</summary>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>. An entity instance that has one or more properties set to be updated in the record.</param>
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

        /// <summary>Deletes a record.</summary>
        /// <param name="id">Type: Returns_Guid. The ID of the record that you want to delete.</param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
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

        /// <summary>Executes a message in the form of a request, and returns a response.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationResponse"></see>The response from the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationRequest"></see>. A request instance that defines the action to be performed.</param>
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

        /// <summary>Creates a link between records.</summary>
        /// <param name="relatedEntities">Type: <see cref="T:Microsoft.Xrm.Sdk.EntityReferenceCollection"></see>. property_relatedentities to be associated.</param>
        /// <param name="relationship">Type: <see cref="T:Microsoft.Xrm.Sdk.Relationship"></see>. The name of the relationship to be used to create the link. </param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="entityId">Type: Returns_Guid. property_entityid to which the related records are associated.</param>
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

        /// <summary>Deletes a link between records.</summary>
        /// <param name="relatedEntities">Type: <see cref="T:Microsoft.Xrm.Sdk.EntityReferenceCollection"></see>. A collection of entity references (references to records) to be disassociated.</param>
        /// <param name="relationship">Type: <see cref="T:Microsoft.Xrm.Sdk.Relationship"></see>. The name of the relationship to be used to remove the link.</param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="entityId">Type: Returns_Guid. The ID of the record from which the related records are disassociated.</param>
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

        /// <summary>Retrieves a collection of records.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.EntityCollection"></see>The collection of entities returned from the query.</returns>
        /// <param name="query">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.QueryBase"></see>. A query that determines the set of records to retrieve.</param>
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

        /// <summary>Creates a record. </summary>
        /// <returns>Type:Returns_Guid
        /// The ID of the newly created record.</returns>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>. An entity instance that contains the properties to set in the newly created record.</param>
        public Guid Create(Entity entity)
        {
            return this.CreateCore(entity);
        }

        /// <summary>Retrieves a record.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>
        /// The requested entity.</returns>
        /// <param name="id">Type: Returns_Guid. The ID of the record that you want to retrieve.</param>
        /// <param name="columnSet">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.ColumnSet"></see>. A query that specifies the set of columns, or attributes, to retrieve. </param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
        {
            return this.RetrieveCore(entityName, id, columnSet);
        }

        /// <summary>Updates an existing record.</summary>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>. An entity instance that has one or more properties set to be updated in the record.</param>
        public void Update(Entity entity)
        {
            this.UpdateCore(entity);
        }

        /// <summary>Deletes a record.</summary>
        /// <param name="id">Type: Returns_Guid. The ID of the record that you want to delete.</param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        public void Delete(string entityName, Guid id)
        {
            this.DeleteCore(entityName, id);
        }

        /// <summary>Executes a message in the form of a request, and returns a response.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationResponse"></see>The response from the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationRequest"></see>. A request instance that defines the action to be performed.</param>
        public OrganizationResponse Execute(OrganizationRequest request)
        {
            return this.ExecuteCore(request);
        }

        /// <summary>Creates a link between records.</summary>
        /// <param name="relatedEntities">Type: <see cref="T:Microsoft.Xrm.Sdk.EntityReferenceCollection"></see>. property_relatedentities to be associated.</param>
        /// <param name="relationship">Type: <see cref="T:Microsoft.Xrm.Sdk.Relationship"></see>. The name of the relationship to be used to create the link. </param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="entityId">Type: Returns_Guid. property_entityid to which the related records are associated.</param>
        public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.AssociateCore(entityName, entityId, relationship, relatedEntities);
        }

        /// <summary>Deletes a link between records.</summary>
        /// <param name="relatedEntities">Type: <see cref="T:Microsoft.Xrm.Sdk.EntityReferenceCollection"></see>. A collection of entity references (references to records) to be disassociated.</param>
        /// <param name="relationship">Type: <see cref="T:Microsoft.Xrm.Sdk.Relationship"></see>. The name of the relationship to be used to remove the link.</param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="entityId">Type: Returns_Guid. The ID of the record from which the related records are disassociated.</param>
        public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.DisassociateCore(entityName, entityId, relationship, relatedEntities);
        }

        /// <summary>Retrieves a collection of records.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.EntityCollection"></see>The collection of entities returned from the query.</returns>
        /// <param name="query">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.QueryBase"></see>. A query that determines the set of records to retrieve.</param>
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
