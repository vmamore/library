namespace Library.Api.Application.Librarians
{
    using System.Threading.Tasks;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("/librarians")]
    public class LibrariansCommandsApi(LibrarianApplicationService applicationService) : Controller
    {
        [HttpPost]
        [Authorize(Roles = "librarian")]
        public Task<IActionResult> Post([FromBody] Commands.V1.RegisterLibrarian request)
            => RequestHandler.HandleCommand(request, applicationService.Handle);
    }
}
