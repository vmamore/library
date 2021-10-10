namespace Library.Api.Application.Users
{
    using System.Threading.Tasks;
    using Library.Api.Application.Locators;
    using Library.Api.Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("/locators")]
    public class LocatorCommandsApi : Controller
    {
        private readonly LocatorApplicationService _applicationService;

        public LocatorCommandsApi(LocatorApplicationService applicationService) => _applicationService = applicationService;

        [HttpPost]
        [Authorize(Roles = "librarian,locator")]
        public Task<IActionResult> Post([FromBody] Commands.V1.RegisterLocator request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle);
    }
}
