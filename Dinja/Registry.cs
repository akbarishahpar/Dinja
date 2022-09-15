using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dinja
{
    public class Registry
    {
        private readonly IServiceCollection _services;
        private readonly IConfigurationRoot _configuration;

        public Registry(string configurationJsonPath)
        {
            _services = new ServiceCollection();
            _configuration = LoadConfigurationJson(configurationJsonPath);
        }

        private static IConfigurationRoot LoadConfigurationJson(string path)
        {
            var parentDirectory = Directory.GetParent(AppContext.BaseDirectory);

            if (parentDirectory == null)
                throw new DirectoryNotFoundException("Parent directory is not accessible");

            return new ConfigurationBuilder()
                .SetBasePath(parentDirectory.FullName)
                .AddJsonFile(path, false)
                .Build();
        }

        public Registry AddConfiguration<T>(string key) where T : class
        {
            _services.Configure<T>(_configuration.GetSection(key));
            _services.AddSingleton(sp => sp.GetRequiredService<IOptions<T>>().Value);
            return this;
        }

        public Registry AddSingleton<TService>()
            where TService : class
        {
            _services.AddSingleton<TService>();
            return this;
        }

        public Registry AddSingleton<TService, TImplementation>()
            where TService : class where TImplementation : class, TService
        {
            _services.AddSingleton<TService, TImplementation>();
            return this;
        }

        public Registry AddScoped<TService>()
            where TService : class
        {
            _services.AddScoped<TService>();
            return this;
        }

        public Registry AddScoped<TService, TImplementation>()
            where TService : class where TImplementation : class, TService
        {
            _services.AddScoped<TService, TImplementation>();
            return this;
        }

        public Registry AddTransient<TService>()
            where TService : class
        {
            _services.AddTransient<TService>();
            return this;
        }

        public Registry AddTransient<TService, TImplementation>()
            where TService : class where TImplementation : class, TService
        {
            _services.AddTransient<TService, TImplementation>();
            return this;
        }

        public Registry AddContainer<T>(T container) where T : Container
        {
            container.ConfigureServices(_services, _configuration);
            return this;
        }

        public Registry CreateContainer(Assembly assembly)
        {
            var container = new Container(assembly);
            return AddContainer(container);
        }

        public void AddEntryPoint<T>(Action<T> entryPoint) where T : class
        {
            AddSingleton<T>();

            var serviceProvider = _services.BuildServiceProvider();
            var entryPointService = serviceProvider.GetRequiredService<T>();

            entryPoint(entryPointService);
        }
    }
}