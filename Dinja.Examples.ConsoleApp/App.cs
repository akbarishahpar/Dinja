using Dinja.Examples.ConsoleApp.IPv4DetectorService;

namespace Dinja.Examples.ConsoleApp;

public class App
{
    private readonly AppInfo _appInfo;
    private readonly IIPv4DetectorService _iPv4DetectorService;

    public App(AppInfo appInfo, IIPv4DetectorService iPv4DetectorService)
    {
        _appInfo = appInfo;
        _iPv4DetectorService = iPv4DetectorService;
    }

    public async Task Start()
    {
        Console.WriteLine(_appInfo.Name);
        Console.WriteLine(await _iPv4DetectorService.GetIPv4Async());
    }
}