namespace Library.Api.Application.Shared;

using System.Threading.Tasks;

public interface IApplicationService
{
    Task Handle(ICommand command);
}
