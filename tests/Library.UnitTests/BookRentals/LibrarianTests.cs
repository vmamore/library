using FluentAssertions;
using Library.Api.Domain.BookRentals.Users;
using System;
using System.Linq;
using Xunit;
using static Library.Api.Domain.BookRentals.Users.Events.V1;

namespace Library.UnitTests.BookRentals
{
    public class LibrarianTests
    {
        [Fact]
        public void Should_Create_With_Success()
        {
            var librarian = Librarian.Create("Vinicius", "Mamoré", new DateTime(1997, 10, 25),
            "0001234567", "Campo Grande", "MS", "Rua Hehehe", "80");
            librarian.Should().NotBeNull();
            var librarianCreatedEvent = librarian.GetChanges().Last();
            librarianCreatedEvent.Should().BeOfType<LibrarianCreated>();
        }
    }
}
