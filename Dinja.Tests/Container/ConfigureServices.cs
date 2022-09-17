using System.Reflection;
using Dinja.Tests.Container.Services;
using Dinja.Tests.Container.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dinja.Tests.Container;

public class ConfigureServices
{
    [Test]
    public void Should_probe_services_and_add_to_collection()
    {
        //Arrange
        var serviceCollection = new ServiceCollection();
        var configuration = Dinja.Registry.LoadConfigurationJson("appsettings.json");
        var container = new Dinja.Container(Assembly.GetExecutingAssembly());

        //Act
        container.ConfigureServices(serviceCollection, configuration);
        
        //Assert
        Assert.Multiple(() =>
        {
            Assert.That(serviceCollection
                    .Where(s => s.Lifetime == ServiceLifetime.Singleton)
                    .Any(s => s.ServiceType == typeof(AppVersion))
                , Is.True);
            
            Assert.That(serviceCollection
                    .Where(s => s.Lifetime == ServiceLifetime.Singleton)
                    .Any(s => s.ServiceType == typeof(SingletonService))
                , Is.True);
            Assert.That(serviceCollection
                    .Where(s => s.Lifetime == ServiceLifetime.Scoped)
                    .Any(s => s.ServiceType == typeof(ScopedService))
                , Is.True);
            Assert.That(serviceCollection
                    .Where(s => s.Lifetime == ServiceLifetime.Transient)
                    .Any(s => s.ServiceType == typeof(TransientService))
                , Is.True);
            
            Assert.That(serviceCollection
                    .Where(s => s.Lifetime == ServiceLifetime.Singleton)
                    .Where(s => s.ServiceType == typeof(IContractedSingletonService))
                    .Any(s => s.ImplementationType == typeof(ContractedSingletonService))
                , Is.True);
            Assert.That(serviceCollection
                    .Where(s => s.Lifetime == ServiceLifetime.Scoped)
                    .Where(s => s.ServiceType == typeof(IContractedScopedService))
                    .Any(s => s.ImplementationType == typeof(ContractedScopedService))
                , Is.True);
            Assert.That(serviceCollection
                    .Where(s => s.Lifetime == ServiceLifetime.Transient)
                    .Where(s => s.ServiceType == typeof(IContractedTransientService))
                    .Any(s => s.ImplementationType == typeof(ContractedTransientService))
                , Is.True);
            
            Assert.That(serviceCollection
                    .Where(s => s.Lifetime == ServiceLifetime.Singleton)
                    .Where(s => s.ServiceType == typeof(IHostedService))
                    .Any(s => s.ImplementationType == typeof(HostedService))
                , Is.True);
        });
    }
}