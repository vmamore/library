namespace Library.Api.Infrastructure
{
    using Library.Api.Books;
    using Microsoft.EntityFrameworkCore;

    public class LibraryDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }

        public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(x => x.Id)
                .ValueGeneratedNever();

            modelBuilder.Entity<Book>()
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Book>()
                .Property(x => x.Author)
                .IsRequired()
                .HasMaxLength(150);

            modelBuilder.Entity<Book>()
                .Property(x => x.ReleasedYear)
                .IsRequired()
                .HasMaxLength(5);

            modelBuilder.Entity<Book>()
                .Property(x => x.Pages)
                .IsRequired()
                .HasMaxLength(5);

            modelBuilder.Entity<Book>()
                .Property(x => x.Version)
                .IsRequired()
                .HasMaxLength(5);


        }
    }
}
