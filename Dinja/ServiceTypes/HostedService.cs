namespace Dinja.ServiceTypes
{
    public class HostedService : Service
    {
        public HostedService() : base(ServiceLifeCycle.HostedService)
        {
        }
    }
}
