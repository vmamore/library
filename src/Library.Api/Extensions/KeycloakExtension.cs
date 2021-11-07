namespace Library.Api.Extensions
{
    using System;
    using Library.Api.Application.Shared;
    using Library.Api.Infrastructure.Clients;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class KeycloakExtension
    {
        public static IServiceCollection AddKeycloakClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient<IAuthenticationClient, KeycloakClient>("keycloak", client =>
            {
                client.BaseAddress = new Uri(configuration["Keycloak:Authority"]);
            })
            .AddClientAccessTokenHandler("keycloak");
            services.AddAccessTokenManagement(options =>
            {
                options.Client.Clients.Add("keycloak", new IdentityModel.Client.ClientCredentialsTokenRequest
                {
                    Address = configuration["Keycloak:TokenEndpoint"],
                    GrantType = "client_credentials",
                    ClientId = configuration["Keycloak:ClientId"],
                    ClientSecret = configuration["Keycloak:ClientSecret"]
                });
            });

            return services;
        }
    }
}
