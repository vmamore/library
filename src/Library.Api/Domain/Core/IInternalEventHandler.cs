namespace Library.Api.Domain.Core
{
    public interface IInternalEventHandler
    {
        void Handle(DomainEvent @event);
    }
}
