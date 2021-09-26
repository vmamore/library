namespace Library.Api.Application.Librarians
{
    using System.Threading.Tasks;
    using Library.Api.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Route("/librarians")]
    public class LibrariansCommandsApi : Controller
    {
        private readonly LibrarianApplicationService _applicationService;

        public LibrariansCommandsApi(LibrarianApplicationService applicationService) => _applicationService = applicationService;

        [HttpPost]
        public Task<IActionResult> Post([FromBody] Commands.V1.RegisterLibrarian request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle);
    }
}
