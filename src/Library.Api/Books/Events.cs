namespace Library.Api.Books
{
    using System;
    using Library.Api.Core;

    public static class Events
    {
        public static class V1
        {
            public class BookRegistered : DomainEvent
            {
                public string Title { get; set; }
                public string Author { get; set; }
                public string ReleasedYear { get; set; }
                public int Pages { get; set; }
                public int Version { get; set; }
            }

            public class BookRented : DomainEvent
            {
                public DateTime DayToReturn { get; set; }
                public Guid BookId { get; set; }
                public Guid PersonId { get; set; }
                public DateTime RentedDay { get; set; }
                public Guid BookRentedId { get; set; }
            }

            public class BookReturned : DomainEvent
            {
                public Guid BookId { get; set; }
                public DateTime ReturnedDay { get; set; }
                public string BookCondition { get; set; }
            }
        }
    }
}
