namespace Library.Api.Domain.Core
{
    public abstract class Entity : IInternalEventHandler
    {
        protected abstract void When(IDomainEvent @event);
        protected void Apply(IDomainEvent @event)
        {
            When(@event);
        }

        void IInternalEventHandler.Handle(IDomainEvent @event) => When(@event);
    }
}
