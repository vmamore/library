namespace Library.Api.Infrastructure.Integrations.Events;

using System.Threading.Tasks;

public interface IIntegrationEventHandler<in TEvent> where TEvent : class, IIntegrationEvent
{
    Task HandleAsync(TEvent @event);
}
