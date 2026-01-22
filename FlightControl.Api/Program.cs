using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using FlightControl.ServiceDefaults;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddOpenApi();

// Add CORS for dashboard
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Simple in-memory store for now
builder.Services.AddSingleton<FlightStore>();

var app = builder.Build();

app.MapDefaultEndpoints();

app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// app.UseHttpsRedirection();

app.MapGet("/flights", (FlightStore store) => store.Flights.Values);

app.MapPost("/flights/update", ([FromBody] Flight flight, FlightStore store) => 
{
    store.Flights[flight.Id] = flight;
    return Results.Ok();
});

app.MapGet("/weather", () => 
{
    var rng = new Random();
    var conditions = new[] { "CLEAR", "CLOUDY", "RAIN", "FOGGY", "STORM" };
    var airports = new[] { "KJFK", "EGLL", "KLAX", "RJTT", "LFPG" };
    
    return airports.Select(code => new WeatherUpdate(
        code, 
        conditions[rng.Next(conditions.Length)], 
        Math.Round(10 + rng.NextDouble() * 20, 1), 
        rng.Next(0, 30), 
        rng.Next(0, 360)
    ));
});

app.Run();


public class FlightStore
{
    public ConcurrentDictionary<string, Flight> Flights { get; }

    public FlightStore()
    {
        var initialFlights = new Flight[]
        {
            new("FL101", "AA101", 40.7128, -74.0060, 30000, 500, 90, "EnRoute", "KJFK", "EGLL", new()),
            new("FL102", "BA202", 51.5074, -0.1278, 32000, 520, 270, "EnRoute", "EGLL", "KJFK", new()),
            new("FL103", "DL303", 34.0522, -118.2437, 35000, 510, 45, "EnRoute", "KLAX", "RJTT", new()),
            new("FL104", "UA404", 41.8781, -87.6298, 28000, 490, 180, "EnRoute", "KORD", "KMIA", new()),
            new("FL105", "AF505", 48.8566, 2.3522, 31000, 515, 135, "EnRoute", "LFPG", "OMDB", new())
        };

        Flights = new ConcurrentDictionary<string, Flight>(initialFlights.ToDictionary(f => f.Id));
    }
}

