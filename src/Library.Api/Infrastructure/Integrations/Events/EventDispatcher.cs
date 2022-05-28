namespace Library.Api.Infrastructure.Integrations.Events;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

internal sealed class EventDispatcher : IEventDispatcher
{
    private readonly IServiceProvider _serviceProvider;

    public EventDispatcher(IServiceProvider serviceProvider)
        => _serviceProvider = serviceProvider;

    public async Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default)
        where TEvent : class, IIntegrationEvent
    {
        using var scope = _serviceProvider.CreateScope();
        var handlerType = typeof(IIntegrationEventHandler<>).MakeGenericType(@event.GetType());
        var handlers = scope.ServiceProvider.GetServices(handlerType);
        var tasks = handlers.Select(x => (Task) handlerType
            .GetMethod(nameof(IIntegrationEventHandler<TEvent>.HandleAsync))
            ?.Invoke(x, new object[] {@event}));
        await Task.WhenAll(tasks);
    }
}
