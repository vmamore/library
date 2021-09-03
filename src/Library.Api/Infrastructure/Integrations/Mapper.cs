namespace Library.Api.Infrastructure.Integrations
{
    using Library.Api.Application.Inventories;
    using Library.Api.Domain.Core;
    using static Library.Api.Domain.Inventory.Events.V1;

    public class Mapper : IIntegrationEventsMapper
    {
        private readonly Dictionary<Type, Type> _types = new Dictionary<Type, Type>()
    {
        {typeof(BookRegistered), typeof(IntegrationEvents.BookRegistered) }
    };

        private readonly IIntegrationEventHandler _handler;

        public Mapper(IIntegrationEventHandler handler) => this._handler = handler;


        public async Task Handle(IEnumerable<DomainEvent> events)
        {
            foreach (var @event in events)
            {
                if (_types.ContainsKey(@event.GetType()) == false)
                    continue;

                await Dispatcher.HandleIntegrationEvent(@event, _handler.Handle);
            }
        }
    }
}
