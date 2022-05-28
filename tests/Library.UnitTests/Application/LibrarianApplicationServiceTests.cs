using FluentAssertions;
using Library.Api.Application.Librarians;
using Moq;
using System;
using System.Threading.Tasks;
using Library.Api.Domain.Users;
using Xunit;
using static Library.Api.Application.Librarians.Commands;

namespace Library.UnitTests.Application
{
    public class LibrarianApplicationServiceTests
    {
        private readonly Mock<ILibrarianRepository> repositoryMock;

        public LibrarianApplicationServiceTests() => repositoryMock = new();

        [Fact]
        public void Given_CreatingLibrarian_When_CommandIsValid_ThenShouldNotThrowException()
        {
            var registerLibrarianCommand = CreateRegistrationCommand();

            Func<Task> action = async () => await GetSut().Handle(registerLibrarianCommand);

            action.Should().NotThrowAsync();
            repositoryMock.Verify(r => r.Add(It.IsAny<Librarian>()), Times.Once);
            repositoryMock.Verify(r => r.Commit(), Times.Once);
        }

        private V1.RegisterLibrarian CreateRegistrationCommand() => new V1.RegisterLibrarian
        {
            FirstName = "Vinicius",
            LastName = "Mamoré",
            CPF = "00011122233",
            BirthDate = new DateTime(1997, 10, 25),
            Street = "Avenida Afonso Pena",
            City = "Campo Grande",
            Number = "12",
            District = "Tiradentes"
        };

        private LibrarianApplicationService GetSut() => new LibrarianApplicationService(repositoryMock.Object);
    }
}
