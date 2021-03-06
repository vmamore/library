namespace Library.Api.Infrastructure.Inventory.Configurations
{
    using Library.Api.Domain.Inventory;
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
                .HasMaxLength(12);

            builder
                .Property(x => x.PhotoUrl)
                .IsRequired()
                .HasMaxLength(450);

            builder
                .OwnsOne(x => x.ISBN, x =>
                {
                    x.Property(x => x.Value)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("ISBN");
                });

            builder
                .Property(x => x.Pages)
                .IsRequired();

            builder
                .Property(x => x.Version)
                .IsRequired();

            builder.ToTable("books", "inventory");
        }
    }
}
