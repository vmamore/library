namespace Library.Api.Infrastructure.Integrations
{
    using System;
    using System.Threading.Tasks;

    public static class Dispatcher
    {
        public static async Task HandleIntegrationEvent<T>(T request, Func<T, Task> handler) => await handler(request);
    }
}
