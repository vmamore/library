namespace Library.Api.Infrastructure
{
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Threading.Tasks;
    using Dapper;
    using Library.Api.Application.Rentals;
    using static Library.Api.Infrastructure.BookRentals.ReadModels;

    public static class Queries
    {
        public static Task<IEnumerable<BookListItem>> Query(
            this DbConnection connection,
            QueryModels.GetAllBooks query)
            => connection.QueryAsync<BookListItem>(
                "SELECT \"Id\", \"Title\", \"Author\"" +
                "FROM \"rentals\".\"Books\"");

        public static Task<BookItem> Query(
            this DbConnection connection,
            QueryModels.GetBookById query) => connection.QueryFirstOrDefaultAsync<BookItem>(
                 "SELECT \"Id\", \"Title\", \"Author\"" +
                 "FROM \"rentals\".\"Books\" Where \"Id\" = @id", new { id = query.Id });
    }
}
