namespace Dinja.Tests.Registry;

public abstract class TestFixtureBase
{
    protected Dinja.Registry Registry;

    [SetUp]
    public void SetUp()
    {
        Registry = new Dinja.Registry("appsettings.json");
    }
}