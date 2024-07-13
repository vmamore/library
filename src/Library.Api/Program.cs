using Library.Api.Application.Shared;
using Library.Api.Domain.Shared;
using Library.Api.Extensions;
using Library.Api.Infrastructure.Clients;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDatabaseIntegration(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddApplicationServices();
builder.Services.AddIntegrationEvents();
builder.Services.AddAuth(builder.Configuration);
builder.Services.AddCors();
builder.Services.AddMvc();
builder.Services.AddSwagger();
builder.Services.AddBackgroundJobs();
builder.Services.AddSingleton<ISystemClock, SystemClock>();
builder.Services.AddScoped<IHolidayClient, HolidayClient>();

var app = builder.Build();

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

await app.RunAsync();
