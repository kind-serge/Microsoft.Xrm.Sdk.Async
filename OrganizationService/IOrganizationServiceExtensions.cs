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
    /// <summary>
    /// Extensions methods for the IOrganizationService interface
    /// </summary>
    public static class IOrganizationServiceExtensions
    {
        /// <summary>
        /// Converts to asynchronous version of the service (if possible)
        /// </summary>
        /// <param name="service">The synchronous service</param>
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

        /// <summary>Creates a record. </summary>
        /// <returns>Type:Returns_Guid
        /// The ID of the newly created record.</returns>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>. An entity instance that contains the properties to set in the newly created record.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="service">The service instance</param>
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

        /// <summary>Updates an existing record.</summary>
        /// <param name="entity">Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>. An entity instance that has one or more properties set to be updated in the record.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="service">The service instance</param>
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

        /// <summary>Deletes a record.</summary>
        /// <param name="id">Type: Returns_Guid. The ID of the record that you want to delete.</param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="service">The service instance</param>
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

        /// <summary>Creates a link between records.</summary>
        /// <param name="relatedEntities">Type: <see cref="T:Microsoft.Xrm.Sdk.EntityReferenceCollection"></see>. property_relatedentities to be associated.</param>
        /// <param name="relationship">Type: <see cref="T:Microsoft.Xrm.Sdk.Relationship"></see>. The name of the relationship to be used to create the link. </param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="entityId">Type: Returns_Guid. property_entityid to which the related records are associated.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="service">The service instance</param>
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

        /// <summary>Deletes a link between records.</summary>
        /// <param name="relatedEntities">Type: <see cref="T:Microsoft.Xrm.Sdk.EntityReferenceCollection"></see>. A collection of entity references (references to records) to be disassociated.</param>
        /// <param name="relationship">Type: <see cref="T:Microsoft.Xrm.Sdk.Relationship"></see>. The name of the relationship to be used to remove the link.</param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="entityId">Type: Returns_Guid. The ID of the record from which the related records are disassociated.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="service">The service instance</param>
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

        /// <summary>Retrieves a record.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Entity"></see>
        /// The requested entity.</returns>
        /// <param name="id">Type: Returns_Guid. The ID of the record that you want to retrieve.</param>
        /// <param name="columnSet">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.ColumnSet"></see>. A query that specifies the set of columns, or attributes, to retrieve. </param>
        /// <param name="entityName">Type: Returns_String. The logical name of the entity that is specified in the entityId parameter.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="service">The service instance</param>
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

        /// <summary>Retrieves a collection of records.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.EntityCollection"></see>The collection of entities returned from the query.</returns>
        /// <param name="query">Type: <see cref="T:Microsoft.Xrm.Sdk.Query.QueryBase"></see>. A query that determines the set of records to retrieve.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="service">The service instance</param>
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

        /// <summary>Executes a message in the form of a request, and returns a response.</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationResponse"></see>The response from the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.OrganizationRequest"></see>. A request instance that defines the action to be performed.</param>
        /// <param name="cancellationToken"></param>
        /// <param name="service">The service instance</param>
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
