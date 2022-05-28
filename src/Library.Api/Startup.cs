using Library.Api.Application.Shared;
using Library.Api.Extensions;
using Library.Api.Infrastructure.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ISystemClock = Library.Api.Domain.Shared.ISystemClock;
using SystemClock = Library.Api.Application.Shared.SystemClock;

public class Startup
{
    private readonly IConfiguration configuration;

    public Startup(IConfiguration configuration) => this.configuration = configuration;

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDatabaseIntegration(this.configuration);
        services.AddRepositories();
        services.AddApplicationServices();
        services.AddIntegrationEvents();
        services.AddAuth(this.configuration);
        services.AddCors();
        services.AddMvc();
        services.AddSwagger();
        services.AddBackgroundJobs();
        services.AddSingleton<ISystemClock, SystemClock>();
        services.AddScoped<IHolidayClient, HolidayClient>();
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

