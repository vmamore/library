namespace Library.Api.Infrastructure.BookRentals.Configurations
{
    using Library.Api.Domain.BookRentals;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PenaltyConfiguration : IEntityTypeConfiguration<Penalty>
    {
        public void Configure(EntityTypeBuilder<Penalty> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();

            builder
                .Property(x => x.CreatedDate)
                .IsRequired();

            builder
                .Property(x => x.EndDate)
                .IsRequired();

            builder.ToTable("penalties", "rentals");
        }
    }
}
