# Dinja

**Dinja** is a simple and powerful library for managing Dependency Injection (DI) in .NET applications. It is designed to ease the process of registering and managing services, especially when you need to break up your service registration logic across multiple files. It helps developers to set up DI more efficiently, particularly in **Console Applications** where DI is not pre-configured.



### Key Features

- **Service Registration by Attributes**: Use attributes like `[Singleton]`, `[Scoped]`, `[Transient]`, etc., to register services easily without writing repetitive code.
- **Built-In Support for Console Apps**: Easily configure DI in a Console App, reducing boilerplate and enhancing maintainability.
- **Integration with ASP.NET Core**: Works seamlessly with ASP.NET Core projects to register services.
- **Flexible Configuration**: Automatically registers configurations from `appsettings.json` or other configuration files.



## Why Use Dinja?

As your application scales, the `Startup.cs` file often becomes cluttered with dependency registrations, leading to a tangled, hard-to-maintain structure. **Dinja** addresses this by enabling you to distribute service registrations across multiple files, reducing the complexity in the `Startup.cs` file. By leveraging attributes like `[Singleton]`, `[Scoped]`, and `[Transient]`, **Dinja** helps keep your DI setup clean, organized, and more manageable, ensuring your code remains modular and easy to navigate.



## How to Use

### 1. Installation

You can install Dinja via **NuGet**. Run the following command in the **Package Manager Console**:

```powershell
Install-Package Dinja
```

Or via the **.NET CLI**:

```powershell
dotnet add package Dinja
```



### 2. Quick Example: Console Application

Here’s a simple example of how you can use **Dinja** in a Console Application to set up DI and service registration:

```c#
using System.Reflection;
using Dinja;
using Dinja.Examples.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;

var registry = new Registry("appsettings.json")
    .RegisterByExtensionMethod((services, configuration) => services.AddHttpClient())
    .AddContainer(Assembly.GetExecutingAssembly());

await registry.AddEntryPointAsync<App>(async app => await app.Start());
```

In this example:

- We initialize the `Registry` with a configuration file (`appsettings.json`).
- We register services using an extension method.
- We add services from the current assembly using `AddContainer`.



### 3. Service Registration Methods

#### `AddConfiguration`

Easily register configuration models based on settings in `appsettings.json`:

```c#
registry.AddConfiguration<ConfigurationModel>("AppSettings");
```

#### `AddSingleton`

Register services as singleton:

```c#
registry.AddSingleton<ServiceImplementation>();
registry.AddSingleton<IServiceInterface, ServiceImplementation>();
```

#### `AddScoped`

Register services with scoped lifetime:

```c#
registry.AddScoped<ServiceImplementation>();
registry.AddScoped<IServiceInterface, ServiceImplementation>();
```

#### `AddTransient`

Register services with transient lifetime:

```c#
registry.AddTransient<ServiceImplementation>();
registry.AddTransient<IServiceInterface, ServiceImplementation>();
```

#### `AddHostedService`

Register background services (hosted services) for long-running operations:

```c#
registry.AddHostedService<BackgroundJob>();
```



### 4. Service Registration Using Attributes

You can register services with **attributes** to simplify and organize your DI setup.

#### `Configuration Attribute`

Automatically bind a configuration model to a section in `appsettings.json`:

```c#
[Configuration(nameof(AppVersion))]
public class AppVersion
{
    public int Major { get; set; }
    public int Minor { get; set; }
    public int Patch { get; set; }
}
```

#### `Singleton Attribute`

Register a service as a singleton:

```c#
[Singleton]
public class SingletonService
{
    // Singleton service implementation
}
```

#### `Scoped Attribute`

Register a service as scoped:

```c#
[Scoped]
public class ScopedService
{
    // Scoped service implementation
}
```

#### `Transient Attribute`

Register a service as transient:

```c#
[Transient]
public class TransientService
{
    // Transient service implementation
}
```

#### `HostedService Attribute`

Register a background job as a hosted service:

```c#
[HostedService]
public class BackgroundJob : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return Task.CompletedTask;
    }
}
```



### 5. Integration with ASP.NET Core

You can also use Dinja with **ASP.NET Core**. Simply add the `AddContainer` method to the `ConfigureServices` method in `Startup.cs`:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddContainer(Assembly.GetExecutingAssembly());    
    services.AddRazorPages();
    services.AddControllers();
}
```

You can also create a custom container by inheriting from `Container`:

```c#
services.AddContainer<MyContainer>(Configuration);
```



## Contributing

Feel free to fork the project, make improvements, or open issues for any bugs or feature requests.



## License

This project is licensed under the MIT License - see the LICENSE file for details.



