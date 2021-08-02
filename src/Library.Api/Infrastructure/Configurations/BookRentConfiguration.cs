namespace Library.Api.Infrastructure.Configurations
{
    using Library.Api.Books;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class BookRentConfiguration : IEntityTypeConfiguration<BookRent>
    {
        public void Configure(EntityTypeBuilder<BookRent> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();

            builder
                .Property(x => x.PersonId)
                .IsRequired();

            builder
                .Property(x => x.RentedDay)
                .IsRequired();

            builder
                .Property(x => x.DayToReturn)
                .IsRequired();

            builder
                .Property(x => x.ReturnedDay)
                .IsRequired();

            builder
                .Property(x => x.Status)
                .IsRequired();

            builder
                .Property(x => x.BookCondition)
                .IsRequired(false)
                .HasMaxLength(100);
        }
    }
}
