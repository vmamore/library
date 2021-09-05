namespace Library.Api.Infrastructure.BookRentals.Configurations
{
    using Library.Api.Domain.BookRentals;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BookRentConfiguration : IEntityTypeConfiguration<BookRental>
    {
        public void Configure(EntityTypeBuilder<BookRental> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();

            builder
                .Property(x => x.RentedDay)
                .IsRequired();

            builder
                .OwnsOne(x => x.DayToReturn)
                .Property(x => x.Value)
                .HasColumnName("DayToReturn")
                .IsRequired();

            builder
                .Property(x => x.ReturnedDay)
                .IsRequired(false);

            builder
                .Property(x => x.Status)
                .IsRequired();

            builder.HasOne(x => x.Librarian)
                .WithMany()
                .HasForeignKey("LibrarianId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Locator)
                .WithMany()
                .HasForeignKey("LocatorId")
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(x => x.Books).WithMany(x => x.Rentals).
            UsingEntity<Dictionary<string, object>>(
                "booksrentals",
                b => b.HasOne<Book>().WithMany().HasForeignKey("BookId"),
                b => b.HasOne<BookRental>().WithMany().HasForeignKey("BookRentalId"));

            builder.ToTable("rentals", "rentals");
        }
    }
}
