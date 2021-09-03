namespace Library.Api.Application.Inventories
{
    using System.Data.Common;
    using System.Threading.Tasks;
    using Library.Api.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    [Route("inventory/books")]
    public class InventoryQueryApi : Controller
    {
        private readonly DbConnection _connection;

        public InventoryQueryApi(DbConnection connection) => _connection = connection;

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
