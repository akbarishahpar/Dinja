namespace Dinja.Examples.ConsoleApp.IPv4DetectorService;

public interface IIPv4DetectorService
{
    Task<string> GetIPv4Async();
}