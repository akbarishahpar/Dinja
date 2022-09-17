namespace Dinja.Tests.Registry.Models;

public class AppVersion : IAppVersion
{
    public int Major { get; set; }
    public int Minor { get; set; }
    public int Patch { get; set; }
}