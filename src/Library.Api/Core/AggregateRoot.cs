namespace Library.Api.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class AggregateRoot : IInternalEventHandler
    {
        List<DomainEvent> events = new List<DomainEvent>();

        public IEnumerable<DomainEvent> GetChanges() => events.AsEnumerable();

        public void Apply(DomainEvent @event)
        {
            When(@event);
            EnsureValidState();
            events.Add(@event);
        }

        public abstract void When(DomainEvent @event);

        public abstract void EnsureValidState();
        void IInternalEventHandler.Handle(DomainEvent @event) => When(@event);

        protected void ApplyToEntity(IInternalEventHandler entity, DomainEvent @event) => entity?.Handle(@event);
    }
}
