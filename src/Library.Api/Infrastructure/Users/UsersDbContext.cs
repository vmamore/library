namespace Library.Api.Infrastructure.Users
{
    using Library.Api.Domain.Users;
    using Library.Api.Infrastructure.Users.Configurations;
    using Microsoft.EntityFrameworkCore;

    public class UsersDbContext : DbContext
    {
        public DbSet<Librarian> Librarians { get; set; }

        public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(LibrarianConfiguration).Assembly);
        }
    }
}
