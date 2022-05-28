namespace Library.Api.Infrastructure.Integrations
{
    using Domain.Shared.Core;
    using BookRegisteredDomain = Domain.Inventory.Events.V1.BookRegistered;

    public class IntegrationEventMapper : IIntegrationEventMapper
    {
        public IIntegrationEvent Map<T>(T @event) where T : IDomainEvent
            => @event switch
            {
                BookRegisteredDomain e => new BookRegistered
                {
                    Title = e.Title, Author = e.Author, PhotoUrl = e.PhotoUrl
                },
                _ => null
            };
    }
}
