namespace Dinja.ServiceTypes
{
    public class Configuration : Service
    {
        public readonly string SettingsName;

        public Configuration(string settingsName) : base(ServiceLifeCycle.Configuration)
        {
            SettingsName = settingsName;
        }
    }
}