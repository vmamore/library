namespace Library.Api.Application.Rentals
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Library.Api.Application.Core;

    public static class Commands
    {
        public static class V1
        {
            public record RentBooks([Required] Guid[] BooksId, [Required] Guid PersonId, [Required] Guid LibrarianId, [Required] Guid LocatorId) : ICommand;
            public record ReturnBookRental([Required] Guid BookRentalIdId, [Required] string Condition, [Required] Guid LibrarianId) : ICommand;
        }
    }
}

