namespace Library.Api.Extensions;

using Infrastructure.Integrations.Events;
using Microsoft.Extensions.DependencyInjection;

public static class EventsExtensions
{
    public static IServiceCollection AddEvents(this IServiceCollection services)
    {
        services.AddSingleton<IEventDispatcher, EventDispatcher>();
        services.Scan(s => s.FromExecutingAssembly()
            .AddClasses(c => c.AssignableTo(typeof(IIntegrationEventHandler<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
        return services;
    }
}
