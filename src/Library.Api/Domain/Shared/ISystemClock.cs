namespace Library.Api.Domain.Shared;

using System;

public interface ISystemClock
{
    DateTime UtcNow { get; }
}
