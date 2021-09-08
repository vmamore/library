namespace Library.Api.Domain.Inventory
{
    using Library.Api.Domain.Core;

    public static class Events
    {
        public static class V1
        {
            public class BookRegistered : DomainEvent
            {
                public string ISBN { get; set; }
                public string Title { get; set; }
                public string Author { get; set; }
                public string ReleasedYear { get; set; }
                public int Pages { get; set; }
                public int Version { get; set; }
                public string PhotoUrl { get; internal set; }
            }
        }
    }
}
