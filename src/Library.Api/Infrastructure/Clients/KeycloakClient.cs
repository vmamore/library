namespace Library.Api.Infrastructure.Clients
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Library.Api.Application.Shared;
    using Library.Api.Domain.BookRentals;

    public class KeycloakClient : IAuthenticationClient
    {
        private readonly HttpClient httpClient;

        public KeycloakClient(HttpClient httpClient) => this.httpClient = httpClient;

        public Task<HttpResponseMessage> CreateLocator(Locator locator)
        {
            var body = new
            {
                email = locator.Email.Value,
                emailVerified = true,
                enabled = true,
                firstName = locator.Name.FirstName,
                lastName = locator.Name.LastName,
                username = locator.Cpf.Value,
                attributes = new
                {
                    library_id = locator.Id
                },
                groups = new[]
                {
                    "locators"
                },
                credentials = new[]
                {
                    new { type = "password", value = "test123", temporary = false }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(body));
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return this.httpClient.PostAsync("auth/admin/realms/library/users", content);
        }
    }
}
