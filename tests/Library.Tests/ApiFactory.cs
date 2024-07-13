using Library.Api;
using Library.Api.Infrastructure.BookRentals;
using Library.Api.Infrastructure.Inventory;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Tests
{
    public class ApiFactory : WebApplicationFactory<IApiAssemblyMarker>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var inventoryDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<InventoryDbContext>));

                services.Remove(inventoryDescriptor);

                services.AddDbContext<InventoryDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });

                var bookRentalDescriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<BookRentalDbContext>));

                services.Remove(bookRentalDescriptor);

                services.AddDbContext<BookRentalDbContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForTesting");
                });
            });
        }
    }
}
