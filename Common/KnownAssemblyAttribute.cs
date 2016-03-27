using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace Microsoft.Xrm.Sdk.Async
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
    internal sealed class KnownAssemblyAttribute : Attribute, IContractBehavior
    {
        private KnownTypesResolver resolver;

        public KnownAssemblyAttribute()
        {
            this.resolver = new KnownTypesResolver();
        }

        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            this.CreateMyDataContractSerializerOperationBehaviors(contractDescription);
        }

        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            this.CreateMyDataContractSerializerOperationBehaviors(contractDescription);
        }

        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }

        private void CreateMyDataContractSerializerOperationBehaviors(ContractDescription contractDescription)
        {
            foreach (OperationDescription current in contractDescription.Operations) {
                this.CreateMyDataContractSerializerOperationBehavior(current);
            }
        }

        private void CreateMyDataContractSerializerOperationBehavior(OperationDescription operation)
        {
            DataContractSerializerOperationBehavior dataContractSerializerOperationBehavior = operation.Behaviors.Find<DataContractSerializerOperationBehavior>();
            if (dataContractSerializerOperationBehavior != null) {
                dataContractSerializerOperationBehavior.DataContractResolver = this.resolver;
            }
        }
    }
}
