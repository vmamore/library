namespace Library.Api.Application.Rentals
{
    using System.Threading.Tasks;
    using Library.Api.Infrastructure;
    using Microsoft.AspNetCore.Mvc;

    [Route("rentals/books")]
    public class BookRentalsCommandsApi : Controller
    {
        private readonly BookRentalApplicationService _applicationService;

        public BookRentalsCommandsApi(BookRentalApplicationService applicationService) => _applicationService = applicationService;

        [HttpPost("{bookId}/rent")]
        public Task<IActionResult> Post([FromBody] Commands.V1.RentBooks request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle);

        [HttpPost("{bookId}/return")]
        public Task<IActionResult> Post([FromBody] Commands.V1.ReturnBookRental request)
            => RequestHandler.HandleCommand(request, _applicationService.Handle);
    }
}
