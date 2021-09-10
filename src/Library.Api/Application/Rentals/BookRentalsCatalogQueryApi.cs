namespace Library.Api.Application.Rentals
{
    using System.Data.Common;
    using System.Threading.Tasks;
    using Library.Api.Infrastructure;
    using Library.Api.Infrastructure.BookRentals;
    using Microsoft.AspNetCore.Mvc;

    [Route("rentals/books")]
    public class BookRentalsCatalogQueryApi : Controller
    {
        private readonly DbConnection _connection;

        public BookRentalsCatalogQueryApi(DbConnection connection) => _connection = connection;

        [HttpGet]
        [Route("all")]
        public Task<IActionResult> Get(QueryModels.GetAllBooks request)
            => RequestHandler.HandleQuery(() => _connection.Query(request));
    }
}
