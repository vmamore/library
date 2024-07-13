using Library.Api.Application.Rentals;
using Library.Api.Application.Shared;
using Library.Api.Domain.BookRentals;
using Library.Api.Domain.Shared;
using static Library.Api.Application.Rentals.Commands;

namespace Library.Tests.UnitTests.Application
{
    public class BookRentalApplicationServiceTests
    {
        private readonly Mock<IBookRentalRepository> _bookRentalRepositoryMock;
        private readonly Mock<ILocatorRepository> _locatorRepositoryMock;
        private readonly Mock<IHolidayClient> _holidayClientMock;
        private readonly Mock<ISystemClock> _clockMock;

        public BookRentalApplicationServiceTests()
        {
            _bookRentalRepositoryMock = new();
            _locatorRepositoryMock = new();
            _holidayClientMock = new();
            _clockMock = new();
        }

        [Fact]
        public void Given_CreatingRental_When_CommandIsValid_ThenShouldNotThrowException()
        {
            var locator = Locator.Create("Vinicius", "Mamoré", new DateTime(1997, 10, 25),
            "00011122233", "Campo Grande", "MS", "Rua Hehehe", "80");

            var books = new List<Book>()
            {
                Book.Create("Fight Club", "Chuck Palahniuk", "https://photo"),
                Book.Create("Fight Club 2", "Chuck Palahniuk", "https://photo"),
                Book.Create("Do not talk about Fight Club", "Chuck Palahniuk", "https://photo"),
            };

            var rentBooksCommand = CreateBooksRentCommand(books, locator);

            _locatorRepositoryMock.Setup(l => l.Load(It.IsAny<Guid>()))
                .ReturnsAsync(locator);

            _bookRentalRepositoryMock.Setup(b => b.LoadBooks(It.IsAny<Guid[]>()))
                .ReturnsAsync(books);

            _holidayClientMock.Setup(c => c.GetNextBusinessDate())
                .ReturnsAsync(DateTime.Now);


            Func<Task> action = async () => await GetSut().Handle(rentBooksCommand);

            action.Should().NotThrowAsync();
            _bookRentalRepositoryMock.Verify(r => r.Add(It.IsAny<BookRental>()), Times.Once);
            _bookRentalRepositoryMock.Verify(r => r.Commit(), Times.Once);
        }

        private V1.RentBooks CreateBooksRentCommand(List<Book> books, Locator locator) =>
            new(books.Select(b => b.Id).ToArray(), locator.Id);

        private BookRentalApplicationService GetSut() => new(_bookRentalRepositoryMock.Object,
            _locatorRepositoryMock.Object,
            _holidayClientMock.Object,
            _clockMock.Object);
    }
}
