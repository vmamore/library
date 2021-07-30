namespace Library.Api.Books
{
using System.Data.Common;
    using System.Threading.Tasks;
    using Library.Api.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    [Route("/books")]
    public class BookQueryApi : Controller
    {
        private readonly DbConnection _connection;

        public BookQueryApi(DbConnection connection) => _connection = connection;

        [HttpGet]
        [Route("all")]
        public Task<IActionResult> Get(QueryModels.GetAllBooks request)
            => RequestHandler.HandleQuery(() => _connection.Query(request));
    }
}
