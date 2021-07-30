namespace Library.Api.Books
{
    using System;
    using Library.Api.Core;
    using static Library.Api.Books.Events.V1;

    public class BookRent : Entity
    {
        public enum BookRentStatus
        {
            OnGoing = 1,
            Done
        }

        public Guid Id { get; private set; }
        public Guid PersonId { get; private set; }
        public Guid BookId { get; private set; }
        public DateTime RentedDay { get; private set; }
        public DateTime DayToReturn { get; private set; }
        public DateTime ReturnedDay { get; private set; }
        public BookRentStatus Status { get; private set; }
        public string BookCondition { get; set; }

        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case BookRented e:
                    Status = BookRentStatus.OnGoing;
                    Id = e.BookRentedId;
                    RentedDay = e.RentedDay;
                    PersonId = e.PersonId;
                    BookId = e.BookId;
                    DayToReturn = e.DayToReturn;
                    break;
                case BookReturned e:
                    Status = BookRentStatus.Done;
                    ReturnedDay = e.ReturnedDay;
                    BookCondition = e.BookCondition;
                    break;
                default:
                    break;
            }
        }
    }
}
