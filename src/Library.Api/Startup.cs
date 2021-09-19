using System.Data.Common;
using Library.Api.Application.Inventories;
using Library.Api.Application.Librarians;
using Library.Api.Application.Locators;
using Library.Api.Application.Rentals;
using Library.Api.Application.Shared;
using Library.Api.Domain.BookRentals;
using Library.Api.Domain.BookRentals.Users;
using Library.Api.Domain.Inventory;
using Library.Api.Domain.Shared;
using Library.Api.Infrastructure;
using Library.Api.Infrastructure.BookRentals;
using Library.Api.Infrastructure.Clients;
using Library.Api.Infrastructure.Integrations;
using Library.Api.Infrastructure.Inventory;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Npgsql;
using BookRentalsIntegrationEventHandler = Library.Api.Infrastructure.BookRentals.IntegrationEventHandler;
using IntegrationEventHandler = Library.Api.Infrastructure.Integrations.IntegrationEventHandler;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        const string connectionString =
            "Server=library_db;Port=5432;Database=library_db;User Id=postgres;Password=post_pwd123;Include Error Detail=true;";
        services.AddEntityFrameworkNpgsql();
        services.AddPostgresDbContext<InventoryDbContext>(connectionString);
        services.AddPostgresDbContext<BookRentalDbContext>(connectionString);
        services.AddScoped<DbConnection>(c => new NpgsqlConnection(connectionString));
        services.AddHealthChecks()
                .AddNpgSql(connectionString);
        services.AddScoped<IHolidayClient, HolidayClient>();
        services.AddScoped<IBookRentalRepository, BookRentalRepository>();
        services.AddScoped<IBookRepository, BookRepository>();
        services.AddScoped<ILocatorRepository, LocatorRepository>();
        services.AddScoped<ILibrarianRepository, LibrarianRepository>();
        services.AddScoped<BookRentalApplicationService>();
        services.AddScoped<BookApplicationService>();
        services.AddScoped<LocatorApplicationService>();
        services.AddScoped<LibrarianApplicationService>();
        services.AddScoped<IIntegrationEventsMapper, Mapper>();
        services.AddScoped<IIntegrationEventHandler, IntegrationEventHandler>();
        services.AddScoped<BookRentalsIntegrationEventHandler>();
        services.AddSingleton<ISystemClock, SystemClock>();

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
        app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();

            endpoints.MapHealthChecks("/health");
        });
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library v1");
            c.RoutePrefix = string.Empty;
        });
    }
}

