namespace Library.Api.Application.Inventories
{
    using System.ComponentModel.DataAnnotations;
    using Shared;

    public static class Commands
    {
        public static class V1
        {
            public record RegisterBook([Required] string Title, [Required] string Author, [Required] string ReleasedYear, [Required] string ISBN, string PhotoUrl, int Pages, int Version) : ICommand;
        }
    }
}
