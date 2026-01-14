# FlightControl - Startup Script
# This script starts all three services in separate terminals

Write-Host "🛫 Starting FlightControl Aviation Management System..." -ForegroundColor Cyan
Write-Host ""
Write-Host "⚠️  Please ensure no other services are running on ports 5000-5001" -ForegroundColor Yellow
Write-Host ""

# Start API on port 5000
Write-Host "Starting Flight API on http://localhost:5000..." -ForegroundColor Green
Start-Process pwsh -ArgumentList "-NoExit", "-Command", "cd '$PSScriptRoot\FlightControl.Api'; Write-Host '🛫 Flight API Starting...' -ForegroundColor Cyan; dotnet run"

Start-Sleep -Seconds 5

# Start Simulator (connects to API on port 5000)
Write-Host "Starting Flight Simulator..." -ForegroundColor Green
Start-Process pwsh -ArgumentList "-NoExit", "-Command", "cd '$PSScriptRoot\FlightControl.Sim'; Write-Host '✈️ Flight Simulator Starting...' -ForegroundColor Cyan; dotnet run"

Start-Sleep -Seconds 2

# Start Dashboard on port 5001
Write-Host "Starting Dashboard on http://localhost:5001..." -ForegroundColor Green
Start-Process pwsh -ArgumentList "-NoExit", "-Command", "cd '$PSScriptRoot\FlightControl.Dashboard'; Write-Host '📊 Dashboard Starting...' -ForegroundColor Cyan; dotnet run; Write-Host ''; Write-Host '✅ Dashboard is ready at http://localhost:5001' -ForegroundColor Green"

Write-Host ""
Write-Host "✅ All services are starting..." -ForegroundColor Cyan
Write-Host ""
Write-Host "📊 Dashboard: http://localhost:5001" -ForegroundColor Yellow
Write-Host "🔌 API: http://localhost:5000" -ForegroundColor Yellow
Write-Host ""
Write-Host "Wait 5-10 seconds for all services to start, then open:" -ForegroundColor White
Write-Host "  👉 http://localhost:5001" -ForegroundColor Cyan
Write-Host ""
Write-Host "Press any key to exit this window (services will continue running)..." -ForegroundColor Gray
$null = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown")
