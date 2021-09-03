namespace Library.Api.Domain.Users
{
    using System;
    using System.Threading.Tasks;

    public interface ILibrarianRepository
    {
        ValueTask<Librarian> Load(Guid id);

        ValueTask Add(Librarian entity);

        ValueTask<bool> Exists(Guid id);
        Task Commit();
    }
}
