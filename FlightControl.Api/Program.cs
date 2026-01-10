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

app.UseHttpsRedirection();

app.MapGet("/flights", (FlightStore store) => store.Flights.Values);

app.MapPost("/flights/update", ([FromBody] Flight flight, FlightStore store) => 
{
    store.Flights[flight.Id] = flight;
    return Results.Ok();
});

app.Run();


public class FlightStore
{
    public ConcurrentDictionary<string, Flight> Flights { get; } = new();
}

