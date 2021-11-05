using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Library.AcceptanceTests.StepDefinitions
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

            async Task<AuthenticationToken> FetchAuthenticationToken(Roles role)
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
                        new KeyValuePair<string, string>("client_id", "library-api"),
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", "vmamore"),
                        new KeyValuePair<string, string>("password", "mamore123")
                    },
                    _ => throw new ArgumentOutOfRangeException("Not ready for other Roles")
                };
        }
    }

    public enum Roles
    {
        Locator = 1,
        Librarian
    }

    public class AuthenticationToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }
        private DateTime CreatedAt { get; } = DateTime.Now;

        public DateTime AccessTokenExpireTime
        {
            get
            {
                return CreatedAt.AddSeconds(ExpiresIn);
            }
        }
    }
}
