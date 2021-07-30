namespace Library.Api.Core
{
    using System.Threading.Tasks;

    public interface IUnitOfWork
    {
        Task Commit();
    }
}
