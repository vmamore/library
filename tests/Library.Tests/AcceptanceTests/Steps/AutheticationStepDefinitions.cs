using System.Text.Json;
using Library.Tests.AcceptanceTests.Dtos;
using TechTalk.SpecFlow;

namespace Library.Tests.AcceptanceTests.Steps
{
    [Binding]
    public sealed class AutheticationStepDefinitions
    {
        private readonly ScenarioContext _context;

        public AutheticationStepDefinitions(ScenarioContext context)
            => _context = context;

        [Given("a (Librarian|Locator) user")]
        public async Task GivenALibrarianUser(string userRole)
        {
            var role = Enum.Parse<Roles>(userRole);

            await SetAuthTokenInContext(role);
        }

        public async Task SetAuthTokenInContext(Roles role)
        {
            var foundToken = _context.TryGetValue<AuthenticationToken>("access_token", out var authenticationToken);

            if (!foundToken)
            {
                authenticationToken = await FetchAuthenticationToken(role);
                _context.Set(authenticationToken, "access_token");
                return;
            }

            if (authenticationToken.AccessTokenExpireTime < DateTime.Now)
            {
                authenticationToken = await FetchAuthenticationToken(role);
                _context.Set(authenticationToken, "access_token");
                return;
            }

            _context.Set(authenticationToken, "access_token");

            async Task<AuthenticationToken?> FetchAuthenticationToken(Roles role)
            {
                var endpoint = "http://localhost:8080/auth/realms/library/protocol/openid-connect/token";
                var response = await new HttpClient().PostAsync(endpoint, new FormUrlEncodedContent(GetUserAuthenticationParameters(role)));
                if (!response.IsSuccessStatusCode) throw new InvalidOperationException("Error when fetching access token.");
                return await JsonSerializer.DeserializeAsync<AuthenticationToken>(await response.Content.ReadAsStreamAsync());
            }

            List<KeyValuePair<string, string>> GetUserAuthenticationParameters(Roles role)
                => role switch
                {
                    Roles.Librarian => new List<KeyValuePair<string, string>>() {
                        new("client_id", "library-api"),
                        new("grant_type", "password"),
                        new("username", "vmamore"),
                        new("password", "mamore123")
                    },
                    _ => throw new ArgumentOutOfRangeException("Not ready for other Roles")
                };
        }
    }
}
