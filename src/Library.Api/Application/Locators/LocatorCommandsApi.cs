namespace Library.Api.Application.Users
{
    using System.Threading.Tasks;
    using Locators;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("/locators")]
    public class LocatorCommandsApi(LocatorApplicationService applicationService) : Controller
    {
        [HttpPost]
        [AllowAnonymous]
        public Task<IActionResult> Post([FromBody] Commands.V1.RegisterLocator request)
            => RequestHandler.HandleCommand(request, applicationService.Handle);
    }
}
