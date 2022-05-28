namespace Library.Api.Domain.Shared.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public abstract class AggregateRoot : IInternalEventHandler
    {
        List<IDomainEvent> events = new();

        public IEnumerable<IDomainEvent> GetChanges() => events.AsEnumerable();

        public void Apply(IDomainEvent @event)
        {
            When(@event);
            EnsureValidState();
            events.Add(@event);
        }

        public abstract void When(IDomainEvent @event);

        public abstract void EnsureValidState();
        void IInternalEventHandler.Handle(IDomainEvent @event) => When(@event);

        protected void ApplyToEntity(IInternalEventHandler entity, IDomainEvent @event) => entity?.Handle(@event);

        protected void ApplyToEntity(IEnumerable<IInternalEventHandler> entities, IDomainEvent @event)
        {
            foreach (var entity in entities)
                this.ApplyToEntity(entity, @event);
        }
    }
}
