namespace Library.Api.Extensions
{
    using Infrastructure;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.IdentityModel.Tokens;

    public static class AuthExtensions
    {
        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var keycloackSettings = configuration.GetSection(KeycloackSettings.Path).Get<KeycloackSettings>();

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.Authority = $"{keycloackSettings.Authority}{keycloackSettings.LibraryRealmPath}";
                    options.Audience = keycloackSettings.Audience;
                    options.RequireHttpsMetadata = false;
                    options.IncludeErrorDetails = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true
                    };
                    options.TokenValidationParameters.ValidIssuer = options.Authority;
                });

            services.AddKeycloakClient(configuration);
            services.AddTransient<IClaimsTransformation, KeycloakClaimsTransformer>();
            services.AddAuthorization(options => options.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                .RequireAuthenticatedUser().Build()));

            return services;
        }
    }
}
