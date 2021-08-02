namespace Library.Api.Books
{
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Threading.Tasks;
    using Dapper;
    using static Library.Api.Books.ReadModels;

    public static class Queries
    {
        public static Task<IEnumerable<BookListItem>> Query(
            this DbConnection connection,
            QueryModels.GetAllBooks query)
            => connection.QueryAsync<BookListItem>(
                "SELECT \"Id\", \"Title\", \"Author\"" +
                "FROM \"Books\"");

        public static Task<BookItem> Query(
            this DbConnection connection,
            QueryModels.GetBookById query)
            => connection.QueryFirstAsync<BookItem>(
                "SELECT \"Id\", \"Title\", \"Author\"" +
                "FROM \"Books\"" +
                "WHERE \"Id\" = @id", new { id = query.id});
    }
}
