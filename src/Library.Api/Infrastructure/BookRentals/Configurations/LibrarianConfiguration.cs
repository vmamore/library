namespace Library.Api.Infrastructure.BookRentals.Configurations
{
    using Domain.Users;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class LibrarianConfiguration : IEntityTypeConfiguration<Librarian>
    {
        public void Configure(EntityTypeBuilder<Librarian> builder)
        {
            builder
                .Property(x => x.Id)
                .ValueGeneratedNever();

            builder
                .OwnsOne(x => x.Name)
                .Property(x => x.FirstName)
                .HasColumnName("FirstName");

            builder
                .OwnsOne(x => x.Name)
                .Property(x => x.LastName)
                .HasColumnName("LastName");

            builder
                .OwnsOne(x => x.Age)
                .Property(x => x.BirthDate)
                .HasColumnName("BirthDate");

            builder
                .OwnsOne(x => x.Cpf)
                .Property(x => x.Value)
                .HasColumnName("Cpf");

            builder.OwnsOne(x => x.Address, x =>
            {
                x.Property(x => x.City)
                    .HasMaxLength(50)
                    .HasColumnName("City");

                x.Property(x => x.Street)
                    .HasMaxLength(100)
                    .HasColumnName("Street");

                x.Property(x => x.Number)
                    .HasMaxLength(10)
                    .HasColumnName("Number");

                x.Property(x => x.District)
                    .HasMaxLength(20)
                    .HasColumnName("District");
            });

            builder.ToTable("librarians", "rentals");
        }
    }
}
