namespace Dinja.ServiceTypes
{
    public class Scoped : Service
    {
        public Scoped() : base(ServiceLifeCycle.Scoped)
        {
        }

        public Scoped(Type implementationType) : base(ServiceLifeCycle.Scoped, implementationType)
        {
        }
    }
}
