var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.FlightControl_Api>("api")
    .WithHttpHealthCheck("/health");

builder.AddProject<Projects.FlightControl_Sim>("sim")
    .WithReference(api)
    .WaitFor(api);

builder.AddProject<Projects.FlightControl_Dashboard>("dashboard")
    .WithExternalHttpEndpoints()
    .WithReference(api)
    .WaitFor(api);

builder.Build().Run();
