namespace Library.Api.Infrastructure
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    public static class RequestHandler
    {
        public static async Task<IActionResult> HandleCommand<T>(
            T request, Func<T, Task> handler)
        {
            try
            {
                await handler(request);
                return new OkResult();
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new
                {
                    error = e.Message,
                    stackTrace = e.StackTrace
                });
            }
        }

        public static async Task<IActionResult> HandleQuery<TModel>(
            Func<Task<TModel>> query)
        {
            try
            {
                return new OkObjectResult(await query());
            }
            catch (Exception e)
            {
                return new BadRequestObjectResult(new
                {
                    error = e.Message,
                    stackTrace = e.StackTrace
                });
            }
        }
    }
}
