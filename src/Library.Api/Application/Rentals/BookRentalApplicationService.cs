
namespace Library.Api.Application.Rentals
{
    using System;
    using System.Threading.Tasks;
    using Library.Api.Application.Core;
    using Library.Api.Application.Shared;
    using Library.Api.Domain.BookRentals;
    using Library.Api.Domain.Users;
    using static Library.Api.Application.Rentals.Commands;

    public class BookRentalApplicationService : IApplicationService
    {
        private readonly IBookRentalRepository _repository;
        private readonly ILibrarianRepository _librarianRepository;
        private readonly ILocatorRepository _locatorRepository;
        private readonly IHolidayClient _holidayClient;

        public BookRentalApplicationService(IBookRentalRepository repository, IHolidayClient holidayClient)
        {
            _repository = repository;
            _holidayClient = holidayClient;
        }

        public Task Handle(ICommand command) => command switch
        {
            V1.RentBooks cmd =>
                HandleCreate(cmd),
            V1.ReturnBookRental cmd =>
                HandleUpdate(cmd.BookRentalIdId, c => c.Returned(cmd.LibrarianId)),
            _ => Task.CompletedTask
        };

        private async Task HandleCreate(V1.RentBooks command)
        {
            var librarian = await _librarianRepository.Load(command.LibrarianId);

            var locator = await _locatorRepository.Load(command.LocatorId);

            var books = await _repository.LoadBooks(command.BooksId);

            var date = await _holidayClient.GetNextBusinessDate();

            var newBook = await BookRental.Create(librarian, locator, books, date);

            await _repository.Add(newBook);
        }

        private async Task HandleUpdate(
            Guid bookRentalId,
            Action<BookRental> operation
        )
        {
            var book = await _repository
                .Load(bookRentalId);

            if (book == null)
                throw new InvalidOperationException(
                    $"Entity with id {bookRentalId} cannot be found"
                );

            operation(book);
        }
    }
}
