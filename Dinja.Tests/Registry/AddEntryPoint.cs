namespace Dinja.Tests.Registry;

[TestFixture]
public class AddEntryPoint : TestFixtureBase
{
    [Test]
    public void Should_run_entry_point_with_injected_dependencies()
    {
        Registry
            .AddConfiguration<Models.AppVersion>()
            .AddEntryPoint<App>(program => program.EntryPoint());
    }
    
    // ReSharper disable once ClassNeverInstantiated.Local
    private class App
    {
        private readonly Models.AppVersion _appVersion;

        public App(Models.AppVersion appVersion)
        {
            _appVersion = appVersion;
        }

        public void EntryPoint()
        {
            Assert.Multiple(() =>
            {
                Assert.That(_appVersion, Is.Not.Null);
                Assert.That(_appVersion.Major, Is.EqualTo(1));
                Assert.That(_appVersion.Minor, Is.EqualTo(0));
                Assert.That(_appVersion.Patch, Is.EqualTo(0));
            });
        }
    }
}