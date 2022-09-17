using Dinja.Tests.Registry.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Dinja.Tests.Registry;

[TestFixture]
public class AddTransient : TestFixtureBase
{
    [Test]
    public void Should_add_transient_service_into_collection_correctly()
    {
        //Act
        Registry.AddTransient<AppVersion>();
        
        //Assert
        Assert.That(Registry.Services
            .Where(s => s.Lifetime == ServiceLifetime.Transient)
            .Any(s => s.ServiceType == typeof(AppVersion)), Is.True);
    }
    
    [Test]
    public void Should_add_contracted_transient_service_into_collection_correctly()
    {
        //Act
        Registry.AddTransient<IAppVersion, AppVersion>();
        
        //Assert
        Assert.That(Registry.Services
            .Where(s => s.Lifetime == ServiceLifetime.Transient)
            .Where(s => s.ServiceType == typeof(IAppVersion))
            .Any(s => s.ImplementationType == typeof(AppVersion)), Is.True);
    }
}