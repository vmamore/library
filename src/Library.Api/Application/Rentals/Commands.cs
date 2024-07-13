namespace Library.Api.Application.Rentals
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Shared;

    public static class Commands
    {
        public static class V1
        {
            public record RentBooks([Required] Guid[] BooksId, [Required] Guid LocatorId) : ICommand;
            public record ReturnBookRental(Guid BookRentalId, [Required] string Condition, [Required] Guid LibrarianId) : ICommand;
        }
    }
}

