﻿namespace Dinja.Tests.Registry;

[TestFixture]
public class AddEntryPoint
{
    private Dinja.Registry _registry;

    [SetUp]
    public void SetUp()
    {
        _registry = new Dinja.Registry("appsettings.json");
    }

    
    [Test]
    public void Should_run_entry_point_with_injected_dependencies()
    {
        _registry
            .AddConfiguration<Models.Version>()
            .AddEntryPoint<App>(program => program.Main());
    }
    
    class App
    {
        private readonly Models.Version _version;

        public App(Models.Version version)
        {
            _version = version;
        }
        
        public void Main()
        {
            Console.WriteLine(_version.Major);
        }
    }
}