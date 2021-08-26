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

            builder.HasOne("_locator")
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne("_librarian")
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(r => r.Books)
                .WithOne();

            builder.ToTable("bookrentals", "rentals");
        }
    }
}
