using System;
using System.Reflection;
using Microsoft.Xrm.Sdk.Async;
using Microsoft.Xrm.Sdk.Query;

namespace Microsoft.Xrm.Sdk.WebServiceClient.Async
{
    /// <summary>Implements <see cref="T:Microsoft.Xrm.Sdk.IOrganizationService"/> and <see cref="T:Microsoft.Xrm.Sdk.Async.IAsyncOrganizationService"/> and provides an authenticated connection to the Organization.svc/web endpoint. This /web endpoint is also used by web resources.</summary>
    public partial class OrganizationWebProxyAsyncClient : WebProxyClient<IWcfAsyncOrganizationService>, IWcfAsyncOrganizationService, IAsyncOrganizationService
    {
        internal bool OfflinePlayback
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
        public UserType userType
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

        /// <summary>constructor_initializes<see cref="T:Microsoft.Xrm.Sdk.WebServiceClient.OrganizationWebProxyClient"></see> class using an Organization service URL.</summary>
        /// <param name="useStrongTypes">Type: Returns_Boolean. When true, use early-bound types; otherwise, false.</param>
        /// <param name="serviceUrl">Type: Returns_URI. The URL of the Organization web service.</param>
        public OrganizationWebProxyAsyncClient(Uri serviceUrl, bool useStrongTypes) : base(serviceUrl, useStrongTypes)
        {
        }

        /// <summary>constructor_initializes<see cref="T:Microsoft.Xrm.Sdk.WebServiceClient.OrganizationWebProxyClient"></see> class using an Organization service URL and an assembly.</summary>
        /// <param name="strongTypeAssembly">Type: Returns_Assembly. An assembly containing early-bound types.</param>
        /// <param name="serviceUrl">Type: Returns_URI. The URL of the Organization web service.</param>
        public OrganizationWebProxyAsyncClient(Uri serviceUrl, Assembly strongTypeAssembly) : base(serviceUrl, strongTypeAssembly)
        {
        }

        /// <summary>constructor_initializes<see cref="T:Microsoft.Xrm.Sdk.WebServiceClient.OrganizationWebProxyClient"></see> class using an Organization service URL and time span.</summary>
        /// <param name="useStrongTypes">Type: Returns_Boolean. When true, use early-bound types; otherwise, false.</param>
        /// <param name="serviceUrl">Type: Returns_URI. The URL of the Organization web service.</param>
        /// <param name="timeout">Type: Returns_TimeSpan. The maximum amount of time a single channel operation has to complete before a timeout fault is raised on a service channel binding.</param>
        public OrganizationWebProxyAsyncClient(Uri serviceUrl, TimeSpan timeout, bool useStrongTypes) : base(serviceUrl, timeout, useStrongTypes)
        {
        }

        /// <summary>constructor_initializes<see cref="T:Microsoft.Xrm.Sdk.WebServiceClient.OrganizationWebProxyClient"></see> class using an Organization service URL, a time span, and an assembly.</summary>
        /// <param name="strongTypeAssembly">Type: Returns_Assembly. An assembly containing early-bound types.</param>
        /// <param name="timeout">Type: Returns_TimeSpan. The maximum amount of time a single channel operation has to complete before a timeout fault is raised on a service channel binding.</param>
        /// <param name="uri">Type: Returns_URI. The URL of the Organization web service.</param>
        public OrganizationWebProxyAsyncClient(Uri uri, TimeSpan timeout, Assembly strongTypeAssembly) : base(uri, timeout, strongTypeAssembly)
        {
        }

        /// <summary>Creates the WCF proxy client initializer which gets invoked on every SDK method call.  This method makes sure that the access token and other header values are added to the outbound call.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.WebServiceClient.WebProxyClientContextInitializer`1"></see>A web proxy client context initializer.</returns>
        protected override WebProxyClientContextInitializer<IWcfAsyncOrganizationService> CreateNewInitializer()
        {
            return new OrganizationWebProxyClientContextInitializer(this);
        }

        private TResult ExecuteAction<TResult>(Func<TResult> action)
        {
            if (action == null) {
                throw new ArgumentNullException("action");
            }
            TResult result;
            using (this.CreateNewInitializer()) {
                result = action();
            }
            return result;
        }

        private void ExecuteAction(Action action)
        {
            if (action == null) {
                throw new ArgumentNullException("action");
            }
            using (this.CreateNewInitializer()) {
                action();
            }
        }

