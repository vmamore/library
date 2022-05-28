namespace Library.Api.Infrastructure.Integrations.Events;

using System.Threading;
using System.Threading.Tasks;

public interface IEventDispatcher
{
    Task PublishAsync<TEvent>(TEvent @event, CancellationToken cancellationToken = default) where TEvent : class, IIntegrationEvent;
}
