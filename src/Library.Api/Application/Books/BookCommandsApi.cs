namespace Library.Api.Application.Books
{
    using System.Threading.Tasks;
    using Library.Api.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    [Route("/books")]
    public class BookCommandsApi : Controller
    {
        private readonly BookApplicationService _applicationService;

        public BookCommandsApi(BookApplicationService applicationService) => _applicationService = applicationService;

        [HttpPost]
        public Task<IActionResult> Post([FromBody] Commands.V1.RegisterBook request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle);

        [HttpPost("{bookId}/rent")]
        public Task<IActionResult> Post([FromBody] Commands.V1.RentBooks request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle);

        [HttpPost("{bookId}/return")]
        public Task<IActionResult> Post([FromBody] Commands.V1.ReturnBookRental request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle);
    }
}
