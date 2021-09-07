namespace Library.Api.Infrastructure.BookRentals
{
    using System.Collections.Generic;
    using System.Data.Common;
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
                "SELECT count(*) FROM \"rentals\".\"books\";" +
                "SELECT \"Id\", \"Title\", \"Author\"" +
                "FROM \"rentals\".\"books\"" +
                "LIMIT @rows " +
                "OFFSET @offset", new
                {
                    offset,
                    rows
                });

            return new GetAllBooksPaginated()
            {
                CurrentPage = query.page,
                TotalCount = await reader.ReadFirstAsync<long>(),
                Books = await reader.ReadAsync<GetAllBooksPaginated.BookListItem>()
            };
        }

        public static Task<BookItem> Query(
            this DbConnection connection,
            QueryModels.GetBookById query)
            => connection.QueryFirstOrDefaultAsync<BookItem>(
                 "SELECT \"Id\", \"Title\", \"Author\"" +
                 "FROM \"rentals\".\"books\" Where \"Id\" = @id",
                 new { id = query.Id });
    }
}
