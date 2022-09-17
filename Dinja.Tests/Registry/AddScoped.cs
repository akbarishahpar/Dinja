using Dinja.Tests.Registry.Models;
using Microsoft.Extensions.DependencyInjection;
using Version = System.Version;

namespace Dinja.Tests.Registry;

[TestFixture]
public class AddScoped : TestFixtureBase
{
    [Test]
    public void Should_add_scoped_service_into_collection_correctly()
    {
        //Act
        Registry.AddScoped<Version>();
        
        //Assert
        Assert.That(Registry.Services
            .Where(s => s.Lifetime == ServiceLifetime.Scoped)
            .Any(s => s.ServiceType == typeof(Version)), Is.True);
    }
    
    [Test]
    public void Should_add_contracted_scoped_service_into_collection_correctly()
    {
        //Act
        Registry.AddScoped<IAppVersion, AppVersion>();
        
        //Assert
        Assert.That(Registry.Services
            .Where(s => s.Lifetime == ServiceLifetime.Scoped)
            .Where(s => s.ServiceType == typeof(IAppVersion))
            .Any(s => s.ImplementationType == typeof(AppVersion)), Is.True);
    }
}