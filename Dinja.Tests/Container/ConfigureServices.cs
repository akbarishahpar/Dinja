using System.Reflection;
using Dinja.Tests.Container.Services;
using Dinja.Tests.Container.Services.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dinja.Tests.Container;

public class ConfigureServices
{
#pragma warning disable CS8618
    private ServiceCollection _serviceCollection;
    private IConfigurationRoot _configuration;
    private Dinja.Container _container;
#pragma warning restore CS8618

    [SetUp]
    public void SetUp()
    {
        _serviceCollection = new ServiceCollection();
        _configuration = Dinja.Registry.LoadConfigurationJson("appsettings.json");
        _container = new Dinja.Container(Assembly.GetExecutingAssembly());
    }
    
    [Test]
    public void Should_probe_configurations_and_add_to_collection()
    {
       //Act
        _container.ConfigureServices(_serviceCollection, _configuration);
        
        //Assert
        Assert.That(_serviceCollection
                .Where(s => s.Lifetime == ServiceLifetime.Singleton)
                .Any(s => s.ServiceType == typeof(AppVersion))
            , Is.True);
    }
    
    [Test]
    public void Should_probe_singleton_services_and_add_to_collection()
    {
        //Act
        _container.ConfigureServices(_serviceCollection, _configuration);
        
        //Assert
        Assert.That(_serviceCollection
                .Where(s => s.Lifetime == ServiceLifetime.Scoped)
                .Any(s => s.ServiceType == typeof(ScopedService))
            , Is.True);
    }
    
    [Test]
    public void Should_probe_contracted_singleton_services_and_add_to_collection()
    {
        //Act
        _container.ConfigureServices(_serviceCollection, _configuration);
        
        //Assert
        Assert.That(_serviceCollection
                .Where(s => s.Lifetime == ServiceLifetime.Singleton)
                .Where(s => s.ServiceType == typeof(IContractedSingletonService))
                .Any(s => s.ImplementationType == typeof(ContractedSingletonService))
            , Is.True);
    }
    
    [Test]
    public void Should_probe_scoped_services_and_add_to_collection()
    {
        //Act
        _container.ConfigureServices(_serviceCollection, _configuration);
        
        //Assert
        Assert.That(_serviceCollection
                .Where(s => s.Lifetime == ServiceLifetime.Scoped)
                .Any(s => s.ServiceType == typeof(ScopedService))
            , Is.True);
    }
    
    [Test]
    public void Should_probe_contracted_scoped_services_and_add_to_collection()
    {
        //Act
        _container.ConfigureServices(_serviceCollection, _configuration);
        
        //Assert
        Assert.That(_serviceCollection
                .Where(s => s.Lifetime == ServiceLifetime.Scoped)
                .Where(s => s.ServiceType == typeof(IContractedScopedService))
                .Any(s => s.ImplementationType == typeof(ContractedScopedService))
            , Is.True);
    }
    
    [Test]
    public void Should_probe_transient_services_and_add_to_collection()
    {
        //Act
        _container.ConfigureServices(_serviceCollection, _configuration);
        
        //Assert
        Assert.That(_serviceCollection
                .Where(s => s.Lifetime == ServiceLifetime.Transient)
                .Any(s => s.ServiceType == typeof(TransientService))
            , Is.True);
    }
    
    [Test]
    public void Should_probe_contracted_transient_services_and_add_to_collection()
    {
        //Act
        _container.ConfigureServices(_serviceCollection, _configuration);
        
        //Assert
        Assert.That(_serviceCollection
                .Where(s => s.Lifetime == ServiceLifetime.Transient)
                .Where(s => s.ServiceType == typeof(IContractedTransientService))
                .Any(s => s.ImplementationType == typeof(ContractedTransientService))
            , Is.True);
    }
    
    [Test]
    public void Should_probe_hosted_services_services_and_add_to_collection()
    {
        //Act
        _container.ConfigureServices(_serviceCollection, _configuration);
        
        //Assert
        Assert.That(_serviceCollection
                .Where(s => s.Lifetime == ServiceLifetime.Singleton)
                .Where(s => s.ServiceType == typeof(IHostedService))
                .Any(s => s.ImplementationType == typeof(HostedService))
            , Is.True);
    }
}