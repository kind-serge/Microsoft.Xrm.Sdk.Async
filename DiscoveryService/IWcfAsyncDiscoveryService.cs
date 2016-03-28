using System;
using System.ComponentModel;
using System.ServiceModel;

namespace Microsoft.Xrm.Sdk.Discovery.Async
{
    /// <summary>Provides programmatic access to organization and user information. </summary>
    [ServiceContract(Name = "IDiscoveryService", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Discovery"), ServiceKnownType(typeof(DiscoveryServiceFault))]
    public interface IWcfAsyncDiscoveryService : IDiscoveryService
    {
        /// <summary>Executes a discovery service message in the form of a request, and returns a response. </summary>
        /// <param name="request">Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryRequest"></see>. A request instance that defines the action to be performed.</param>
        /// <param name="callback"></param>
        /// <param name="asyncState"></param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [OperationContract(AsyncPattern = true, Name = "Execute")]
        IAsyncResult BeginExecute(DiscoveryRequest request, AsyncCallback callback, object asyncState);

        /// <summary>Finishes the asynchronous operation</summary>
        /// <returns>Type: <see cref="T:Microsoft.Xrm.Sdk.Discovery.DiscoveryResponse"></see>. The response from processing the request. You must cast the return value of this method to the specific instance of the response that corresponds to the Request parameter.</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        DiscoveryResponse EndExecute(IAsyncResult result);
    }
}
