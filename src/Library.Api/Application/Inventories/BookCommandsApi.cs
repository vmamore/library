namespace Library.Api.Application.Inventories
{
    using System.Threading.Tasks;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("inventory/books")]
    public class BooksCommandsApi : Controller
    {
        private readonly BookApplicationService _applicationService;

        public BooksCommandsApi(BookApplicationService applicationService) => _applicationService = applicationService;

        [HttpPost]
        [Authorize(Roles = "librarian")]
        public Task<IActionResult> Post([FromBody] Commands.V1.RegisterBook request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle);
    }
}
