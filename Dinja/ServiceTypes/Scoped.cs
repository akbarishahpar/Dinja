namespace Dinja.ServiceTypes
{
    public class Scoped : Service
    {
        public Scoped() : base(ServiceLifeCycle.Scoped)
        {
        }

        public Scoped(Type serviceType) : base(ServiceLifeCycle.Scoped, serviceType)
        {
        }
    }
}
