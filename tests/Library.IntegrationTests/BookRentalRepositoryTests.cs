using System;
using System.Threading.Tasks;
using FluentAssertions;
using Library.Api.Application.Shared;
using Library.Api.Domain.BookRentals;
using Library.Api.Infrastructure.BookRentals;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Library.IntegrationTests
{
    public class BookRentalRepositoryTests 
    {
        private readonly BookRentalRepository _sut;
        private readonly LocatorRepository _locatorRepository;

        public BookRentalRepositoryTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookRentalDbContext>();
            optionsBuilder.UseNpgsql(
                "Server=localhost;Port=5432;Database=library_db;User Id=postgres;Password=post_pwd123;Include Error Detail=true;");
            var context = new BookRentalDbContext(optionsBuilder.Options);
            _sut = new BookRentalRepository(context);
            _locatorRepository = new LocatorRepository(context);
        }

        [Fact]
        public async Task Given_QueryingRentalById_Should_ContainBooksRented()
        {
            var locator = await _locatorRepository.Load(Guid.Parse("e4eafac2-0e6d-463d-b76a-cab2a9ff2f7c"));
            var books = await _sut.LoadBooks(new[]
            {
                Guid.Parse("44bf17e5-ffd2-4219-8211-28a37448a119"),
                Guid.Parse("bb58cd57-2148-4d46-a001-8cc4ce93ad4f")
            });
            var rental = BookRental.Create(locator, books, DateTime.Now.AddDays(3));
            await _sut.Add(rental);
            await _sut.Commit();
            
            var persistedRental = await _sut.Load(rental.Id);
            persistedRental.Should().NotBeNull();
            persistedRental.Books.Should().NotBeEmpty();
            
            persistedRental.Returned(new SystemClock());
            await _sut.Commit();
        }
    }
}