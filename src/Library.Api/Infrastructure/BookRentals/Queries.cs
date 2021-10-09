namespace Library.Api.Infrastructure.BookRentals
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Library.Api.Application.Rentals;
    using static Library.Api.Infrastructure.BookRentals.ReadModels;

    public static class Queries
    {
        public static async Task<GetAllBooksPaginated> Query(
            this DbConnection connection,
            QueryModels.GetAllBooks query)
        {
            var rows = 10;
            var offset = (query.page - 1) * rows;

            var reader = await connection.QueryMultipleAsync(
                "SELECT count(*) FROM \"rentals\".\"books\"" +
                "WHERE \"Title\" ILIKE CONCAT('%', @title, '%');" +
                "SELECT \"Id\", \"Title\", \"Author\", \"PhotoUrl\", \"Status\"" +
                "FROM \"rentals\".\"books\" " +
                "WHERE \"Title\" ILIKE CONCAT('%', @title, '%') " +
                "LIMIT @rows " +
                "OFFSET @offset", new
                {
                    offset,
                    rows,
                    query.title
                });

            return new GetAllBooksPaginated()
            {
                CurrentPage = query.page,
                TotalCount = await reader.ReadFirstAsync<long>(),
                Books = await reader.ReadAsync<GetAllBooksPaginated.BookListItem>()
            };
        }

        public static async Task<BookRental> Query(
            this DbConnection connection,
            QueryModels.GetRentalByLocator query)
        {
            var bookRentalDictionary = new Dictionary<Guid, BookRental>();

            var list = await connection.QueryAsync<BookRental, BookRental.BookRented, BookRental>(
                 "SELECT " +
                    "r.\"Id\"," +
                    "r.\"RentedDay\"," +
                    "r.\"DayToReturn\"," +
                    "r.\"ReturnedDay\", " +
                    "CASE " +
                        "WHEN r.\"Status\" = 1 THEN 'On Going'" +
                        "WHEN r.\"Status\" = 2 THEN 'Done'" +
                        "WHEN r.\"Status\" = 3 THEN 'Late'" +
                    "END \"Status\"," +
                    "b.\"Title\"," +
                    "b.\"Author\"" +
                "FROM \"rentals\".\"rentals\" r " +
                "INNER JOIN \"rentals\".\"booksrentals\" br ON br.\"BookRentalId\" = r.\"Id\"" +
                "INNER JOIN \"rentals\".\"books\" b ON b.\"Id\" = br.\"BookId\"" +
                "WHERE r.\"LocatorId\" = @locatorId",
                 (bookRental, bookRented) =>
                 {
                     BookRental bookRentalEntry;

                     if (!bookRentalDictionary.TryGetValue(bookRental.Id, out bookRentalEntry))
                     {
                         bookRentalEntry = bookRental;
                         bookRentalEntry.Books = new List<BookRental.BookRented>();
                         bookRentalDictionary.Add(bookRentalEntry.Id, bookRentalEntry);
                     }

                     bookRentalEntry.Books.Add(bookRented);
                     return bookRentalEntry;
                 }, new { query.locatorId }, splitOn: "Title");

            if (list.Count() == 0)
            {
                return null;
            }

            return bookRentalDictionary.First().Value;
        }
    }
}
