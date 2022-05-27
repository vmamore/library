namespace Library.Api.Infrastructure.BackgroundJobs;

using System;
using System.Threading;
using System.Threading.Tasks;
using Integrations;
using Integrations.Events;
using Microsoft.Extensions.Hosting;

public sealed class MessageProcessor : BackgroundService
{
    private readonly MessagesChannel channel;
    private readonly IIntegrationEventMapper integrationEventMapper;
    private readonly IServiceProvider serviceProvider;
    private readonly IEventDispatcher eventDispatcher;

    public MessageProcessor(MessagesChannel channel, IIntegrationEventMapper integrationEventMapper, IServiceProvider serviceProvider, IEventDispatcher eventDispatcher)
    {
        this.channel = channel;
        this.integrationEventMapper = integrationEventMapper;
        this.serviceProvider = serviceProvider;
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

            await eventDispatcher.PublishAsync(integrationEvent);
        }
    }
}
