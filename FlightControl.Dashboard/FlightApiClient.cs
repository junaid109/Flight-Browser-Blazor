using System.Net.Http.Json;
using FlightControl.ServiceDefaults;

namespace FlightControl.Dashboard;

public class FlightApiClient(HttpClient httpClient)
{
    public async Task<Flight[]> GetFlightsAsync(CancellationToken cancellationToken = default)
    {
        return await httpClient.GetFromJsonAsync<Flight[]>("/flights", cancellationToken) ?? Array.Empty<Flight>();
    }
}
