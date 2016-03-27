using System;

namespace Microsoft.Xrm.Sdk.Client.Async
{
    internal abstract class ServiceContextInitializer<TService> : IDisposable where TService : class
    {
        private System.ServiceModel.OperationContextScope _operationScope;

        public ServiceProxy<TService> ServiceProxy
        {
            get;
            private set;
        }

        protected ServiceContextInitializer(ServiceProxy<TService> proxy)
        {
            if (proxy == null)
                throw new ArgumentNullException(nameof(proxy));
            this.ServiceProxy = proxy;
            this.Initialize(proxy);
        }

        protected void Initialize(ServiceProxy<TService> proxy)
        {
            this._operationScope = new System.ServiceModel.OperationContextScope((System.ServiceModel.IContextChannel)((object)proxy.ServiceChannel.Channel));
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~ServiceContextInitializer()
        {
            this.Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && this._operationScope != null)
            {
                this._operationScope.Dispose();
            }
        }
    }
}
