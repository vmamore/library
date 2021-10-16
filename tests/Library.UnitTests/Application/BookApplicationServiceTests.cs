using FluentAssertions;
using Library.Api.Application.Inventories;
using Library.Api.Application.Librarians;
using Library.Api.Application.Locators;
using Library.Api.Application.Shared;
using Library.Api.Domain.BookRentals;
using Library.Api.Domain.BookRentals.Users;
using Library.Api.Domain.Inventory;
using Library.Api.Infrastructure.Integrations;
using Moq;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using static Library.Api.Application.Inventories.Commands;

namespace Library.UnitTests.Application
{
    public class BookApplicationServiceTests
    {
        private readonly Mock<IBookRepository> repositoryMock;
        private readonly Mock<IIntegrationEventsMapper> integrationEventsMapperMock;

        public BookApplicationServiceTests()
        {
            repositoryMock = new();
            integrationEventsMapperMock = new();
        }

        [Fact]
        public void Given_CreatingBook_When_CommandIsValid_ThenShouldNotThrowException()
        {
            var registerBookCommand = CreateBookRegistrationCommand();

            Func<Task> action = async () => await GetSut().Handle(registerBookCommand);

            action.Should().NotThrowAsync();
            repositoryMock.Verify(r => r.Add(It.IsAny<Api.Domain.Inventory.Book>()), Times.Once);
            repositoryMock.Verify(r => r.Commit(), Times.Once);
        }

        private V1.RegisterBook CreateBookRegistrationCommand() =>
            new("Clube da Luta", "Chuck Palaniuhk", "2005", "111222333444555", "www.photo.com/clube-da-luta", 205, 3);

        private BookApplicationService GetSut() => new BookApplicationService(repositoryMock.Object, integrationEventsMapperMock.Object, new BackgroundWorkerQueue());
    }
}
