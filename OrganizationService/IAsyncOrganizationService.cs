using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Query;

namespace Microsoft.Xrm.Sdk.Async
{
    public interface IAsyncOrganizationService
    {
        Task<Guid> CreateAsync(Entity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task<Entity> RetrieveAsync(string entityName, Guid id, ColumnSet columnSet, CancellationToken cancellationToken = default(CancellationToken));

        Task UpdateAsync(Entity entity, CancellationToken cancellationToken = default(CancellationToken));

        Task DeleteAsync(string entityName, Guid id, CancellationToken cancellationToken = default(CancellationToken));

        Task<OrganizationResponse> ExecuteAsync(OrganizationRequest request, CancellationToken cancellationToken = default(CancellationToken));

        Task<EntityCollection> RetrieveMultipleAsync(QueryBase query, CancellationToken cancellationToken = default(CancellationToken));

        Task AssociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken = default(CancellationToken));

        Task DisassociateAsync(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken = default(CancellationToken));
    }
}
