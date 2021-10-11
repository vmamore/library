namespace Library.Api.Application.Rentals
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Mvc;

    public static class QueryModels
    {
        public record GetAllBooks([FromQuery] int page = 1, [FromQuery] string title = null) { }

        public record GetBookById([FromRoute][Required] Guid Id);

        public record GetRentalByLocator([FromRoute][Required] Guid locatorId);

        public record GetAllRentals();
    }
}
