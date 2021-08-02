namespace Library.Api.Books
{
    using System;
    using System.Threading.Tasks;

    public interface IBookRepository
    {
        Task<Book> Load(Guid id);

        ValueTask Add(Book entity);

        ValueTask<bool> Exists(Guid id);
    }
}
