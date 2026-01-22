# FlightControl - Real-Time Aviation Management System

A professional-grade .NET Aspire application that simulates and displays real-time flight data with an industry-standard airport manager dashboard.

## 🚀 Architecture

This solution demonstrates a modern microservices architecture using .NET Aspire:

```
┌─────────────────────────────────────────────────────────┐
│              FlightControl.AppHost                      │
│         (Aspire Orchestration Layer)                    │
└─────────────────────────────────────────────────────────┘
                          │
        ┌─────────────────┼─────────────────┐
        │                 │                 │
        ▼                 ▼                 ▼
┌──────────────┐  ┌──────────────┐  ┌──────────────┐
│ Dashboard    │  │  Flight API  │  │  Simulator   │
│  (Blazor)    │──│  (Web API)   │◄─│  (Worker)    │
└──────────────┘  └──────────────┘  └──────────────┘
        │                 │                 │
        └─────────────────┴─────────────────┘
                          │
              ┌───────────────────────┐
              │  ServiceDefaults      │
              │  (Shared Services)    │
              └───────────────────────┘
```
<img width="1913" height="860" alt="image" src="https://github.com/user-attachments/assets/5cf41fc4-8aca-4eb7-93c7-f09a424803da" />

### Projects

- **FlightControl.AppHost**: Aspire orchestrator that manages all services
- **FlightControl.Api**: REST API exposing flight data endpoints
- **FlightControl.Dashboard**: Blazor Server interactive dashboard with real-time radar visualization
- **FlightControl.Sim**: Background worker that generates realistic flight telemetry
- **FlightControl.ServiceDefaults**: Shared configuration for OpenTelemetry, Service Discovery, and Resilience

## ✨ Features

### Real-Time Flight Simulation
- 5 mock flights with realistic telemetry updates every second
- Dynamic position, altitude, speed, and heading changes
- Automatic data synchronization across services

### Professional Dashboard
- **Live Radar Display**: Visual representation of aircraft positions
- **Flight List**: Real-time status of all active flights
- **Telemetry Tracking**: Click any flight to view detailed coordinates
- **Weather Stations**: Mock weather data for major airports
- **System Statistics**: Traffic density and operational metrics

### Enterprise-Grade Infrastructure
- **Service Discovery**: Automatic service-to-service communication
- **OpenTelemetry**: Distributed tracing, metrics, and logging
- **Resilience Patterns**: Retry policies and circuit breakers
- **Health Checks**: Liveness and readiness endpoints

## 🎨 Dashboard Preview

The dashboard features:
- Dark aviation-themed UI with blue accent colors
- Grid-based radar overlay
- Animated plane markers with rotation based on heading
- HUD-style information panels
- Real-time updates without page refresh

## 🛠️ Prerequisites

- .NET 10.0 SDK or later
- Visual Studio 2022 or VS Code
- (Optional) Docker Desktop for containerized dependencies

## 🚀 Getting Started

### Option 1: Run with Aspire Dashboard (Recommended)

**Note**: The Aspire workload is deprecated in .NET 10. For production use, consider using Docker Compose or Kubernetes for orchestration. However, the application can still run standalone.

```powershell
# Run individual services
cd FlightControl.Api
dotnet run

# In another terminal
cd FlightControl.Sim
dotnet run

# In another terminal
cd FlightControl.Dashboard
dotnet run
```

### Option 2: Run Services Individually

1. **Start the API**:
```powershell
cd FlightControl.Api
dotnet run
```
The API will be available at `http://localhost:5000` (or check console output)

2. **Start the Simulator**:
```powershell
cd FlightControl.Sim
dotnet run
```

3. **Start the Dashboard**:
```powershell
cd FlightControl.Dashboard
dotnet run
```
Open your browser to the URL shown in the console (typically `http://localhost:5001`)

## 📡 API Endpoints

### GET /flights
Returns all active flights with current telemetry.

**Response**:
```json
[
  {
    "id": "FL101",
    "flightNumber": "AA101",
    "latitude": 40.7128,
    "longitude": -74.0060,
    "altitude": 30000,
    "speed": 500,
    "heading": 90,
    "status": "EnRoute"
  }
]
```

### POST /flights/update
Updates flight telemetry (used by simulator).

**Request Body**:
```json
{
  "id": "FL101",
  "flightNumber": "AA101",
  "latitude": 40.7128,
  "longitude": -74.0060,
  "altitude": 30000,
  "speed": 500,
  "heading": 90,
  "status": "EnRoute"
}
```

## 🔧 Configuration

### Service Discovery
Services communicate using named endpoints:
- API: `http://api`
- Dashboard: `http://dashboard`

### Telemetry
OpenTelemetry exports to OTLP endpoint if configured:
```bash
OTEL_EXPORTER_OTLP_ENDPOINT=http://localhost:4317
```

## 🎯 Future Enhancements

- [ ] Add SignalR for real-time push updates
- [ ] Implement flight path history and predictions
- [ ] Add weather API integration
- [ ] Implement airport runway management
- [ ] Add flight scheduling and conflict detection
- [ ] Integrate with real aviation data sources (ADS-B)
- [ ] Add authentication and role-based access
- [ ] Implement data persistence with Entity Framework Core
- [ ] Add Docker Compose configuration
- [ ] Create Kubernetes deployment manifests

## 📊 Monitoring

Access health checks:
- API: `http://localhost:5000/health`
- Dashboard: `http://localhost:5001/health`

## 🏗️ Development

### Adding New Flights
Edit `FlightControl.Sim/FlightGenerator.cs`:
```csharp
private void InitializeFlights()
{
    _flights.Add(new Flight("FL106", "EK606", 25.2532, 55.3657, 38000, 550, 315, "EnRoute"));
}
```

### Customizing Dashboard
- **Styles**: Edit `FlightControl.Dashboard/wwwroot/app.css`
- **Layout**: Modify `FlightControl.Dashboard/Components/Pages/Home.razor`
- **Update Frequency**: Change the timer interval in `Home.razor` (currently 1000ms)

## 📝 License

This is a demonstration project for educational purposes.

## 🤝 Contributing

This project demonstrates modern .NET development practices including:
- Microservices architecture
- Real-time data processing
- Interactive web UIs with Blazor
- Observability and monitoring
- Service-to-service communication

Feel free to fork and extend with additional features!
