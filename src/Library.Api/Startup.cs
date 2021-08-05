using System.Data.Common;
using Library.Api.Application.Books;
using Library.Api.Application.Locators;
using Library.Api.Application.Shared;
using Library.Api.Domain.Books;
using Library.Api.Domain.Core;
using Library.Api.Infrastructure;
using Library.Api.Infrastructure.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Npgsql;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        const string connectionString =
            "Server=localhost;Port=5432;Database=library_db;User Id=postgres;Password=post_pwd123;";
        services.AddEntityFrameworkNpgsql();
        services.AddPostgresDbContext<LibraryDbContext>(connectionString);
        services.AddScoped<DbConnection>(c => new NpgsqlConnection(connectionString));
        services.AddHealthChecks()
                .AddNpgSql(connectionString);
        services.AddScoped<IHolidayClient, HolidayClient>();
        services.AddScoped<IUnitOfWork, EfCoreUnitOfWork>();
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
        app.EnsureDatabase();
    }
}

