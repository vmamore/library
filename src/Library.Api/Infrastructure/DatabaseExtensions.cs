namespace Library.Api.Infrastructure
{
    using System.Linq;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class DatabaseExtensions
    {
        public static IApplicationBuilder EnsureDatabase(this IApplicationBuilder builder)
        {
            using var scope = builder.ApplicationServices.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
            EnsureContextIsMigrated(dbContext);
            return builder;
        }

        private static void EnsureContextIsMigrated(DbContext context)
        {
            if (!context.Database.EnsureCreated())
            {
                var migrations = context.Database.GetPendingMigrations();
                if (migrations.Any())
                    context.Database.Migrate();
            }
        }

        public static IServiceCollection AddPostgresDbContext<T>(this IServiceCollection services,
            string connectionString) where T : DbContext
        {
            services.AddDbContext<T>(options => options.UseNpgsql(connectionString));
            return services;
        }
    }
}
