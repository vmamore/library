namespace Library.Api.Application.Rentals
{
    using System;
    using System.Threading.Tasks;
    using Library.Api.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    [Route("rentals")]
    public class BookRentalsCommandsApi : Controller
    {
        private readonly BookRentalApplicationService _applicationService;

        public BookRentalsCommandsApi(BookRentalApplicationService applicationService) => _applicationService = applicationService;

        [HttpPost]
        public Task<IActionResult> Post([FromBody] Commands.V1.RentBooks request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle);

        [HttpPatch("{rentalId}/return")]
        public async Task<IActionResult> Post([FromRoute] Guid rentalId, [FromBody] Commands.V1.ReturnBookRental request)
        {
            if (rentalId != request.BookRentalIdId)
            {
                return new BadRequestObjectResult(new
                {
                    error = "RentalId from route is different from body's RentalId"
                });
            }

            return await RequestHandler.HandleCommand(request, _applicationService.Handle);
        }
    }
}
