using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Query;

namespace Microsoft.Xrm.Sdk.Async
{
    internal sealed class AsyncOrganizationServiceWrapper : IAsyncOrganizationService, IOrganizationService
    {
        private IOrganizationService _service;

        public AsyncOrganizationServiceWrapper(IOrganizationService service)
        {
            _service = service;
        }

        #region IAsyncOrganizationService

        public Task AssociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken = default(CancellationToken))
            => _service.AssociateAsync(entityName, entityId, relationship, relatedEntities, cancellationToken);

        public Task<Guid> CreateAsync(Entity entity, CancellationToken cancellationToken = default(CancellationToken))
            => _service.CreateAsync(entity, cancellationToken);

        public Task DeleteAsync(string entityName, Guid id, CancellationToken cancellationToken = default(CancellationToken))
            => _service.DeleteAsync(entityName, id, cancellationToken);

        public Task DisassociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken = default(CancellationToken))
            => _service.DisassociateAsync(entityName, entityId, relationship, relatedEntities, cancellationToken);

        public Task<OrganizationResponse> ExecuteAsync(OrganizationRequest request, CancellationToken cancellationToken = default(CancellationToken))
            => _service.ExecuteAsync(request, cancellationToken);

        public Task<Entity> RetrieveAsync(string entityName, Guid id, ColumnSet columnSet, CancellationToken cancellationToken = default(CancellationToken))
            => _service.RetrieveAsync(entityName, id, columnSet, cancellationToken);

        public Task<EntityCollection> RetrieveMultipleAsync(QueryBase query, CancellationToken cancellationToken = default(CancellationToken))
            => _service.RetrieveMultipleAsync(query, cancellationToken);

        public Task UpdateAsync(Entity entity, CancellationToken cancellationToken = default(CancellationToken))
            => _service.UpdateAsync(entity, cancellationToken);

        #endregion

        #region IOrganizationService

        public Guid Create(Entity entity)
            => _service.Create(entity);

        public Entity Retrieve(string entityName, Guid id, ColumnSet columnSet)
            => _service.Retrieve(entityName, id, columnSet);

        public void Update(Entity entity)
            => _service.Update(entity);

        public void Delete(string entityName, Guid id)
            => _service.Delete(entityName, id);

        public OrganizationResponse Execute(OrganizationRequest request)
            => _service.Execute(request);

        public void Associate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
            => _service.Associate(entityName, entityId, relationship, relatedEntities);

        public void Disassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities)
            => _service.Disassociate(entityName, entityId, relationship, relatedEntities);

        public EntityCollection RetrieveMultiple(QueryBase query)
            => _service.RetrieveMultiple(query);

        #endregion
    }
}
