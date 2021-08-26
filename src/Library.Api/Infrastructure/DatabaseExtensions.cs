namespace Library.Api.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;

    public static class DatabaseExtensions
    {
        public static IServiceCollection AddPostgresDbContext<T>(this IServiceCollection services,
            string connectionString) where T : DbContext
        {
            services.AddDbContext<T>(options =>
            {
                options.UseNpgsql(connectionString);
                options.EnableSensitiveDataLogging();
            });
            return services;
        }
    }
}
