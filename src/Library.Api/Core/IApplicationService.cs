namespace Library.Api.Core
{
    using System.Threading.Tasks;

    public interface IApplicationService
    {
        Task Handle(ICommand command);
    }
}
