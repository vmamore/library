namespace Library.Api.Domain.Shared;

public interface ISystemClock
{
    DateTime UtcNow { get; }
}
