using System.Reflection;
using Dinja.ServiceTypes;
using System.ComponentModel;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Dinja
{
    public class Container
    {
        private readonly Assembly _assembly;

        public Container(Assembly assembly)
        {
            _assembly = assembly;
        }

        public void ConfigureServices(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var serviceTypes = _assembly.GetExportedTypes()
                .Where(type => type.IsClass || type.IsInterface)
                .Where(type => type.GetCustomAttributes(typeof(Service), true).Length > 0)
                .ToList();

            foreach (var serviceType in serviceTypes)
            {
                var attributes = serviceType.GetCustomAttributes(typeof(Service), true);
                foreach (Service attribute in attributes)
                {
                    var implementationType = attribute.ImplementationType ?? serviceType;
                    switch (attribute.LifeCycle)
                    {
                        case ServiceLifeCycle.Singleton:
                            serviceCollection.AddSingleton(serviceType, implementationType);
                            break;
                        case ServiceLifeCycle.Scoped:
                            serviceCollection.AddScoped(serviceType, implementationType);
                            break;
                        case ServiceLifeCycle.Transient:
                            serviceCollection.AddTransient(serviceType, implementationType);
                            break;
                        case ServiceLifeCycle.HostedService:
                            serviceCollection.TryAddEnumerable(
                                ServiceDescriptor.Singleton(typeof(IHostedService), implementationType));
                            break;
                        case ServiceLifeCycle.Configuration:
                            AddConfiguration(serviceCollection, configuration, attribute, implementationType);
                            break;
                        default: throw new InvalidEnumArgumentException(attribute.LifeCycle.ToString());
                    }
                }
            }
        }

        private static void AddConfiguration(IServiceCollection serviceCollection, IConfiguration configuration,
            Service attribute, Type implementationType)
        {
            var settingsName = (attribute as Configuration)?.SettingsName;

            if (string.IsNullOrEmpty(settingsName))
                throw new ArgumentException(nameof(settingsName));

            var configurationSection = configuration.GetSection(settingsName);
            var configureMethod =
                typeof(OptionsConfigurationServiceCollectionExtensions).GetMethod(
                    nameof(OptionsConfigurationServiceCollectionExtensions.Configure),
                    new[] { typeof(IServiceCollection), typeof(IConfiguration) });

            if (configureMethod == null)
                throw new ArgumentException(nameof(configureMethod));

            var configureGenericMethod = configureMethod.MakeGenericMethod(implementationType);
            configureGenericMethod.Invoke(null, new object[] { serviceCollection, configurationSection });
            
            serviceCollection.AddSingleton(implementationType,
                sp =>
                {
                    var configurationType = typeof(IOptions<>).MakeGenericType(implementationType);
                    var service = sp.GetRequiredService(configurationType);
                    var configurationValue = configurationType.GetProperty("Value")?.GetValue(service);
                    if (configurationValue == null)
                        throw new Exception();
                    return configuration;
                });
        }
    }
}