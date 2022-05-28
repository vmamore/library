namespace Library.Api.Domain.BookRentals
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IBookRentalRepository
    {
        ValueTask<BookRental> Load(Guid id);

        ValueTask<BookRental> GetActive(Guid locatorId);

        ValueTask Add(BookRental entity);

        ValueTask<bool> Exists(Guid id);
        Task<IEnumerable<Book>> LoadBooks(Guid[] booksId);

        ValueTask Add(Book entity);
        Task Commit();
    }
}
