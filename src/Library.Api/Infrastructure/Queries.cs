namespace Library.Api.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Library.Api.Application.Books;
    using static Library.Api.Infrastructure.ReadModels;

    public static class Queries
    {
        public static Task<IEnumerable<BookListItem>> Query(
            this DbConnection connection,
            QueryModels.GetAllBooks query)
            => connection.QueryAsync<BookListItem>(
                "SELECT \"Id\", \"Title\", \"Author\"" +
                "FROM \"Books\"");

        public static async Task<BookItem> Query(
            this DbConnection connection,
            QueryModels.GetBookById query)
        {
            var bookById = new Dictionary<Guid, BookItem>();

            var result = await connection.QueryAsync<BookItem, BookItem.BookRentItem, BookItem>(
                "SELECT " +
                "\"Books\".\"Id\"," +
                "\"Books\".\"Title\"," +
                "\"Books\".\"Author\"," +
                "\"BookRent\".\"Id\" as \"BookRentId\"," +
                "\"BookRent\".\"PersonId\"," +
                "\"BookRent\".\"DayToReturn\"," +
                "\"BookRent\".\"ReturnedDay\"," +
                "\"BookRent\".\"Status\", " +
                "CASE " +
                "WHEN \"BookRent\".\"Status\" = 1 THEN 'Borrowed' " +
                "WHEN \"BookRent\".\"Status\" = 2 THEN 'Returned' " +
                "END as \"Status\" " +
                "FROM \"public\".\"Books\" " +
                "JOIN \"public\".\"BookRent\" ON \"BookId\" = \"Books\".\"Id\" " +
                "WHERE \"Books\".\"Id\" = @id",
                (book, rent) =>
                {
                    BookItem bookItem = null;

                    if (bookById.TryGetValue(book.Id, out bookItem))
                    {
                        bookItem.Rents.Add(rent);
                    }
                    else
                    {
                        bookItem = book;
                        bookItem.Rents = new List<BookItem.BookRentItem>();
                        bookItem.Rents.Add(rent);
                        bookById.Add(book.Id, bookItem);
                    }

                    return book;
                },
                new { id = query.id },
                splitOn: "BookRentId");

            return bookById.FirstOrDefault().Value;
        }
    }
}
