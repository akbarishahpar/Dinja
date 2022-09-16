using System.Reflection;
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

        private static IConfigurationRoot LoadConfigurationJson(string configurationJsonPath)
        {
            var parentDirectory = Directory.GetParent(AppContext.BaseDirectory);
            var basePath = parentDirectory?.FullName ?? string.Empty;

            var path = configurationJsonPath;
            if (!Path.IsPathRooted(configurationJsonPath))
                path = Path.Combine(basePath, configurationJsonPath);

            if (!File.Exists(path))
                throw new FileNotFoundException(path);

            return new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(configurationJsonPath, false)
                .Build();
        }

        public Registry AddConfiguration<T>() where T : class
        {
            return AddConfiguration<T>(typeof(T).Name);
        }

        public Registry AddConfiguration<T>(string key) where T : class
        {
            var configurationSection = _configuration.GetSection(key);
            if (configurationSection.Value == null && !configurationSection.GetChildren().Any())
                throw new KeyNotFoundException();
            _services.Configure<T>(configurationSection);
            _services.AddSingleton(sp =>
            {
                var value = sp.GetRequiredService<IOptions<T>>().Value;
                return value;
            });
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

        public Registry AddContainer(Assembly assembly)
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