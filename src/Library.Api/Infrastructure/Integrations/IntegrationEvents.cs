namespace Library.Api.Infrastructure.Integrations
{
    using Library.Api.Domain.Core;

    public static class IntegrationEvents
    {
        public class BookRegistered : IntegrationEvent
        {
            public string Title { get; set; }
            public string Author { get; set; }
        }

    }
}
