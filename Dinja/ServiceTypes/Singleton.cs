namespace Dinja.ServiceTypes
{
    public class Singleton : Service
    {
        public Singleton() : base(ServiceLifeCycle.Singleton)
        {
        }

        public Singleton(Type implementationType) : base(ServiceLifeCycle.Singleton, implementationType)
        {
        }
    }
}
