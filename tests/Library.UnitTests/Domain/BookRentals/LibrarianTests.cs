using FluentAssertions;
using System;
using System.Linq;
using Library.Api.Domain.Users;
using Xunit;
using static Library.Api.Domain.Users.Events.V1;

namespace Library.UnitTests.Domain.BookRentals
{
    public class LibrarianTests
    {
        [Fact]
        public void Should_Create_With_Success()
        {
            var librarian = Librarian.Create("Vinicius", "Mamoré", new DateTime(1997, 10, 25),
            "00011122233", "Campo Grande", "MS", "Rua Hehehe", "80");
            librarian.Should().NotBeNull();
            var librarianCreatedEvent = librarian.GetChanges().Last();
            librarianCreatedEvent.Should().BeOfType<LibrarianCreated>();
        }

        [Theory]
        [InlineData(null, "Vinicius", "Mamoré")]
        [InlineData("0001234567", null, "Mamoré")]
        [InlineData("0001234567", "Vinicius", null)]
        public void Should_Throw_Exception_When_Passing_Invalid_Value(string cpf, string firstName, string lastName)
        {
            var action = () => Librarian.Create(firstName, lastName, new DateTime(1997, 10, 25),
            cpf, "Campo Grande", "MS", "Rua Hehehe", "80");
            action.Should().Throw<Exception>();
        }
    }
}
