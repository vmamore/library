namespace Library.Api.Application.Rentals
{
    using System;
    using System.Threading.Tasks;
    using Infrastructure;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("rentals")]
    public class BookRentalsCommandsApi(BookRentalApplicationService applicationService) : Controller
    {
        [HttpPost]
        [Authorize(Roles = "locator,librarian")]
        public Task<IActionResult> Post([FromBody] Commands.V1.RentBooks request)
            => RequestHandler.HandleCommand(request, applicationService.Handle);

        [HttpPatch("{rentalId}/return")]
        [Authorize(Roles = "librarian")]
        public async Task<IActionResult> Post([FromRoute] Guid rentalId, [FromBody] Commands.V1.ReturnBookRental request)
        {
            if (rentalId != request.BookRentalId)
            {
                return new BadRequestObjectResult(new
                {
                    error = "Invalid Rental"
                });
            }

            return await RequestHandler.HandleCommand(request, applicationService.Handle);
        }
    }
}
