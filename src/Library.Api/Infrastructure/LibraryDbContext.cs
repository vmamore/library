namespace Library.Api.Infrastructure
{
    using Library.Api.Books;
    using Library.Api.Infrastructure.Configurations;
    using Microsoft.EntityFrameworkCore;

    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookConfiguration).Assembly);
        }
    }
}
