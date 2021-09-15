using Library.Api.Domain.Shared;

namespace Library.Api.Application.Shared;

public class SystemClock : ISystemClock
{
    public DateTime UtcNow => DateTime.UtcNow;
}
