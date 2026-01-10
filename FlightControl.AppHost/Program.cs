var builder = DistributedApplication.CreateBuilder(args);

var api = builder.AddProject<Projects.FlightControl_Api>("api");

builder.AddProject<Projects.FlightControl_Sim>("sim")
    .WithReference(api);

builder.AddProject<Projects.FlightControl_Dashboard>("dashboard")
    .WithReference(api)
    .WithExternalHttpEndpoints();

builder.Build().Run();
