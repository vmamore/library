namespace Library.Api.Application.Rentals
{
    using System.Data.Common;
    using System.Threading.Tasks;
    using Infrastructure;
    using Infrastructure.BookRentals;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("rentals/books")]
    public class BookRentalsCatalogQueryApi(DbConnection connection) : Controller
    {
        [HttpGet]
        [Route("all")]
        [Authorize(Roles = "locator,librarian")]
        public Task<IActionResult> Get(QueryModels.GetAllBooks request)
            => RequestHandler.HandleQuery(() => connection.Query(request));
    }
}
