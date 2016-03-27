using System;
using System.Reflection;
using Microsoft.Xrm.Sdk.Async;
using Microsoft.Xrm.Sdk.Query;

namespace Microsoft.Xrm.Sdk.WebServiceClient.Async
{
    public partial class OrganizationWebProxyAsyncClient : WebProxyClient<IWcfAsyncOrganizationService>, IWcfAsyncOrganizationService, IAsyncOrganizationService
    {
        internal bool OfflinePlayback
        {
            get;
            set;
        }

        public string SyncOperationType
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

        public UserType userType
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

        public OrganizationWebProxyAsyncClient(Uri serviceUrl, bool useStrongTypes) : base(serviceUrl, useStrongTypes)
        {
        }

        public OrganizationWebProxyAsyncClient(Uri serviceUrl, Assembly strongTypeAssembly) : base(serviceUrl, strongTypeAssembly)
        {
        }

        public OrganizationWebProxyAsyncClient(Uri serviceUrl, TimeSpan timeout, bool useStrongTypes) : base(serviceUrl, timeout, useStrongTypes)
        {
        }

        public OrganizationWebProxyAsyncClient(Uri uri, TimeSpan timeout, Assembly strongTypeAssembly) : base(uri, timeout, strongTypeAssembly)
        {
        }

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

        public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.AssociateCore(entityName, entityId, relationship, relatedEntities);
        }

        public Guid Create(Entity entity)
        {
            return this.CreateCore(entity);
        }

        public void Delete(string entityName, Guid id)
        {
            this.DeleteCore(entityName, id);
        }

        public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            this.DisassociateCore(entityName, entityId, relationship, relatedEntities);
        }

        public OrganizationResponse Execute(OrganizationRequest request)
        {
            return this.ExecuteCore(request);
        }

        public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
        {
            return this.RetrieveCore(entityName, id, columnSet);
        }

        public EntityCollection RetrieveMultiple(QueryBase query)
        {
            return this.RetrieveMultipleCore(query);
        }

        public void Update(Entity entity)
        {
            this.UpdateCore(entity);
        }

        protected internal virtual Guid CreateCore(Entity entity)
        {
            return ExecuteAction<Guid>(() => this.Channel.Create(entity));
        }

        protected internal virtual Entity RetrieveCore(string entityName, Guid id, ColumnSet columnSet)
        {
            return ExecuteAction<Entity>(() => this.Channel.Retrieve(entityName, id, columnSet));
        }

        protected internal virtual void UpdateCore(Entity entity)
        {
            ExecuteAction(delegate {
                this.Channel.Update(entity);
            });
        }

        protected internal virtual void DeleteCore(string entityName, Guid id)
        {
            ExecuteAction(delegate {
                this.Channel.Delete(entityName, id);
            });
        }

        protected internal virtual OrganizationResponse ExecuteCore(OrganizationRequest request)
        {
            return ExecuteAction<OrganizationResponse>(() => this.Channel.Execute(request));
        }

        protected internal virtual void AssociateCore(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            ExecuteAction(delegate {
                this.Channel.Associate(entityName, entityId, relationship, relatedEntities);
            });
        }

        protected internal virtual void DisassociateCore(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
        {
            ExecuteAction(delegate {
                this.Channel.Disassociate(entityName, entityId, relationship, relatedEntities);
            });
        }

        protected internal virtual EntityCollection RetrieveMultipleCore(QueryBase query)
        {
            return ExecuteAction<EntityCollection>(() => this.Channel.RetrieveMultiple(query));
        }
    }
}
