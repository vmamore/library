namespace Library.Api.Infrastructure.Integrations
{
    using System.Threading.Tasks;
    using Library.Api.Domain.Core;

    public interface IIntegrationEventHandler
    {
        Task Handle<T>(T @event) where T : DomainEvent;
    }
}
