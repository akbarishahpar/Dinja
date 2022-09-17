using Dinja.ServiceTypes;

namespace Dinja.Examples.ConsoleApp.IPv4DetectorService;

[Configuration(nameof(AppInfo))]
public class AppInfo
{
    public string Name { get; set; }
}