using System;
using System.ComponentModel;
using System.ServiceModel;

namespace Microsoft.Xrm.Sdk.Discovery.Async
{
    [ServiceContract(Name = "IDiscoveryService", Namespace = "http://schemas.microsoft.com/xrm/2011/Contracts/Discovery"), ServiceKnownType(typeof(DiscoveryServiceFault))]
    public interface IWcfAsyncDiscoveryService : IDiscoveryService
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        [OperationContract(AsyncPattern = true, Name = "Execute")]
        IAsyncResult BeginExecute(DiscoveryRequest request, AsyncCallback callback, object asyncState);

        [EditorBrowsable(EditorBrowsableState.Never)]
        DiscoveryResponse EndExecute(IAsyncResult result);
    }
}
