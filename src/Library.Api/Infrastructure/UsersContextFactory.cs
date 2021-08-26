namespace Library.Api.Infrastructure
{
    using Library.Api.Infrastructure.Users;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Design;

    public class UsersContextFactory : IDesignTimeDbContextFactory<UsersDbContext>
    {
        public UsersDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<UsersDbContext>();
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=library_db;User Id=postgres;Password=post_pwd123;");
            optionsBuilder.EnableSensitiveDataLogging();

            return new UsersDbContext(optionsBuilder.Options);
        }
    }
}
