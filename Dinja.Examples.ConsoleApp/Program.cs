// See https://aka.ms/new-console-template for more information

using System.Reflection;
using Dinja;
using Dinja.Examples.ConsoleApp;
using Microsoft.Extensions.DependencyInjection;

var registry = new Registry("appsettings.json")
    .RegisterByExtensionMethod(s => s.AddHttpClient())
    .AddContainer(Assembly.GetExecutingAssembly());

await registry.AddEntryPointAsync<App>(async app => await app.Start());