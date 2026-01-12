using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InternshipEx.Modules.Auth.Persistence.Outbox
{
    public class OutboxBackgroundService(
        IServiceScopeFactory serviceScopeFactory,
        ILogger<OutboxBackgroundService> logger) : BackgroundService
    {
        private const int OutboxProxessorFrequency = 1000000;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                logger.LogInformation("Outbox background service is starting.");
                while (!stoppingToken.IsCancellationRequested)
                {
                    using var scope = serviceScopeFactory.CreateScope();
                    var outboxProcessor = scope.ServiceProvider.GetRequiredService<OutboxProcessor>();

                    await outboxProcessor.Execute(stoppingToken);

                    //simulate
                    await Task.Delay(TimeSpan.FromSeconds(OutboxProxessorFrequency), stoppingToken);
                }
                logger.LogInformation("Outbox background service is stopping.");
            }
            catch (OperationCanceledException)
            {
                logger.LogInformation("OutboxBackgroundService cancelled.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred in OutboxBackgroundService");
            }
            finally
            {
                logger.LogInformation("OutboxBackgroundService finished...");
            }
        }
    }
}
