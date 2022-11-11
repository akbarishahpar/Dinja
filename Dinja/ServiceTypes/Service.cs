namespace Dinja.ServiceTypes
{
    public enum ServiceLifeCycle
    {
        Singleton,
        Scoped,
        Transient,
        HostedService,
        Configuration
    }

    public class Service : Attribute
    {
        public ServiceLifeCycle LifeCycle { get; }

        public Type? ServiceType { get; }

        protected Service(ServiceLifeCycle lifeCycle)
        {
            LifeCycle = lifeCycle;
        }

        protected Service(ServiceLifeCycle lifeCycle, Type serviceType)
        {
            LifeCycle = lifeCycle;
            ServiceType = serviceType;
        }
    }
}
