namespace Library.Api.Extensions;

using Application.Inventories;
using Application.Librarians;
using Application.Locators;
using Application.Rentals;
using Microsoft.Extensions.DependencyInjection;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<BookRentalApplicationService>();
        services.AddScoped<BookApplicationService>();
        services.AddScoped<LocatorApplicationService>();
        services.AddScoped<LibrarianApplicationService>();
        return services;
    }
}
