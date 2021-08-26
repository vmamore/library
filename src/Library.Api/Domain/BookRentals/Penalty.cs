namespace Library.Api.Domain.BookRentals
{
    using System;
    using Library.Api.Domain.Core;
    using static Library.Api.Domain.BookRentals.Events.V1;

    public class Penalty : Entity
    {
        private Penalty() { }

        public Penalty(Locator locator) => Locator = locator;

        public Locator Locator { get; private set; }

        public Guid Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public string Reason { get; set; }
        protected override void When(DomainEvent @event)
        {
            switch (@event)
            {
                case LocatorPenalized e:
                    Id = Guid.NewGuid();
                    CreatedDate = DateTime.UtcNow;
                    EndDate = e.PenaltyEnd;
                    Reason = e.Reason;
                    break;
                default:
                    break;
            }
        }
    }
}