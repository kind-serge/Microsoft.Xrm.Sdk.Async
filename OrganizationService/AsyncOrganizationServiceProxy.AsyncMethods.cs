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

        /// <summary>Creates a record. </summary>
        /// <returns>Type:Returns_Guid
        /// The ID of the newly created record.</returns>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>. An entity instance that contains the properties to set in the newly created record.</param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        public IAsyncResult BeginCreate(Entity entity, AsyncCallback callback, object asyncState)
            => BeginCreate(entity, callback, asyncState, CancellationToken.None);

        internal IAsyncResult BeginCreate(Entity entity, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<Guid>.Begin("Create", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginCreate(entity, _callback, _state),
                endOperation: (_asynResult) => base.ServiceChannel.Channel.EndCreate(_asynResult),
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        /// <summary>
        /// Ends the operation
        /// </summary>
        /// <param name="result">The async operation</param>
        public Guid EndCreate(IAsyncResult result) => ((AsyncCallOperation<Guid>)result).GetResult();

        /// <summary>Creates a record. </summary>
        /// <returns>Type:Returns_Guid
        /// The ID of the newly created record.</returns>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>. An entity instance that contains the properties to set in the newly created record.</param>
        /// <param name="cancellationToken"></param>
        public Task<Guid> CreateAsync(Entity entity, CancellationToken cancellationToken) =>
            Task.Run(async () => await Task.Factory.FromAsync((callback, state) => BeginCreate(entity, callback, state, cancellationToken), EndCreate, state: null));

        #endregion

        #region Update

        /// <summary>Updates an existing record.</summary>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>. An entity instance that has one or more properties set to be updated in the record.</param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        public IAsyncResult BeginUpdate(Entity entity, AsyncCallback callback, object asyncState)
            => BeginUpdate(entity, callback, asyncState);

        internal IAsyncResult BeginUpdate(Entity entity, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<int>.Begin("Update", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginUpdate(entity, _callback, _state),
                endOperation: (_asynResult) => { base.ServiceChannel.Channel.EndUpdate(_asynResult); return 0; },
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        /// <summary>
        /// Ends the operation
        /// </summary>
        /// <param name="result">The async operation</param>
        public void EndUpdate(IAsyncResult result) =>
            ((AsyncCallOperation<int>)result).GetResult();

        /// <summary>Updates an existing record.</summary>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>. An entity instance that has one or more properties set to be updated in the record.</param>
        /// <param name="cancellationToken"></param>
        public Task UpdateAsync(Entity entity, CancellationToken cancellationToken)
            => Task.Run(async () => await Task.Factory.FromAsync((callback, state) => BeginUpdate(entity, callback, state, cancellationToken), EndUpdate, state: null));

        #endregion

        #region Delete

        /// <summary>Deletes a record.</summary>
        /// <param name="id">Type: Returns_Guid. The ID of the record that you want to delete.</param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        public IAsyncResult BeginDelete(string entityName, Guid id, AsyncCallback callback, object asyncState)
            => BeginDelete(entityName, id, callback, asyncState, CancellationToken.None);

        internal IAsyncResult BeginDelete(string entityName, Guid id, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<int>.Begin("Delete", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginDelete(entityName, id, _callback, _state),
                endOperation: (_asynResult) => { base.ServiceChannel.Channel.EndDelete(_asynResult); return 0; },
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        /// <summary>
        /// Ends the operation
        /// </summary>
        /// <param name="result">The async operation</param>
        public void EndDelete(IAsyncResult result)
            => ((AsyncCallOperation<int>)result).GetResult();

        /// <summary>Deletes a record.</summary>
        /// <param name="id">Type: Returns_Guid. The ID of the record that you want to delete.</param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="cancellationToken"></param>
        public Task DeleteAsync(string entityName, Guid id, CancellationToken cancellationToken)
            => Task.Run(async () => await Task.Factory.FromAsync((callback, state) => BeginDelete(entityName, id, callback, state, cancellationToken), EndDelete, state: null));

        #endregion

        #region Associate

        /// <summary>Creates a link between records.</summary>
        /// <param name="relatedEntities">Type: <see cref="T:Microsoft.Xrm.Sdk.EntityReferenceCollection"></see>. property_relatedentities to be associated.</param>
        /// <param name="relationship">Type: <see cref="T:Microsoft.Xrm.Sdk.Relationship"></see>. The name of the relationship to be used to create the link. </param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="entityId">Type: Returns_Guid. property_entityid to which the related records are associated.</param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        public IAsyncResult BeginAssociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, AsyncCallback callback, object asyncState)
            => BeginAssociate(entityName, entityId, relationship, relatedEntities, callback, asyncState, CancellationToken.None);

        internal IAsyncResult BeginAssociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<int>.Begin("Associate", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginAssociate(entityName, entityId, relationship, relatedEntities, _callback, _state),
                endOperation: (_asynResult) => { base.ServiceChannel.Channel.EndAssociate(_asynResult); return 0; },
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        /// <summary>
        /// Ends the operation
        /// </summary>
        /// <param name="result">The async operation</param>
        public void EndAssociate(IAsyncResult result)
            => ((AsyncCallOperation<int>)result).GetResult();

        /// <summary>Creates a link between records.</summary>
        /// <param name="relatedEntities">Type: <see cref="T:Microsoft.Xrm.Sdk.EntityReferenceCollection"></see>. property_relatedentities to be associated.</param>
        /// <param name="relationship">Type: <see cref="T:Microsoft.Xrm.Sdk.Relationship"></see>. The name of the relationship to be used to create the link. </param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="entityId">Type: Returns_Guid. property_entityid to which the related records are associated.</param>
        /// <param name="cancellationToken"></param>
        public Task AssociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken)
            => Task.Run(async () => await Task.Factory.FromAsync((callback, state) => BeginAssociate(entityName, entityId, relationship, relatedEntities, callback, state, cancellationToken), EndAssociate, state: null));

        #endregion

        #region Disassociate

        /// <summary>Deletes a link between records.</summary>
        /// <param name="relatedEntities">Type: <see cref="T:Microsoft.Xrm.Sdk.EntityReferenceCollection"></see>. A collection of entity references (references to records) to be disassociated.</param>
        /// <param name="relationship">Type: <see cref="T:Microsoft.Xrm.Sdk.Relationship"></see>. The name of the relationship to be used to remove the link.</param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="entityId">Type: Returns_Guid. The ID of the record from which the related records are disassociated.</param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        public IAsyncResult BeginDisassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, AsyncCallback callback, object asyncState)
            => BeginDisassociate(entityName, entityId, relationship, relatedEntities, callback, asyncState, CancellationToken.None);

        internal IAsyncResult BeginDisassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<int>.Begin("Disassociate", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginDisassociate(entityName, entityId, relationship, relatedEntities, _callback, _state),
                endOperation: (_asynResult) => { base.ServiceChannel.Channel.EndDisassociate(_asynResult); return 0; },
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        /// <summary>
        /// Ends the operation
        /// </summary>
        /// <param name="result">The async operation</param>
        public void EndDisassociate(IAsyncResult result)
            => ((AsyncCallOperation<int>)result).GetResult();

        /// <summary>Deletes a link between records.</summary>
        /// <param name="relatedEntities">Type: <see cref="T:Microsoft.Xrm.Sdk.EntityReferenceCollection"></see>. A collection of entity references (references to records) to be disassociated.</param>
        /// <param name="relationship">Type: <see cref="T:Microsoft.Xrm.Sdk.Relationship"></see>. The name of the relationship to be used to remove the link.</param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="entityId">Type: Returns_Guid. The ID of the record from which the related records are disassociated.</param>
        /// <param name="cancellationToken"></param>
        public Task DisassociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken)
            => Task.Run(async () => await Task.Factory.FromAsync((callback, state) => BeginDisassociate(entityName, entityId, relationship, relatedEntities, callback, state, cancellationToken), EndDisassociate, state: null));

        #endregion

        #region Retrieve

        /// <summary>Retrieves a record.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>
        /// The requested entity.</returns>
        /// <param name="id">Type: Returns_Guid. The ID of the record that you want to retrieve.</param>
        /// <param name="columnSet">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.ColumnSet"></see>. A query that specifies the set of columns, or attributes, to retrieve. </param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        public IAsyncResult BeginRetrieve(string entityName, Guid id, ColumnSet columnSet, AsyncCallback callback, object asyncState)
            => BeginRetrieve(entityName, id, columnSet, callback, asyncState, CancellationToken.None);

        internal IAsyncResult BeginRetrieve(string entityName, Guid id, ColumnSet columnSet, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<Entity>.Begin("Retrieve", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginRetrieve(entityName, id, columnSet, _callback, _state),
                endOperation: (_asynResult) => base.ServiceChannel.Channel.EndRetrieve(_asynResult),
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        /// <summary>
        /// Ends the operation
        /// </summary>
        /// <param name="result">The async operation</param>
        public Entity EndRetrieve(IAsyncResult result)
            => ((AsyncCallOperation<Entity>)result).GetResult();

        /// <summary>Retrieves a record.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>
        /// The requested entity.</returns>
        /// <param name="id">Type: Returns_Guid. The ID of the record that you want to retrieve.</param>
        /// <param name="columnSet">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.ColumnSet"></see>. A query that specifies the set of columns, or attributes, to retrieve. </param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="cancellationToken"></param>
        public Task<Entity> RetrieveAsync(string entityName, Guid id, ColumnSet columnSet, CancellationToken cancellationToken)
            => Task.Run(async () => await Task<Entity>.Factory.FromAsync((callback, state) => BeginRetrieve(entityName, id, columnSet, callback, state, cancellationToken), EndRetrieve, state: null));

        #endregion

        #region RetrieveMultiple

        /// <summary>Retrieves a collection of records.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.EntityCollection"></see>The collection of entities returned from the query.</returns>
        /// <param name="query">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.QueryBase"></see>. A query that determines the set of records to retrieve.</param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        public IAsyncResult BeginRetrieveMultiple(QueryBase query, AsyncCallback callback, object asyncState)
            => BeginRetrieveMultiple(query, callback, asyncState, CancellationToken.None);

        internal IAsyncResult BeginRetrieveMultiple(QueryBase query, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<EntityCollection>.Begin("RetrieveMultiple", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginRetrieveMultiple(query, _callback, _state),
                endOperation: (_asynResult) => base.ServiceChannel.Channel.EndRetrieveMultiple(_asynResult),
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        /// <summary>
        /// Ends the operation
        /// </summary>
        /// <param name="result">The async operation</param>
        public EntityCollection EndRetrieveMultiple(IAsyncResult result)
            => ((AsyncCallOperation<EntityCollection>)result).GetResult();

        /// <summary>Retrieves a collection of records.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.EntityCollection"></see>The collection of entities returned from the query.</returns>
        /// <param name="query">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.QueryBase"></see>. A query that determines the set of records to retrieve.</param>
        /// <param name="cancellationToken"></param>
        public Task<EntityCollection> RetrieveMultipleAsync(QueryBase query, CancellationToken cancellationToken)
            => Task.Run(async () => await Task<EntityCollection>.Factory.FromAsync((callback, state) => BeginRetrieveMultiple(query, callback, state, cancellationToken), EndRetrieveMultiple, state: null));

        #endregion

        #region Execute

        /// <summary>Executes a message in the form of a request, and returns a response.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationResponse"></see>The response from the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationRequest"></see>. A request instance that defines the action to be performed.</param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        public IAsyncResult BeginExecute(OrganizationRequest request, AsyncCallback callback, object asyncState)
            => BeginExecute(request, callback, asyncState, CancellationToken.None);

        internal IAsyncResult BeginExecute(OrganizationRequest request, AsyncCallback callback, object asyncState, CancellationToken cancellationToken)
            => AsyncCallOperation<OrganizationResponse>.Begin("Execute", callback, asyncState, cancellationToken,
                createContext: () => new OrganizationServiceContextInitializer(this),
                tryBegin: TryRun,
                tryEnd: TryRun,
                beginOperation: (_callback, _state) => base.ServiceChannel.Channel.BeginExecute(request, _callback, _state),
                endOperation: (_asynResult) => base.ServiceChannel.Channel.EndExecute(_asynResult),
                cancelOperation: (_asyncResult) => base.ServiceChannel.Abort());

        /// <summary>
        /// Ends the operation
        /// </summary>
        /// <param name="result">The async operation</param>
        public OrganizationResponse EndExecute(IAsyncResult result)
            => ((AsyncCallOperation<OrganizationResponse>)result).GetResult();

        /// <summary>Executes a message in the form of a request, and returns a response.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationResponse"></see>The response from the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationRequest"></see>. A request instance that defines the action to be performed.</param>
        /// <param name="cancellationToken"></param>
        public Task<OrganizationResponse> ExecuteAsync(OrganizationRequest request, CancellationToken cancellationToken)
            => Task.Run(async () => await Task<OrganizationResponse>.Factory.FromAsync((callback, state) => BeginExecute(request, callback, state, cancellationToken), EndExecute, state: null));

        #endregion
    }
}
