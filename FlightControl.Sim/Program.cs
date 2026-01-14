using FlightControl.Sim;

var builder = Host.CreateApplicationBuilder(args);

builder.AddServiceDefaults();

// Configure HttpClient with Aspire service discovery
builder.Services.AddHttpClient<FlightGenerator>(client => 
{
    // "api" matches the service name defined in AppHost
    client.BaseAddress = new Uri("https+http://api");
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
