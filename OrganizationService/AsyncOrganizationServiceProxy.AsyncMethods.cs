using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Async;
using Microsoft.Xrm.Sdk.Query;

namespace Microsoft.Xrm.Sdk.Client.Async
{
    public partial class AsyncOrganizationServiceProxy : IAsyncOrganizationService, IWcfAsyncOrganizationService
    {
        #region Create

        public IAsyncResult BeginCreate(Entity entity, AsyncCallback callback, object asyncState)
            => BeginCreate(entity, callback, asyncState, CancellationToken.None);

        public IAsyncResult BeginCreate(Entity entity, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<Guid>.Begin("Create", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginCreate(entity, _callback, _state),
                endOperation: (_asynResult) => base.ServiceChannel.Channel.EndCreate(_asynResult),
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        public Guid EndCreate(IAsyncResult result) => ((AsyncCallOperation<Guid>)result).GetResult();

        public Task<Guid> CreateAsync(Entity entity, CancellationToken cancellationToken) =>
            Task.Run(async () => await Task.Factory.FromAsync((callback, state) => BeginCreate(entity, callback, state, cancellationToken), EndCreate, state: null));

        #endregion

        #region Update

        public IAsyncResult BeginUpdate(Entity entity, AsyncCallback callback, object asyncState)
            => BeginUpdate(entity, callback, asyncState);

        public IAsyncResult BeginUpdate(Entity entity, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<int>.Begin("Update", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginUpdate(entity, _callback, _state),
                endOperation: (_asynResult) => { base.ServiceChannel.Channel.EndUpdate(_asynResult); return 0; },
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        public void EndUpdate(IAsyncResult result) =>
            ((AsyncCallOperation<int>)result).GetResult();

        public Task UpdateAsync(Entity entity, CancellationToken cancellationToken)
            => Task.Run(async () => await Task.Factory.FromAsync((callback, state) => BeginUpdate(entity, callback, state, cancellationToken), EndUpdate, state: null));

        #endregion

        #region Delete

        public IAsyncResult BeginDelete(string entityName, Guid id, AsyncCallback callback, object asyncState)
            => BeginDelete(entityName, id, callback, asyncState, CancellationToken.None);

        public IAsyncResult BeginDelete(string entityName, Guid id, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<int>.Begin("Delete", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginDelete(entityName, id, _callback, _state),
                endOperation: (_asynResult) => { base.ServiceChannel.Channel.EndDelete(_asynResult); return 0; },
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        public void EndDelete(IAsyncResult result)
            => ((AsyncCallOperation<int>)result).GetResult();

        public Task DeleteAsync(string entityName, Guid id, CancellationToken cancellationToken)
            => Task.Run(async () => await Task.Factory.FromAsync((callback, state) => BeginDelete(entityName, id, callback, state, cancellationToken), EndDelete, state: null));

        #endregion

        #region Associate

        public IAsyncResult BeginAssociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, AsyncCallback callback, object asyncState)
            => BeginAssociate(entityName, entityId, relationship, relatedEntities, callback, asyncState, CancellationToken.None);
        public IAsyncResult BeginAssociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<int>.Begin("Associate", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginAssociate(entityName, entityId, relationship, relatedEntities, _callback, _state),
                endOperation: (_asynResult) => { base.ServiceChannel.Channel.EndAssociate(_asynResult); return 0; },
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        public void EndAssociate(IAsyncResult result)
            => ((AsyncCallOperation<int>)result).GetResult();

        public Task AssociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken)
            => Task.Run(async () => await Task.Factory.FromAsync((callback, state) => BeginAssociate(entityName, entityId, relationship, relatedEntities, callback, state, cancellationToken), EndAssociate, state: null));

        #endregion

        #region Disassociate

        public IAsyncResult BeginDisassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, AsyncCallback callback, object asyncState)
            => BeginDisassociate(entityName, entityId, relationship, relatedEntities, callback, asyncState, CancellationToken.None);

        public IAsyncResult BeginDisassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<int>.Begin("Disassociate", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginDisassociate(entityName, entityId, relationship, relatedEntities, _callback, _state),
                endOperation: (_asynResult) => { base.ServiceChannel.Channel.EndDisassociate(_asynResult); return 0; },
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        public void EndDisassociate(IAsyncResult result)
            => ((AsyncCallOperation<int>)result).GetResult();

        public Task DisassociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken)
            => Task.Run(async () => await Task.Factory.FromAsync((callback, state) => BeginDisassociate(entityName, entityId, relationship, relatedEntities, callback, state, cancellationToken), EndDisassociate, state: null));

        #endregion

        #region Retrieve

        public IAsyncResult BeginRetrieve(string entityName, Guid id, ColumnSet columnSet, AsyncCallback callback, object asyncState)
            => BeginRetrieve(entityName, id, columnSet, callback, asyncState, CancellationToken.None);

        public IAsyncResult BeginRetrieve(string entityName, Guid id, ColumnSet columnSet, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<Entity>.Begin("Retrieve", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginRetrieve(entityName, id, columnSet, _callback, _state),
                endOperation: (_asynResult) => base.ServiceChannel.Channel.EndRetrieve(_asynResult),
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        public Entity EndRetrieve(IAsyncResult result)
            => ((AsyncCallOperation<Entity>)result).GetResult();

        public Task<Entity> RetrieveAsync(string entityName, Guid id, ColumnSet columnSet, CancellationToken cancellationToken)
            => Task.Run(async () => await Task<Entity>.Factory.FromAsync((callback, state) => BeginRetrieve(entityName, id, columnSet, callback, state, cancellationToken), EndRetrieve, state: null));

        #endregion

        #region RetrieveMultiple

        public IAsyncResult BeginRetrieveMultiple(QueryBase query, AsyncCallback callback, object asyncState)
            => BeginRetrieveMultiple(query, callback, asyncState, CancellationToken.None);

        public IAsyncResult BeginRetrieveMultiple(QueryBase query, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<EntityCollection>.Begin("RetrieveMultiple", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginRetrieveMultiple(query, _callback, _state),
                endOperation: (_asynResult) => base.ServiceChannel.Channel.EndRetrieveMultiple(_asynResult),
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        public EntityCollection EndRetrieveMultiple(IAsyncResult result)
            => ((AsyncCallOperation<EntityCollection>)result).GetResult();

        public Task<EntityCollection> RetrieveMultipleAsync(QueryBase query, CancellationToken cancellationToken)
            => Task.Run(async () => await Task<EntityCollection>.Factory.FromAsync((callback, state) => BeginRetrieveMultiple(query, callback, state, cancellationToken), EndRetrieveMultiple, state: null));

        #endregion

        #region Execute

        public IAsyncResult BeginExecute(OrganizationRequest request, AsyncCallback callback, object asyncState)
            => BeginExecute(request, callback, asyncState, CancellationToken.None);

        public IAsyncResult BeginExecute(OrganizationRequest request, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<OrganizationResponse>.Begin("Execute", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginExecute(request, _callback, _state),
                endOperation: (_asynResult) => base.ServiceChannel.Channel.EndExecute(_asynResult),
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        public OrganizationResponse EndExecute(IAsyncResult result)
            => ((AsyncCallOperation<OrganizationResponse>)result).GetResult();

        public Task<OrganizationResponse> ExecuteAsync(OrganizationRequest request, CancellationToken cancellationToken)
            => Task.Run(async () => await Task<OrganizationResponse>.Factory.FromAsync((callback, state) => BeginExecute(request, callback, state, cancellationToken), EndExecute, state: null));

        #endregion
    }
}
