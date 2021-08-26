using System.Data.Common;
using Library.Api.Application.Books;
using Library.Api.Application.Locators;
using Library.Api.Application.Shared;
using Library.Api.Domain.BookRentals;
using Library.Api.Domain.Inventory;
using Library.Api.Infrastructure;
using Library.Api.Infrastructure.BookRentals;
using Library.Api.Infrastructure.Clients;
using Library.Api.Infrastructure.Inventory;
using Microsoft.OpenApi.Models;
using Npgsql;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        const string connectionString =
            "Server=localhost;Port=5432;Database=library_db;User Id=postgres;Password=post_pwd123;";
        services.AddEntityFrameworkNpgsql();
        services.AddPostgresDbContext<InventoryDbContext>(connectionString);
        services.AddPostgresDbContext<BookRentalDbContext>(connectionString);
        services.AddScoped<DbConnection>(c => new NpgsqlConnection(connectionString));
        services.AddHealthChecks()
                .AddNpgSql(connectionString);
        services.AddScoped<IHolidayClient, HolidayClient>();
        services.AddScoped<IBookRentalRepository, BookRentalRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<BookApplicationService>();
        services.AddScoped<LocatorApplicationService>();

        services.AddMvc();
        services.AddSwaggerGen(c => c.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title = "Library",
                    Version = "v1"
                }));
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseRouting();
        app.UseHttpLogging();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapHealthChecks("/health");
        });
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library v1"));
    }
}

