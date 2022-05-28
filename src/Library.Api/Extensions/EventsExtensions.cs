namespace Library.Api.Extensions;

using Application.Rentals;
using Infrastructure.Integrations;
using Infrastructure.Integrations.Events;
using Microsoft.Extensions.DependencyInjection;

public static class EventsExtensions
{
    public static IServiceCollection AddIntegrationEvents(this IServiceCollection services)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        services.AddScoped<IIntegrationEventHandler<BookRegistered>, BookRegisteredIntegrationEventHandler>();
        return services;
    }
}
