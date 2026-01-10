using FlightControl.Sim;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

// Configure HttpClient with fallback for manual startup
var apiUrl = builder.Configuration["services:api:http:0"] ?? "http://localhost:5000";
builder.Services.AddHttpClient<FlightGenerator>(client => 
{
    client.BaseAddress = new Uri(apiUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