        private T TryRun<T>(Func<T> func, ref bool? retry)
        {
            return func();
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

        /// <summary>Creates a record. </summary>
        /// <returns>Type:Returns_Guid
        /// The ID of the newly created record.</returns>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>. An entity instance that contains the properties to set in the newly created record.</param>
        public Guid Create(Entity entity)
        {
            return this.CreateCore(entity);
        }

        /// <summary>Deletes a record.</summary>
        /// <param name="id">Type: Returns_Guid. The ID of the record that you want to delete.</param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        public void Delete(string entityName, Guid id)
        {
            this.DeleteCore(entityName, id);
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

        /// <summary>Executes a message in the form of a request, and returns a response.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationResponse"></see>The response from the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationRequest"></see>. A request instance that defines the action to be performed.</param>
        public OrganizationResponse Execute(OrganizationRequest request)
        {
            return this.ExecuteCore(request);
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

        /// <summary>Retrieves a collection of records.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.EntityCollection"></see>The collection of entities returned from the query.</returns>
        /// <param name="query">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.QueryBase"></see>. A query that determines the set of records to retrieve.</param>
        public EntityCollection RetrieveMultiple(QueryBase query)
        {
            return this.RetrieveMultipleCore(query);
        }

        /// <summary>Updates an existing record.</summary>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>. An entity instance that has one or more properties set to be updated in the record.</param>
        public void Update(Entity entity)
        {
            this.UpdateCore(entity);
        }

        /// <summary>Creates a record. </summary>
        /// <returns>Type:Returns_Guid
        /// The ID of the newly created record.</returns>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>. An entity instance that contains the properties to set in the newly created record.</param>
        protected internal virtual Guid CreateCore(Entity entity)
        {
            return ExecuteAction<Guid>(() => this.Channel.Create(entity));
        }

        /// <summary>Retrieves a record.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>
        /// The requested entity.</returns>
        /// <param name="id">Type: Returns_Guid. The ID of the record that you want to retrieve.</param>
        /// <param name="columnSet">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.ColumnSet"></see>. A query that specifies the set of columns, or attributes, to retrieve. </param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        protected internal virtual Entity RetrieveCore(string entityName, Guid id, ColumnSet columnSet)
        {
            return ExecuteAction<Entity>(() => this.Channel.Retrieve(entityName, id, columnSet));
        }

        /// <summary>Updates an existing record.</summary>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>. An entity instance that has one or more properties set to be updated in the record.</param>
        protected internal virtual void UpdateCore(Entity entity)
        {
            ExecuteAction(delegate {
                this.Channel.Update(entity);
            });
        }

        /// <summary>Deletes a record.</summary>
        /// <param name="id">Type: Returns_Guid. The ID of the record that you want to delete.</param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        protected internal virtual void DeleteCore(string entityName, Guid id)
        {
            ExecuteAction(delegate {
                this.Channel.Delete(entityName, id);
            });
        }

        /// <summary>Executes a message in the form of a request, and returns a response.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationResponse"></see>The response from the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationRequest"></see>. A request instance that defines the action to be performed.</param>
        protected internal virtual OrganizationResponse ExecuteCore(OrganizationRequest request)
        {
            return ExecuteAction<OrganizationResponse>(() => this.Channel.Execute(request));
        }

        /// <summary>Creates a link between records.</summary>
        /// <param name="relatedEntities">Type: <see cref="T:Microsoft.Xrm.Sdk.EntityReferenceCollection"></see>. property_relatedentities to be associated.</param>
        /// <param name="relationship">Type: <see cref="T:Microsoft.Xrm.Sdk.Relationship"></see>. The name of the relationship to be used to create the link. </param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="entityId">Type: Returns_Guid. property_entityid to which the related records are associated.</param>
        protected internal virtual void AssociateCore(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            ExecuteAction(delegate {
                this.Channel.Associate(entityName, entityId, relationship, relatedEntities);
            });
        }

        /// <summary>Deletes a link between records.</summary>
        /// <param name="relatedEntities">Type: <see cref="T:Microsoft.Xrm.Sdk.EntityReferenceCollection"></see>. A collection of entity references (references to records) to be disassociated.</param>
        /// <param name="relationship">Type: <see cref="T:Microsoft.Xrm.Sdk.Relationship"></see>. The name of the relationship to be used to remove the link.</param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="entityId">Type: Returns_Guid. The ID of the record from which the related records are disassociated.</param>
        protected internal virtual void DisassociateCore(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            ExecuteAction(delegate {
                this.Channel.Disassociate(entityName, entityId, relationship, relatedEntities);
            });
        }

        /// <summary>Retrieves a collection of records.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.EntityCollection"></see>The collection of entities returned from the query.</returns>
        /// <param name="query">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.QueryBase"></see>. A query that determines the set of records to retrieve.</param>
        protected internal virtual EntityCollection RetrieveMultipleCore(QueryBase query)
        {
            return ExecuteAction<EntityCollection>(() => this.Channel.RetrieveMultiple(query));
        }
    }
}
