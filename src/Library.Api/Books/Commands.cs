namespace Library.Api.Books
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Library.Api.Core;

    public static class Commands
    {
        public static class V1
        {
            public record RegisterBook([Required] string Title, [Required] string Author, [Required] string ReleasedYear, int Pages, int Version) : ICommand;
            public record RentBook([Required] Guid BookId, [Required] Guid PersonId) : ICommand;
            public record ReturnBook([Required] Guid BookId, [Required] string condition) : ICommand;
        }
    }
}
