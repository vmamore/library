namespace Library.Api.Application.Shared;

using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Shared.Core;

public interface IDispatcher
{
    Task PublishAsync<T>(T message) where T : IDomainEvent;
    Task PublishAsync<T>(IEnumerable<T> messages) where T : IDomainEvent;
}
