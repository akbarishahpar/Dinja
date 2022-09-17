using System.Reflection;
using Dinja.Exceptions;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dinja
{
    public class Registry
    {
        private readonly IConfigurationRoot _configuration;
        
        public IServiceCollection Services { get; }

        public Registry(string configurationJsonPath)
        {
            Services = new ServiceCollection();
            _configuration = LoadConfigurationJson(configurationJsonPath);
        }

        public static IConfigurationRoot LoadConfigurationJson(string configurationJsonPath)
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
            if (!string.IsNullOrEmpty(configurationSection.Value))
                throw new ShallowConfigurationIsNotSupportedException(key);
            if (!configurationSection.GetChildren().Any())
                throw new KeyNotFoundException();
            Services.Configure<T>(configurationSection);
            Services.AddSingleton(sp => sp.GetRequiredService<IOptions<T>>().Value);
            return this;
        }

        public Registry AddSingleton<TService>()
            where TService : class
        {
            Services.AddSingleton<TService>();
            return this;
        }

        public Registry AddSingleton<TService, TImplementation>()
            where TService : class where TImplementation : class, TService
        {
            Services.AddSingleton<TService, TImplementation>();
            return this;
        }

        public Registry AddScoped<TService>()
            where TService : class
        {
            Services.AddScoped<TService>();
            return this;
        }

        public Registry AddScoped<TService, TImplementation>()
            where TService : class where TImplementation : class, TService
        {
            Services.AddScoped<TService, TImplementation>();
            return this;
        }

        public Registry AddTransient<TService>()
            where TService : class
        {
            Services.AddTransient<TService>();
            return this;
        }

        public Registry AddTransient<TService, TImplementation>()
            where TService : class where TImplementation : class, TService
        {
            Services.AddTransient<TService, TImplementation>();
            return this;
        }

        public Registry AddContainer<T>(T container) where T : Container
        {
            container.ConfigureServices(Services, _configuration);
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

            var serviceProvider = Services.BuildServiceProvider();
            var entryPointService = serviceProvider.GetRequiredService<T>();

            entryPoint(entryPointService);
        }
    }
}