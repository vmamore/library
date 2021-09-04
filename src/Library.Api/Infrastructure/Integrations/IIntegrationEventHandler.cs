namespace Library.Api.Infrastructure.Integrations
{
    using Library.Api.Domain.Core;

    public interface IIntegrationEventHandler
    {
        Task Handle<T>(T @event) where T : DomainEvent;
    }
}
