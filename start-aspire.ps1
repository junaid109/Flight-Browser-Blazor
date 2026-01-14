# Start FlightControl with Aspire Orchestration
Write-Host "🚀 Starting FlightControl with Aspire..." -ForegroundColor Cyan
Write-Host "⚠️  Please ensure you have stopped the previous 'start.ps1' session to avoid port conflicts!" -ForegroundColor Yellow
Write-Host ""
dotnet run --project FlightControl.AppHost/FlightControl.AppHost.csproj
