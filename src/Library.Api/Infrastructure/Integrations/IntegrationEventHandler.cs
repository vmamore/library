namespace Library.Api.Infrastructure.Integrations
{
    using System.Threading.Tasks;
    using Library.Api.Domain.Core;
    using static Library.Api.Domain.Inventory.Events.V1;
    using BookRentalsIntegrationEventHandler = BookRentals.IntegrationEventHandler;

    public class IntegrationEventHandler : IIntegrationEventHandler
    {
        private readonly BookRentalsIntegrationEventHandler _bookRentalsIntegrationEventHandler;

        public IntegrationEventHandler(BookRentalsIntegrationEventHandler bookRentalsIntegrationEventHandler)
            => _bookRentalsIntegrationEventHandler = bookRentalsIntegrationEventHandler;

        public async Task Handle<T>(T @event) where T : DomainEvent
        {
            switch (@event)
            {
                case BookRegistered e:
                    await _bookRentalsIntegrationEventHandler.Handle(
                        new IntegrationEvents.BookRegistered
                        {
                            Title = e.Title,
                            Author = e.Author,
                            PhotoUrl = e.PhotoUrl
                        });
                    return;
                default:
                    return;
            }
        }
    }
}
