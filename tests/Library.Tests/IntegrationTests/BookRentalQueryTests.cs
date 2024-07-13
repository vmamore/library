using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Library.Api.Application.Rentals;
using Library.Api.Application.Shared;
using Library.Api.Domain.BookRentals;
using Library.Api.Infrastructure.BookRentals;
using Microsoft.EntityFrameworkCore;
using Npgsql;
using Xunit;

namespace Library.Tests.IntegrationTests
{
    public class BookRentalQueryTests
    {
        private readonly BookRentalRepository _bookRentalRepository;
        private readonly LocatorRepository _locatorRepository;
        private readonly NpgsqlConnection _connection;

        public BookRentalQueryTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookRentalDbContext>();
            optionsBuilder.UseNpgsql(
                "Server=localhost;Port=5432;Database=library_db;User Id=postgres;Password=post_pwd123;Include Error Detail=true;");
            var context = new BookRentalDbContext(optionsBuilder.Options);
            _bookRentalRepository = new BookRentalRepository(context);
            _locatorRepository = new LocatorRepository(context);
            _connection = new NpgsqlConnection("Server=localhost;Port=5432;Database=library_db;User Id=postgres;Password=post_pwd123;Include Error Detail=true;");
        }

        [Fact]
        public async Task When_FetchingRentalByLocator_Should_ReturnBeAsExpected()
        {
            var locator = await _locatorRepository.Load(Guid.Parse("e4eafac2-0e6d-463d-b76a-cab2a9ff2f7c"));
            var books = await _bookRentalRepository.LoadBooks(new[]
            {
                Guid.Parse("44bf17e5-ffd2-4219-8211-28a37448a119"),
                Guid.Parse("bb58cd57-2148-4d46-a001-8cc4ce93ad4f")
            });
            var rental = BookRental.Create(locator, books, DateTime.Now.AddDays(3));
            await _bookRentalRepository.Add(rental);
            await _bookRentalRepository.Commit();
            
            var expectedBookRental = new List<ReadModels.BookRental>()
            {
                new()
                {
                    Id = rental.Id,
                    RentedDay = DateTime.Now,
                    DayToReturn = DateTime.Now.AddDays(3),
                    ReturnedDay = null,
                    Status = "On Going",
                    Books = new List<ReadModels.BookRental.BookRented>()
                    {
                        new ()
                        {
                            Title = "Fight Club",
                            Author = "Chuck Palahniuk"
                        },
                        new ()
                        {
                            Title = "The Almanack of Naval Ravikant",
                            Author = "Eric Jorgenson"
                        }
                    }
                }
            };
            var rentals = await _connection.Query(new QueryModels.GetRentalByLocator(Guid.Parse("e4eafac2-0e6d-463d-b76a-cab2a9ff2f7c")));
            
            rentals.Should().HaveCount(1);
            rentals.Should().BeEquivalentTo(expectedBookRental, options =>
                options.Excluding(o => o.RentedDay)
                       .Excluding(o => o.ReturnedDay)
                       .Excluding(o => o.DayToReturn));

            var persistedRental = await _bookRentalRepository.Load(rental.Id);
            persistedRental.Returned(new SystemClock());
            await _bookRentalRepository.Commit();
        }
    }
}