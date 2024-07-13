using Library.Api.Application.Inventories;
using Library.Api.Application.Shared;
using Library.Api.Domain.Inventory;
using static Library.Api.Application.Inventories.Commands;

namespace Library.Tests.UnitTests.Application
{
    public class BookApplicationServiceTests
    {
        private readonly Mock<IBookRepository> repositoryMock;
        private readonly Mock<IDispatcher> dispatcher;

        public BookApplicationServiceTests()
        {
            repositoryMock = new();
            dispatcher = new();
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

        private BookApplicationService GetSut() => new(repositoryMock.Object, dispatcher.Object);
    }
}
