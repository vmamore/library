
namespace Library.Api.Application.Rentals
{
    using System;
    using System.Threading.Tasks;
    using Core;
    using Domain.BookRentals;
    using Domain.BookRentals.Exceptions;
    using Domain.Shared;
    using Shared;
    using static Commands;

    public class BookRentalApplicationService : IApplicationService
    {
        private readonly IBookRentalRepository _bookRentalRepository;
        private readonly ILocatorRepository _locatorRepository;
        private readonly IHolidayClient _holidayClient;
        private readonly ISystemClock _clock;

        public BookRentalApplicationService(
            IBookRentalRepository bookRentalRepository,
            ILocatorRepository locatorRepository,
            IHolidayClient holidayClient,
            ISystemClock clock)
        {
            _bookRentalRepository = bookRentalRepository;
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

            if (locator.IsPenalized()) throw new LocatorIsPenalized(locator.ActivePenalty.EndDate);

            var activeBookRental = await this._bookRentalRepository.GetActive(command.LocatorId);

            if (activeBookRental != null) throw new LocatorHasActiveRental(activeBookRental.RentedDay);

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
