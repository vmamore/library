namespace Library.Api.Extensions
{
    using System.Data.Common;
    using Domain.BookRentals;
    using Domain.BookRentals.Users;
    using Domain.Inventory;
    using Infrastructure.BookRentals;
    using Infrastructure.Inventory;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Npgsql;

    public static class DatabaseExtensions
    {
        public static IServiceCollection AddDatabaseIntegration(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("LibraryDb");
            services.AddEntityFrameworkNpgsql();
            services.AddPostgresDbContext<InventoryDbContext>(connectionString);
            services.AddPostgresDbContext<BookRentalDbContext>(connectionString);
            services.AddScoped<DbConnection>(c => new NpgsqlConnection(connectionString));
            services.AddHealthChecks().AddNpgSql(connectionString);
            return services;
        }

        public static IServiceCollection AddPostgresDbContext<T>(this IServiceCollection services,
            string connectionString) where T : DbContext
        {
            services.AddDbContext<T>(options =>
            {
                options.UseNpgsql(connectionString);
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()));
            });
            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IBookRentalRepository, BookRentalRepository>();
            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<ILocatorRepository, LocatorRepository>();
            services.AddScoped<ILibrarianRepository, LibrarianRepository>();
            return services;
        }
    }
}
