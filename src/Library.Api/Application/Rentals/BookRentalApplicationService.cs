
namespace Library.Api.Application.Rentals
{
    using System;
    using System.Threading.Tasks;
    using Library.Api.Application.Core;
    using Library.Api.Application.Shared;
    using Library.Api.Domain.BookRentals;
    using Library.Api.Domain.BookRentals.Users;
    using Library.Api.Domain.Shared;
    using static Library.Api.Application.Rentals.Commands;

    public class BookRentalApplicationService : IApplicationService
    {
        private readonly IBookRentalRepository _bookRentalRepository;
        private readonly ILibrarianRepository _librarianRepository;
        private readonly ILocatorRepository _locatorRepository;
        private readonly IHolidayClient _holidayClient;
        private readonly ISystemClock _clock;

        public BookRentalApplicationService(
            IBookRentalRepository bookRentalRepository,
            ILibrarianRepository librarianRepository,
            ILocatorRepository locatorRepository,
            IHolidayClient holidayClient,
            ISystemClock clock)
        {
            _bookRentalRepository = bookRentalRepository;
            _librarianRepository = librarianRepository;
            _locatorRepository = locatorRepository;
            _holidayClient = holidayClient;
            _clock = clock;
        }

        public Task Handle(ICommand command) => command switch
        {
            V1.RentBooks cmd =>
                HandleCreate(cmd),
            V1.ReturnBookRental cmd =>
                HandleUpdate(cmd.BookRentalId, c => c.Returned(_clock)),
            _ => Task.CompletedTask
        };

        private async Task HandleCreate(V1.RentBooks command)
        {
            var locator = await _locatorRepository.Load(command.LocatorId);

            var books = await _bookRentalRepository.LoadBooks(command.BooksId);

            var date = await _holidayClient.GetNextBusinessDate();

            var bookRental = BookRental.Create(locator, books, date);

            await _bookRentalRepository.Add(bookRental);

            await _bookRentalRepository.Commit();
        }

        private async Task HandleUpdate(
            Guid bookRentalId,
            Action<BookRental> operation
        )
        {
            var bookRental = await _bookRentalRepository
                .Load(bookRentalId);

            if (bookRental == null)
                throw new InvalidOperationException(
                    $"Entity with id {bookRentalId} cannot be found"
                );

            operation(bookRental);

            await _bookRentalRepository.Commit();
        }
    }
}
