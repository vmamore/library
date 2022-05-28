namespace Library.Api.Infrastructure.BackgroundJobs;

using System.Threading;
using System.Threading.Tasks;
using Integrations;
using Integrations.Events;
using Microsoft.Extensions.Hosting;

public sealed class EventsProcessor : BackgroundService
{
    private readonly MessagesChannel channel;
    private readonly IIntegrationEventMapper integrationEventMapper;
    private readonly IEventDispatcher eventDispatcher;

    public EventsProcessor(MessagesChannel channel, IIntegrationEventMapper integrationEventMapper, IEventDispatcher eventDispatcher)
    {
        this.channel = channel;
        this.integrationEventMapper = integrationEventMapper;
        this.eventDispatcher = eventDispatcher;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await foreach (var message in this.channel.Reader.ReadAllAsync())
        {
            var integrationEvent = this.integrationEventMapper.Map(message);
            if (integrationEvent == null)
            {
                return;
            }

            await this.eventDispatcher.PublishAsync(integrationEvent);
        }
    }
}
