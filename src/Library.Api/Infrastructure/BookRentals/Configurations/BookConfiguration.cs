namespace Library.Api.Infrastructure.BookRentals.Configurations
{
    using Library.Api.Domain.BookRentals;
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
                .OwnsOne(x => x.ISBN, x =>
                {
                    x.Property(x => x.Value)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ISBN");
                });

            builder
                .Property(x => x.Status)
                .IsRequired();

            builder.ToTable("books", "rentals");
        }
    }
}
