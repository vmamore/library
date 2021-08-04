namespace Library.Api.Infrastructure.Configurations
{
    using Library.Api.Domain.Books;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();

            builder
                .Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(x => x.Author)
                .IsRequired()
                .HasMaxLength(150);

            builder
                .Property(x => x.ReleasedYear)
                .IsRequired()
                .HasMaxLength(5);

            builder
                .Property(x => x.Pages)
                .IsRequired()
                .HasMaxLength(5);

            builder
                .Property(x => x.Version)
                .IsRequired()
                .HasMaxLength(5);

            builder.HasMany(x => x.Rents)
                .WithOne()
                .HasForeignKey(x => x.BookId);
        }
    }
}
