using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Query;
#if SDK7AtLeast
using Microsoft.Xrm.Sdk.WebServiceClient;
using Microsoft.Xrm.Sdk.WebServiceClient.Async;
#endif

namespace Microsoft.Xrm.Sdk.Async
{
    public static class IOrganizationServiceExtensions
    {
        public static IAsyncOrganizationService ToAsyncService(this IOrganizationService service)
        {
            var asyncService = service as IAsyncOrganizationService;
            if (asyncService != null)
                return asyncService;

#if SDK7AtLeast

            var webClient = service as OrganizationWebProxyClient;
            if (webClient != null)
                return webClient.ToAsyncClient();

#endif
            return new AsyncOrganizationServiceWrapper(service);
        }

        public static Task<Guid> CreateAsync(this IOrganizationService service, Entity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var asyncService = service as IAsyncOrganizationService;
            if (asyncService != null)
                return asyncService.CreateAsync(entity, cancellationToken);

            var wcfAsyncService = service as IWcfAsyncOrganizationService;
            if (wcfAsyncService != null)
                return Task.Run(async () => await Task<Guid>.Factory.FromAsync(wcfAsyncService.BeginCreate, wcfAsyncService.EndCreate, entity, state: null));

            return Task.Run(() => service.Create(entity));
        }

        public static Task UpdateAsync(this IOrganizationService service, Entity entity, CancellationToken cancellationToken = default(CancellationToken))
        {
            var asyncService = service as IAsyncOrganizationService;
            if (asyncService != null)
                return asyncService.UpdateAsync(entity, cancellationToken);

            var wcfAsyncService = service as IWcfAsyncOrganizationService;
            if (wcfAsyncService != null)
                return Task.Run(async () => await Task.Factory.FromAsync(wcfAsyncService.BeginUpdate, wcfAsyncService.EndUpdate, entity, state: null));

            return Task.Run(() => service.Update(entity));
        }

        public static Task DeleteAsync(this IOrganizationService service, string entityName, Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            var asyncService = service as IAsyncOrganizationService;
            if (asyncService != null)
                return asyncService.DeleteAsync(entityName, id, cancellationToken);

            var wcfAsyncService = service as IWcfAsyncOrganizationService;
            if (wcfAsyncService != null)
                return Task.Run(async () => await Task.Factory.FromAsync(wcfAsyncService.BeginDelete, wcfAsyncService.EndDelete, entityName, id, state: null));

            return Task.Run(() => service.Delete(entityName, id));
        }

        public static Task AssociateAsync(this IOrganizationService service, string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken = default(CancellationToken))
        {
            var asyncService = service as IAsyncOrganizationService;
            if (asyncService != null)
                return asyncService.AssociateAsync(entityName, entityId, relationship, relatedEntities, cancellationToken);

            var wcfAsyncService = service as IWcfAsyncOrganizationService;
            if (wcfAsyncService != null)
                return Task.Run(async () => await Task.Factory.FromAsync((callback, state) => wcfAsyncService.BeginAssociate(entityName, entityId, relationship, relatedEntities, callback, state), wcfAsyncService.EndAssociate, state: null));

            return Task.Run(() => service.Associate(entityName, entityId, relationship, relatedEntities));
        }

        public static Task DisassociateAsync(this IOrganizationService service, string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, CancellationToken cancellationToken = default(CancellationToken))
        {
            var asyncService = service as IAsyncOrganizationService;
            if (asyncService != null)
                return asyncService.DisassociateAsync(entityName, entityId, relationship, relatedEntities, cancellationToken);

            var wcfAsyncService = service as IWcfAsyncOrganizationService;
            if (wcfAsyncService != null)
                return Task.Run(async () => await Task.Factory.FromAsync((callback, state) => wcfAsyncService.BeginDisassociate(entityName, entityId, relationship, relatedEntities, callback, state), wcfAsyncService.EndDisassociate, state: null));

            return Task.Run(() => service.Disassociate(entityName, entityId, relationship, relatedEntities));
        }

        public static Task<Entity> RetrieveAsync(this IOrganizationService service, string entityName, Guid id, ColumnSet columnSet, CancellationToken cancellationToken = default(CancellationToken))
        {
            var asyncService = service as IAsyncOrganizationService;
            if (asyncService != null)
                return asyncService.RetrieveAsync(entityName, id, columnSet, cancellationToken);

            var wcfAsyncService = service as IWcfAsyncOrganizationService;
            if (wcfAsyncService != null)
                return Task.Run(async () => await Task<Entity>.Factory.FromAsync(wcfAsyncService.BeginRetrieve, wcfAsyncService.EndRetrieve, entityName, id, columnSet, state: null));

            return Task.Run(() => service.Retrieve(entityName, id, columnSet));
        }

        public static Task<EntityCollection> RetrieveMultipleAsync(this IOrganizationService service, QueryBase query, CancellationToken cancellationToken = default(CancellationToken))
        {
            var asyncService = service as IAsyncOrganizationService;
            if (asyncService != null)
                return asyncService.RetrieveMultipleAsync(query, cancellationToken);

            var wcfAsyncService = service as IWcfAsyncOrganizationService;
            if (wcfAsyncService != null)
                return Task.Run(async () => await Task<EntityCollection>.Factory.FromAsync(wcfAsyncService.BeginRetrieveMultiple, wcfAsyncService.EndRetrieveMultiple, query, state: null));

            return Task.Run(() => service.RetrieveMultiple(query));
        }

        public static Task<OrganizationResponse> ExecuteAsync(this IOrganizationService service, OrganizationRequest request, CancellationToken cancellationToken = default(CancellationToken))
        {
            var asyncService = service as IAsyncOrganizationService;
            if (asyncService != null)
                return asyncService.ExecuteAsync(request, cancellationToken);

            var wcfAsyncService = service as IWcfAsyncOrganizationService;
            if (wcfAsyncService != null)
                return Task.Run(async () => await Task<OrganizationResponse>.Factory.FromAsync(wcfAsyncService.BeginExecute, wcfAsyncService.EndExecute, request, state: null));

            return Task.Run(() => service.Execute(request));
        }
    }
}
