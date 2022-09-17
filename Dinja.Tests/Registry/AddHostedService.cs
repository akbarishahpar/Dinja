using Dinja.Tests.Registry.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dinja.Tests.Registry;

[TestFixture]
public class AddHostedService : TestFixtureBase
{
    [Test]
    public void Should_add_hosted_service_into_collection_correctly()
    {
        //Act
        Registry.AddHostedService<BackgroundService>();
        
        //Assert
        Assert.That(Registry.Services
            .Where(s => s.Lifetime == ServiceLifetime.Singleton)
            .Where(s => s.ServiceType == typeof(IHostedService))
            .Any(s => s.ImplementationType == typeof(BackgroundService)), Is.True);
    }
}