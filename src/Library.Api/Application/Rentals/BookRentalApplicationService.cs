
namespace Library.Api.Application.Rentals
{
    using System;
    using System.Threading.Tasks;
    using Domain.BookRentals;
    using Domain.BookRentals.Exceptions;
    using Domain.Shared;
    using Shared;
    using static Commands;

    public class BookRentalApplicationService(
        IBookRentalRepository bookRentalRepository,
        ILocatorRepository locatorRepository,
        IHolidayClient holidayClient,
        ISystemClock clock)
        : IApplicationService
    {
        public Task Handle(ICommand command) => command switch
        {
            V1.RentBooks cmd => this.HandleCreate(cmd),
            V1.ReturnBookRental cmd => this.HandleUpdate(cmd.BookRentalId, c => c.Returned(clock)),
            _ => Task.CompletedTask
        };

        private async Task HandleCreate(V1.RentBooks command)
        {
            var locator = await locatorRepository.Load(command.LocatorId);

            if (locator.IsPenalized())
            {
                throw new LocatorIsPenalized(locator.ActivePenalty.EndDate);
            }

            var activeBookRental = await bookRentalRepository.GetActive(command.LocatorId);

            if (activeBookRental != null)
            {
                throw new LocatorHasActiveRental(activeBookRental.RentedDay);
            }

            var books = await bookRentalRepository.LoadBooks(command.BooksId);

            var date = await holidayClient.GetNextBusinessDate();

            var bookRental = BookRental.Create(locator, books, date);

            await bookRentalRepository.Add(bookRental);

            await bookRentalRepository.Commit();
        }

        private async Task HandleUpdate(
            Guid bookRentalId,
            Action<BookRental> operation
        )
        {
            var bookRental = await bookRentalRepository
                .Load(bookRentalId);

            if (bookRental == null)
            {
                throw new InvalidOperationException(
                    $"Entity with id {bookRentalId} cannot be found"
                );
            }

            operation(bookRental);

            await bookRentalRepository.Commit();
        }
    }
}
