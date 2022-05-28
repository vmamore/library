namespace Library.Api.Domain.Shared.Core
{
    public interface IInternalEventHandler
    {
        void Handle(IDomainEvent @event);
    }
}
