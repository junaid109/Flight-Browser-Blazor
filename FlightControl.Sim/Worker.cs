namespace FlightControl.Sim;

public class Worker(FlightGenerator generator, ILogger<Worker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Flight Simulator Started.");
        
        while (!stoppingToken.IsCancellationRequested)
        {
            await generator.UpdateFlightsAsync(stoppingToken);
            await Task.Delay(1000, stoppingToken); 
        }
    }
}
