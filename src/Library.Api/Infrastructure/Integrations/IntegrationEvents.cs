namespace Library.Api.Infrastructure.Integrations
{
    public class BookRegistered : IIntegrationEvent
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string PhotoUrl { get; set; }
    }
}
