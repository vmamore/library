using Library.Api.Domain.Core;

namespace Library.Api.Application.Inventories;

public interface IIntegrationEventsMapper
{
    Task Handle(IEnumerable<DomainEvent> @events);
}