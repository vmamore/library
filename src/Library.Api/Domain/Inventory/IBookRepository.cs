namespace Library.Api.Domain.Inventory
{
    using System;
    using System.Threading.Tasks;

    public interface IBookRepository
    {
        ValueTask<Book> Load(Guid id);

        ValueTask Add(Book entity);

        ValueTask<bool> Exists(Guid id);
    }
}
