namespace Library.Api.Application.Rentals
{
    using System.Data.Common;
    using System.Threading.Tasks;
    using Infrastructure;
    using Infrastructure.BookRentals;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("rentals")]
    public class BookRentalsQueryApi(DbConnection connection) : Controller
    {
        [HttpGet]
        [Route("{locatorId}")]
        [Authorize(Roles = "locator,librarian")]
        public Task<IActionResult> GetRentalByLocator(QueryModels.GetRentalByLocator request)
            => RequestHandler.HandleQuery(() => connection.Query(request));

        [HttpGet]
        [Authorize(Roles = "librarian")]
        public Task<IActionResult> GetAllRentals(QueryModels.GetAllRentals request)
            => RequestHandler.HandleQuery(() => connection.Query(request));
    }
}
