namespace Library.Api.Domain.BookRentals
{
    using System;
    using System.Threading.Tasks;

    public interface IBookRentalRepository
    {
        ValueTask<BookRental> Load(Guid id);

        ValueTask Add(BookRental entity);

        ValueTask<bool> Exists(Guid id);
        Task<IEnumerable<Book>> LoadBooks(Guid[] booksId);
    }
}