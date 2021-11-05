using FluentAssertions;
using Library.Api.Application.Rentals;
using Library.Api.Domain.BookRentals;
using Moq;
using System;
using System.Threading.Tasks;
using static Library.Api.Infrastructure.Integrations.IntegrationEvents;

namespace Library.UnitTests.Application
{
    internal class BookRentalIntegrationHandlerTests
    {
        private readonly Mock<IBookRentalRepository> _repositoryMock;

        public BookRentalIntegrationHandlerTests() => _repositoryMock = new();

        public void Given_BookRegisteredEvent_When_HandlingIntegrationEvent_Then_ShouldWithSuccess()
        {
            var bookRegisteredEvent = new BookRegistered
            {
                Title = "Fight Club",
                Author = "Chuck Palaniuk",
                PhotoUrl = "www.images.com/fight-club"
            };

            var sut = GetSut();

            Func<Task> action = () => sut.Handle(bookRegisteredEvent);

            action.Should().NotThrow<Exception>();
            _repositoryMock.Verify(r => r.Add(It.IsAny<Book>()), Times.Once());
            _repositoryMock.Verify(r => r.Commit(), Times.Once());
        }

        private BookRentalIntegrationHandler GetSut() => new BookRentalIntegrationHandler(_repositoryMock.Object);
    }
}
