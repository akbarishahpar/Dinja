using Dinja.Tests.Registry.Models;
using Microsoft.Extensions.DependencyInjection;
using Version = System.Version;

namespace Dinja.Tests.Registry;

[TestFixture]
public class AddSingleton : TestFixtureBase
{
    [Test]
    public void Should_add_singleton_service_into_collection_correctly()
    {
        //Act
        Registry.AddSingleton<Version>();
        
        //Assert
        Assert.That(Registry.Services
            .Where(s => s.Lifetime == ServiceLifetime.Singleton)
            .Any(s => s.ServiceType == typeof(Version)), Is.True);
    }
    
    [Test]
    public void Should_add_contracted_singleton_service_into_collection_correctly()
    {
        //Act
        Registry.AddSingleton<IAppVersion, AppVersion>();
        
        //Assert
        Assert.That(Registry.Services
            .Where(s => s.Lifetime == ServiceLifetime.Singleton)
            .Where(s => s.ServiceType == typeof(IAppVersion))
            .Any(s => s.ImplementationType == typeof(AppVersion)), Is.True);
    }
}