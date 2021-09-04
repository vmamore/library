namespace Library.Api.Application.Rentals
{
    using System.Data.Common;
    using System.Threading.Tasks;
    using Library.Api.Infrastructure;
    using Library.Api.Infrastructure.BookRentals;
    using Microsoft.AspNetCore.Mvc;

    [Route("rentals/books")]
    public class RentalsQueryApi : Controller
    {
        private readonly DbConnection _connection;

        public RentalsQueryApi(DbConnection connection) => _connection = connection;

        [HttpGet]
        [Route("all")]
        public Task<IActionResult> Get(QueryModels.GetAllBooks request)
            => RequestHandler.HandleQuery(() => _connection.Query(request));

        [HttpGet]
        [Route("{id}")]
        public Task<IActionResult> GetById(QueryModels.GetBookById request)
            => RequestHandler.HandleQuery(() => _connection.Query(request));
    }
}
