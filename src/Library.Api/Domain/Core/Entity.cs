namespace Library.Api.Domain.Core
{
    public abstract class Entity : IInternalEventHandler
    {
        protected abstract void When(DomainEvent @event);
        protected void Apply(DomainEvent @event)
        {
            When(@event);
        }

        void IInternalEventHandler.Handle(DomainEvent @event) => When(@event);
    }
}
