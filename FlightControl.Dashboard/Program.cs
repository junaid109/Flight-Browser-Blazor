using FlightControl.Dashboard.Components;
using FlightControl.Dashboard;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Configure HttpClient with fallback for manual startup
var apiUrl = builder.Configuration["services:api:http:0"] ?? "http://localhost:5000";
builder.Services.AddHttpClient<FlightApiClient>(client => 
{
    client.BaseAddress = new Uri(apiUrl);
    client.Timeout = TimeSpan.FromSeconds(30);
});

var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
