namespace Library.Api.Application.Inventories
{
    using System.ComponentModel.DataAnnotations;
    using Library.Api.Application.Core;

    public static class Commands
    {
        public static class V1
        {
            public record RegisterBook([Required] string Title, [Required] string Author, [Required] string ReleasedYear, [Required] string ISBN, int Pages, int Version) : ICommand;
        }
    }
}
