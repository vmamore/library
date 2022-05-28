namespace Library.Api.Domain.BookRentals
{
    using System;
    using Shared.Core;
    using static Events.V1;

    public class Penalty : Entity
    {
        private Penalty() { }

        public Penalty(Locator locator) => Locator = locator;

        public Locator Locator { get; }

        public Guid Id { get; private set; }
        public DateTime CreatedDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public DateTime ActualEndDate { get; private set; }
        public string Reason { get; private set; }
        public bool IsActive { get; private set; }

        protected override void When(IDomainEvent @event)
        {
            switch (@event)
            {
                case LocatorPenalized e:
                    Id = Guid.NewGuid();
                    CreatedDate = DateTime.UtcNow;
                    EndDate = e.PenaltyEnd;
                    Reason = $"{CreatedDate}: {e.Reason}";
                    IsActive = true;
                    break;
                case PenaltyFinished e:
                    ActualEndDate = e.CurrentDate;
                    Reason = $"\n{ActualEndDate}: {e.Reason}";
                    IsActive = false;
                    break;
                default:
                    break;
            }
        }
    }
}
