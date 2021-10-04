namespace Library.Api.Infrastructure.Integrations
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;

    public class IntegrationService : BackgroundService
    {
        private readonly BackgroundWorkerQueue queue;

        public IntegrationService(BackgroundWorkerQueue queue) => this.queue = queue;

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await this.queue.DequeueAsync(stoppingToken);

                await workItem(stoppingToken);
            }
        }
    }
}
