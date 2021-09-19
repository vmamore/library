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
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Npgsql;
using BookRentalsIntegrationEventHandler = Library.Api.Infrastructure.BookRentals.IntegrationEventHandler;
using IntegrationEventHandler = Library.Api.Infrastructure.Integrations.IntegrationEventHandler;

public class Startup
{
    IConfiguration Configuration;

    public Startup(IConfiguration configuration) => Configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Configuration.GetConnectionString("LibraryDb");
        services.AddHealthChecks()
                .AddNpgSql(connectionString);
        services.AddEntityFrameworkNpgsql();
        services.AddPostgresDbContext<InventoryDbContext>(connectionString);
        services.AddPostgresDbContext<BookRentalDbContext>(connectionString);
        services.AddScoped<DbConnection>(c => new NpgsqlConnection(connectionString));

        services.AddSingleton<ISystemClock, SystemClock>();

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

        var keycloackSettings = Configuration.GetSection(KeycloackSettings.Path).Get<KeycloackSettings>();

        services.AddAuthentication(options =>
        {
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        })
                .AddJwtBearer(options =>
                {
                    options.Authority = $"{keycloackSettings.Authority}{keycloackSettings.LibraryRealmPath}";
                    options.Audience = keycloackSettings.Audience;
                    options.RequireHttpsMetadata = false;
                });

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
        app.UseAuthorization();
        app.UseAuthentication();
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

