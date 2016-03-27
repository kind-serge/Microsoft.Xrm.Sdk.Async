## Summary

Adds `IAsyncOrganizationService` and `IAsyncDiscoveryService` with async versions of interface methods to the CRM Dynamics SDK - the [Microsoft.CrmSdk.CoreAssemblies.Async.nuspec NuGet package](https://www.nuget.org/packages/Microsoft.CrmSdk.CoreAssemblies/).

## Why

When you call `IOrganizationService.Execute(request)` for instance, it blocks the current thread until the reply is received from the Dynamics CRM server. In case when multiple such operations are running in parallel the app will use too many worker threads (because they are blocked) and will start suffer from performance degradation due to thread exhaustion. To avoid this issue, it's better not to block threads and process server responses as soon as they are available. This extension library allows you to do this using Task Parrallel Library (TPL) and asynchronous calls to Dynamics CRM server, e.g. `await IAsyncOrganizationService.ExecuteAsync(request, cancellationToken)`.

## How to use

You just need to create the asyncronous version of the client which exposes the asynchronous version of the interface.

### Organization Service client creation options:

    using Microsoft.Xrm.Sdk.Async;
    using Microsoft.Xrm.Sdk.Client.Async;
    
    // Create Async Proxy directly
    // (implements both IOrganizationService and IAsyncOrganizationService)
    var asyncService =
        new AsyncOrganizationServiceProxy(
            serviceUri,
            realmUri,
            clientCredentials,
            deviceCredentials);
    
    // Create Async Proxy indirectly (with the extension method)
    var asyncService =
        new OrganizationServiceProxy(
            serviceUri,
            realmUri,
            clientCredentials,
            deviceCredentials).ToAsyncService();
    
    using Microsoft.Xrm.Sdk.WebServiceClient.Async;
    
    // Create Async Web Client directly
    // (implements both IOrganizationService and IAsyncOrganizationService)
    var asyncClient =
        new OrganizationWebProxyAsyncClient(serviceUri, timeout, useStrongTypes)
            { HeaderToken = headerToken, SdkClientVersion = sdkClientVersion };
    
    // Create Async Web Client indirectly (with the extension method)
    var asyncClient =
        (new OrganizationWebProxyClient(serviceUri, timeout, useStrongTypes)
            { HeaderToken = headerToken, SdkClientVersion = sdkClientVersion }
        ).ToAsyncService();

### Calling Organization Service asynchronously:

    async Task DoFooAsync(
                  IAsyncOrganizationService service,
                  CancellationToken ct)
    {
        OrganizationRequest request = ...;
        var response = await service.ExecuteAsync(request, ct);
        ...
    }

### Discovery Service client creation options:

    using Microsoft.Xrm.Sdk.Discovery.Async;
    using Microsoft.Xrm.Sdk.Client.Async;
    
    // Create Async Proxy directly
    // (implements both IDiscoveryService and IAsyncDiscoveryService)
    var asyncService =
        new AsyncDiscoveryServiceProxy(
            serviceUri,
            realmUri,
            clientCredentials,
            deviceCredentials);
    
    // Create Async Proxy indirectly (with the extension method)
    var asyncService =
        new DiscoveryServiceProxy(
            serviceUri,
            realmUri,
            clientCredentials,
            deviceCredentials).ToAsyncService();
    
    using Microsoft.Xrm.Sdk.WebServiceClient.Async;
    
    // Create Async Web Client directly
    // (implements both IDiscoveryService and IAsyncDiscoveryService)
    var asyncClient =
        new DiscoveryWebProxyAsyncClient(serviceUri, timeout, useStrongTypes)
            { HeaderToken = headerToken, SdkClientVersion = sdkClientVersion };
    
    // Create Async Web Client indirectly (with the extension method)
    var asyncClient =
        (new DiscoveryWebProxyClient(serviceUri, timeout, useStrongTypes)
            { HeaderToken = headerToken, SdkClientVersion = sdkClientVersion }
        ).ToAsyncService();

### Calling Discovery Service asynchronously:

    async Task<OrganizationDetailCollection> FindOrganizationServices(
                  IAsyncDiscoveryService service,
                  CancellationToken ct)
    {
        var request = new RetrieveOrganizationsRequest();
        var response = await service.ExecuteAsync(request, ct);
        return ((RetrieveOrganizationsResponse)response).Details;
    }

## References

GitHub: https://github.com/tyrotoxin/Microsoft.Xrm.Sdk.Async
NuGet.org: https://www.nuget.org/packages/Microsoft.CrmSdk.CoreAssemblies.Async
License: https://opensource.org/licenses/MIT