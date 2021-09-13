namespace Library.Api.Infrastructure
{
    using Library.Api.Infrastructure.BookRentals;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class UsersContextFactory : IDesignTimeDbContextFactory<BookRentalDbContext>
    {
        public BookRentalDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookRentalDbContext>();
            optionsBuilder.UseNpgsql("Server=library_db;Port=5432;Database=library_db;User Id=postgres;Password=post_pwd123;");
            optionsBuilder.EnableSensitiveDataLogging();

            return new BookRentalDbContext(optionsBuilder.Options);
        }
    }
}
