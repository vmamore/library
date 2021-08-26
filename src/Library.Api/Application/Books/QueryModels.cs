namespace Library.Api.Application.Books
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;

    public static class QueryModels
    {
        public class GetAllBooks { }

        public record GetBookById([FromRoute][Required] Guid id);
    }
}
