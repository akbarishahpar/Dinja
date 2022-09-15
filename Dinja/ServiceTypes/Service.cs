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

        public Type ImplementationType { get; }

        public Service(ServiceLifeCycle lifeCycle)
        {
            LifeCycle = lifeCycle;
        }

        public Service(ServiceLifeCycle lifeCycle, Type implementationType)
        {
            LifeCycle = lifeCycle;
            ImplementationType = implementationType;
        }
    }
}
