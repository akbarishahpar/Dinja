using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dinja.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddContainer<T>(this IServiceCollection services, IConfiguration configuration) where T : Container
        {
            Container container = Activator.CreateInstance<T>();
            container.ConfigureServices(services, configuration);
        }

        public static void AddContainer(this IServiceCollection services, IConfiguration configuration, Assembly assembly)
        {
            var container = new Container(assembly);
            container.ConfigureServices(services, configuration);
        }
    }
}
