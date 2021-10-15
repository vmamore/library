using System.Data.Common;
using Library.Api.Application.Inventories;
using Library.Api.Application.Librarians;
using Library.Api.Application.Locators;
using Library.Api.Application.Rentals;
using Library.Api.Application.Shared;
using Library.Api.Domain.BookRentals;
using Library.Api.Domain.BookRentals.Users;
using Library.Api.Domain.Inventory;
using Library.Api.Extensions;
using Library.Api.Infrastructure;
using Library.Api.Infrastructure.BookRentals;
using Library.Api.Infrastructure.Clients;
using Library.Api.Infrastructure.Integrations;
using Library.Api.Infrastructure.Inventory;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using BookRentalsIntegrationEventHandler = Library.Api.Infrastructure.BookRentals.IntegrationEventHandler;
using IntegrationEventHandler = Library.Api.Infrastructure.Integrations.IntegrationEventHandler;
using ISystemClock = Library.Api.Domain.Shared.ISystemClock;
using SystemClock = Library.Api.Application.Shared.SystemClock;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration) => this.configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = this.configuration.GetConnectionString("LibraryDb");
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
        services.AddHostedService<IntegrationService>();
        services.AddSingleton<BackgroundWorkerQueue>();

        services.AddKeycloakClient(this.configuration);
        services.AddCors();
        services.AddTransient<IClaimsTransformation, KeycloakClaimsTransformer>();
        services.AddAuth(this.configuration);
        services.AddAuthorization(options => options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build()));

        services.AddMvc();
        services.AddSwagger();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();
        app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpLogging();
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

