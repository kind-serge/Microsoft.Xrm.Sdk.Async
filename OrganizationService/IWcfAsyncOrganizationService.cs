using System;
using System.ComponentModel;
using System.ServiceModel;
using Microsoft.Xrm.Sdk.Query;

namespace Microsoft.Xrm.Sdk.Async
{
    [KnownAssembly, ServiceContract(Name = "IOrganizationService", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Services")]
    public interface IWcfAsyncOrganizationService : IOrganizationService
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        [OperationContract(AsyncPattern = true, Name = "Create")]
        IAsyncResult BeginCreate(Entity entity, AsyncCallback callback, object asyncState);

        [EditorBrowsable(EditorBrowsableState.Never)]
        Guid EndCreate(IAsyncResult result);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [OperationContract(AsyncPattern = true, Name = "Update")]
        IAsyncResult BeginUpdate(Entity entity, AsyncCallback callback, object asyncState);

        [EditorBrowsable(EditorBrowsableState.Never)]
        void EndUpdate(IAsyncResult result);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [OperationContract(AsyncPattern = true, Name = "Delete")]
        IAsyncResult BeginDelete(string entityName, Guid id, AsyncCallback callback, object asyncState);

        [EditorBrowsable(EditorBrowsableState.Never)]
        void EndDelete(IAsyncResult result);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [OperationContract(AsyncPattern = true, Name = "Associate")]
        IAsyncResult BeginAssociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, AsyncCallback callback, object asyncState);

        [EditorBrowsable(EditorBrowsableState.Never)]
        void EndAssociate(IAsyncResult result);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [OperationContract(AsyncPattern = true, Name = "Disassociate")]
        IAsyncResult BeginDisassociate(string entityName, Guid entityId, Relationship relationship, EntityReferenceCollection relatedEntities, AsyncCallback callback, object asyncState);

        [EditorBrowsable(EditorBrowsableState.Never)]
        void EndDisassociate(IAsyncResult result);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [OperationContract(AsyncPattern = true, Name = "Retrieve")]
        IAsyncResult BeginRetrieve(string entityName, Guid id, ColumnSet columnSet, AsyncCallback callback, object asyncState);

        [EditorBrowsable(EditorBrowsableState.Never)]
        Entity EndRetrieve(IAsyncResult result);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [OperationContract(AsyncPattern = true, Name = "RetrieveMultiple")]
        IAsyncResult BeginRetrieveMultiple(QueryBase query, AsyncCallback callback, object asyncState);

        [EditorBrowsable(EditorBrowsableState.Never)]
        EntityCollection EndRetrieveMultiple(IAsyncResult result);

        [EditorBrowsable(EditorBrowsableState.Never)]
        [OperationContract(AsyncPattern = true, Name = "Execute")]
        IAsyncResult BeginExecute(OrganizationRequest request, AsyncCallback callback, object asyncState);

        [EditorBrowsable(EditorBrowsableState.Never)]
        OrganizationResponse EndExecute(IAsyncResult result);
    }
}
