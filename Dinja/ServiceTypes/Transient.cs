namespace Dinja.ServiceTypes
{
    public class Transient : Service
    {
        public Transient() : base(ServiceLifeCycle.Transient)
        {
        }

        public Transient(Type serviceType) : base(ServiceLifeCycle.Transient, serviceType)
        {
        }
    }
}