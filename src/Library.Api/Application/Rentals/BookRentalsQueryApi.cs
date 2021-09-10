namespace Library.Api.Application.Rentals
{
    using System.Data.Common;
    using System.Threading.Tasks;
    using Library.Api.Infrastructure;
    using Library.Api.Infrastructure.BookRentals;
    using Microsoft.AspNetCore.Mvc;

    [Route("rentals")]
    public class BookRentalsQueryApi : Controller
    {
        private readonly DbConnection _connection;

        public BookRentalsQueryApi(DbConnection connection) => _connection = connection;

        [HttpGet]
        [Route("{locatorId}")]
        public Task<IActionResult> GetRentalByLocator(QueryModels.GetRentalByLocator request)
            => RequestHandler.HandleQuery(() => _connection.Query(request));
    }
}
