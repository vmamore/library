namespace Library.Api.Infrastructure.Integrations
{
    using Domain.Shared.Core;

    public interface IIntegrationEventMapper
    {
        IIntegrationEvent Map<T>(T @event) where T : IDomainEvent;
    }
}
