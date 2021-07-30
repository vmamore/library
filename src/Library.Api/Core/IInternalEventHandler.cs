namespace Library.Api.Core
{
    public interface IInternalEventHandler
    {
        void Handle(DomainEvent @event);
    }
}
