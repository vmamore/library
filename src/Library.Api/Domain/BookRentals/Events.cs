namespace Library.Api.Domain.BookRentals
{
    using System;
    using Shared.Core;

    public static class Events
    {
        public static class V1
        {
            public class BookRegistered : IDomainEvent
            {
                public string Author { get; set; }
                public string Title { get; set; }
                public string PhotoUrl { get; set; }
            }
            public class RentalCreated : IDomainEvent
            {
                public DateTime DayToReturn { get; set; }
                public Guid[] BooksId { get; set; }
                public DateTime RentedDay { get; set; }
            }

            public class RentalReturned : IDomainEvent
            {
                public Guid BookId { get; set; }
                public Guid LibrarianId { get; set; }
                public DateTime ReturnedDay { get; set; }
                public string BookCondition { get; set; }
            }

            public class LocatorCreated : IDomainEvent
            {
                public string FirstName { get; set; }
                public string LastName { get; set; }
                public string CPF { get; set; }
                public DateTime BirthDate { get; set; }
                public string Street { get; set; }
                public string City { get; set; }
                public string Number { get; set; }
                public string District { get; set; }
            }

            public class LocatorPenalized : IDomainEvent
            {
                public DateTime PenalizedDate { get; set; }
                public DateTime PenaltyEnd { get; set; }
                public string Reason { get; set; }
            }

            public class PenaltyFinished : IDomainEvent
            {
                public DateTime CurrentDate { get; set; }
                public string Reason { get; set; }
            }
        }
    }
}
