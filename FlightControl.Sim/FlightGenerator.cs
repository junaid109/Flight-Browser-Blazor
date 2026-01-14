using System.Net.Http.Json;
using FlightControl.ServiceDefaults;

namespace FlightControl.Sim;

public class FlightGenerator(HttpClient httpClient, ILogger<FlightGenerator> logger)
{
    private readonly Random _random = new();
    private List<Flight> _flights = new();

    public async Task UpdateFlightsAsync(CancellationToken cancellationToken)
    {
        if (_flights.Count == 0)
        {
            InitializeFlights();
            logger.LogInformation("Initialized {Count} flights for simulation", _flights.Count);
        }

        var updatedFlights = new List<Flight>();

        foreach (var flight in _flights)
        {
            // Simple mock physics
            var newLat = flight.Latitude + (_random.NextDouble() - 0.5) * 0.05;
            var newLon = flight.Longitude + (_random.NextDouble() - 0.5) * 0.05;
            var newAlt = Math.Max(0, flight.Altitude + _random.Next(-50, 50));
            var newHeading = (flight.Heading + _random.Next(-5, 5)) % 360;
            
            var newHistory = new List<FlightCoordinate>(flight.History)
            {
                new FlightCoordinate(flight.Latitude, flight.Longitude, flight.Altitude, DateTime.UtcNow)
            };
            
            // Keep history manageable
            if (newHistory.Count > 50) newHistory.RemoveAt(0);

            var updatedFlight = flight with 
            { 
                Latitude = newLat, 
                Longitude = newLon, 
                Altitude = newAlt, 
                Heading = newHeading,
                History = newHistory 
            };

            updatedFlights.Add(updatedFlight);

            // Send update
            try 
            {
                 await httpClient.PostAsJsonAsync("/flights/update", updatedFlight, cancellationToken);
                 logger.LogDebug("Updated flight {FlightNumber} at ({Lat}, {Lon})", updatedFlight.FlightNumber, updatedFlight.Latitude, updatedFlight.Longitude);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to update flight {Id}", flight.Id);
            }
        }

        _flights = updatedFlights;
    }

    private void InitializeFlights()
    {
        _flights.Add(new Flight("FL101", "AA101", 40.7128, -74.0060, 30000, 500, 90, "EnRoute", "KJFK", "EGLL", new()));
        _flights.Add(new Flight("FL102", "BA202", 51.5074, -0.1278, 32000, 520, 270, "EnRoute", "EGLL", "KJFK", new()));
        _flights.Add(new Flight("FL103", "DL303", 34.0522, -118.2437, 35000, 510, 45, "EnRoute", "KLAX", "RJTT", new()));
        _flights.Add(new Flight("FL104", "UA404", 41.8781, -87.6298, 28000, 490, 180, "EnRoute", "KORD", "KMIA", new()));
        _flights.Add(new Flight("FL105", "AF505", 48.8566, 2.3522, 31000, 515, 135, "EnRoute", "LFPG", "OMDB", new()));
    }
}
