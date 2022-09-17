# Dinja
Theses days it is common to use Microsoft dependency injection tool for .Net developers. It's lightweight, easy to use and integrated with ASP.Net Core, MAUI and probably other frameworks developed by Microsoft. However, when it comes to register services into service collection using AddSingleton, AddScoped and AddTransient methods in ,for example, Startup.cs file, it is easy to get lost in a huge amount of codes written to just register these services. Moreover, in console applications, there is no pre-installed dependency injection tool and the burden is on the developer to take repetitive steps for using dependency injection. To address these issues, I've developed Dinja. It allows us to distribute registering services among the files by use of service registration attributes. In addition to service registration attributes, It helps developers to create an environment to use dependency injection in templates such as Console App easily.



# How to use this library?

Well, it is super easy. Dinja comes with two features. First, a Registry class by which it's possible to register services and run an entry point fed by injected services. Take a look at following example:

Consider that a console application is developed and the following code is describing **Program.cs**. The following code consist of instantiating a new **Registry** which is supplied a **appsettings.json** file. Usually configurations of application are placed into this json file. As it is apparent, this registry uses a builder-pattern like to allow developer to register services.

```c#
using System.Reflection;
using Dinja;
using Dinja.Examples.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;

var registry = new Registry("appsettings.json")
    .RegisterByExtensionMethod(s => s.AddHttpClient())
    .AddContainer(Assembly.GetExecutingAssembly());

await registry.AddEntryPointAsync<App>(async app => await app.Start());
```

Developers may use methods listed below to register their services:

* **AddConfiguration**

  * This methods helps developers to easily register their configuration models which their values are placed in e.g. **appsettings.json**. It is a generic method which can be used like following peace of code:

    ```c#
    Registry.AddConfiguration<ConfigurationModel>(configurationKey);
    ```

* **AddSingleton**

  * This method helps developers to register their classes as a singleton service.  Developers can use these method either to register just implementations or with an interface. It is a generic method which can be used like following peace of code:

    ```c#
    Registry.AddSingleton<ServiceImplementation>();
    Registry.AddSingleton<IServiceInterface, ServiceImplementation>();
    ```

* **AddScoped**

  * This method helps developers to register their classes as a scoped service.  Developers can use these method either to register just implementations or with an interface. It is a generic method which can be used like following peace of code:

    ```c#
    Registry.AddScoped<ServiceImplementation>();
    Registry.AddScoped<IServiceInterface, ServiceImplementation>();
    ```

    

* **AddTransient**

  * This method helps developers to register their classes as a transient service.  Developers can use these method either to register just implementations or with an interface. It is a generic method which can be used like following peace of code:

    ```c#
    Registry.AddScoped<ServiceImplementation>();
    Registry.AddScoped<IServiceInterface, ServiceImplementation>();
    ```

    

* **AddHostedService**

  * This method helps developers to register their background jobs as a hosted service. It is a generic method which can be used like following peace of code:

    ```c#
    Registry.AddHostedService<BackgroundJob>();
    ```

    

* **AddContainer**

  * In addition to methods mentioned above, it is possible to register dependencies just where the services are implemented. This is happened by use of attributes which are offered by Dinja. These attributes are listed below:

    * **Configuration**

      It is possible to use this attribute to map a model to an existing config section inside e.g. appsettings.json file.

      ```c#
      [Configuration(nameof(AppVersion))]
      public class AppVersion
      {
          public int Major { get; set; }
          public int Minor { get; set; }
          public int Patch { get; set; }
      }
      ```

      

    * **Singleton**

      Also, it is possible to use **[Singleton]** attribute to register singleton services.

      ```c#
      [Singleton]
      public class SingletonService
      {
      	//Implementation of the SingletonService
      }
      ```

      ```c#
      [Singleton(typeof(IContractedSingletonService))]
      public class ContractedSingletonService : IContractedSingletonService
      {
      	//Implementation of the ContractedSingletonService which implements IContrctedSingletonService
      }
      ```

      

    * **Scoped**

      Just like above.

      ```C#
      [Scoped]
      public class ScopedService
      {
      	//Implementation of the ScopedService
      }
      ```

      ```c#
      [Singleton(typeof(IContractedScopedService))]
      public class ContractedScopedService : IContractedScopedService
      {
      	//Implementation of the ContractedScopedService which implements IContrctedScopedService
      }
      ```

      

    * **Transient**

      Just like above.

      ```c#
      [Scoped]
      public class TransientService
      {
      	//Implementation of the TransientService
      }
      ```

      ```c#
      [Singleton(typeof(ITransientScopedService))]
      public class ContractedTransientService : ITransientScopedService
      {
      	//Implementation of the ContractedTransientService which implements IContrctedTransientService
      }
      ```

      

    * **HostedService**

      To register background jobs using hosted services, easily we can use **[HostedService]** attribute. Look at the following exmple:

      ```c#
      [ServiceTypes.HostedService]
      public class HostedService : BackgroundService
      {
          protected override Task ExecuteAsync(CancellationToken stoppingToken)
          {
              return Task.CompletedTask;
          }
      }
      ```

Now that we got familiar with these attributes, the only thing remains is to call **AddContainer** method on a registry. take look at the following example again.					

```c#
using System.Reflection;
using Dinja;
using Dinja.Examples.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;

var registry = new Registry("appsettings.json")
    .AddContainer(Assembly.GetExecutingAssembly());
```

The **AddContainer** method searches the provided assembly for classes which are decorated with mentioned above and add them automatically to the registry.



# Integration with ASP.Net Core

Dinja also can be used alongside with ASP.Net Core. After placing attributes on top of service implementations, the method **AddContainer** can be called on **services** object inside **Startup.cs**. Look at this:

```c#
public void ConfigureServices(IServiceCollection services)
{
    services.AddContainer(Assembly.GetExecutingAssembly());    
    services.AddRazorPages();
    services.AddResponseCaching();
    services.AddControllers();
}
```



Also, it is possible to create a new class extending **Container** class and register it using following code.

```c#
services.AddContainer<MyContainer>(Configuration); //Configuration is a global variable inside Startup.cs
```



# Where can I get it?

First, [install NuGet](http://docs.nuget.org/docs/start-here/installing-nuget). Then, install **Dinja** from the package manager console:

```
PM> Install-Package Dinja
```

Or from the .NET CLI as:

```
dotnet add package Dinja
```