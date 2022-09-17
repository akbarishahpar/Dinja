using Dinja.ServiceTypes;

namespace Dinja.Examples.ConsoleApp.IPv4DetectorService;

[Singleton(typeof(IIPv4DetectorService))]
public class IPv4DetectorService : IIPv4DetectorService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public IPv4DetectorService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }
    
    public async Task<string> GetIPv4Async()
    {
        using var client = _httpClientFactory.CreateClient();
        return await client.GetStringAsync("https://api.ipify.org/");
    }
}