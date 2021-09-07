namespace Library.Api.Application.Rentals
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;

    public static class QueryModels
    {
        public record GetAllBooks([FromQuery] int page = 1) { }

        public record GetBookById([FromRoute][Required] Guid Id);
    }
}
