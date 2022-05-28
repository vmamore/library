namespace Library.Api.Extensions;

using Application.Shared;
using Infrastructure.BackgroundJobs;
using Infrastructure.Integrations;
using Microsoft.Extensions.DependencyInjection;

public static class BackgroundJobExtensions
{
    public static IServiceCollection AddBackgroundJobs(this IServiceCollection services)
    {
        services.AddScoped<IIntegrationEventMapper, IntegrationEventMapper>();
        services.AddSingleton<MessagesChannel>();
        services.AddSingleton<IDispatcher, Dispatcher>();
        services.AddHostedService<EventsProcessor>();
        return services;
    }

}
