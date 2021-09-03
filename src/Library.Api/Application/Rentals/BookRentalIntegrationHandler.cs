
namespace Library.Api.Application.Rentals
{
    using System.Threading.Tasks;
    using Library.Api.Domain.BookRentals;
    using Library.Api.Domain.Core;
    using static Library.Api.Infrastructure.Integrations.IntegrationEvents;

    public class BookRentalIntegrationHandler : IBookRentalIntegrationHandler<BookRegistered>
    {
        private readonly IBookRentalRepository _repository;

        public BookRentalIntegrationHandler(IBookRentalRepository repository) => _repository = repository;

        public async Task Handle(BookRegistered bookRegisteredEvent)
        {
            var newBook = Book.Create(bookRegisteredEvent.Title, bookRegisteredEvent.Author);

            await _repository.Add(newBook);

            await _repository.Commit();
        }
    }

    public interface IBookRentalIntegrationHandler<T> where T : IntegrationEvent
    {
        Task Handle(T @event);
    }
}
