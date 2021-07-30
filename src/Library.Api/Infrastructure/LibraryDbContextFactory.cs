namespace Library.Api.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class LibraryDbContextFactory : IDesignTimeDbContextFactory<LibraryDbContext>
    {
        public LibraryDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<LibraryDbContext>();
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=library_db;User Id=postgres;Password=post_pwd123;");

            return new LibraryDbContext(optionsBuilder.Options);
        }
    }
}
