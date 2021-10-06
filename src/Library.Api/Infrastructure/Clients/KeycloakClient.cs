namespace Library.Api.Infrastructure.Clients
{
    using System.Net.Http;
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
                email = "vinicius.mamore@gmail.com",
                emailVerified = true,
                enabled = true,
                firstName = locator.Name.FirstName,
                lastName = locator.Name.LastName,
                username = locator.Cpf.Value,
                credentials = new[]
                {
                    new
                    {
                        type = "password",
                        secretData = new
                        {
                            value = "teste",
                            salt = "teste"
                        },
                        credentialData = new
                        {
                            algorithm = "sha512",
                            hashIterations = 1
                        }
                    }
                }
            };

            var content = new StringContent(JsonSerializer.Serialize(body));

            return this.httpClient.PostAsync("/library-api/users", content);

        }
    }
}
