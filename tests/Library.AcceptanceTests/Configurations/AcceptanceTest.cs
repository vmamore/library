using Library.AcceptanceTests.Tests.Configurations;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Xunit;

namespace Library.AcceptanceTests.Configurations
{
    public abstract class AcceptanceTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private static AuthenticationToken AuthenticationToken { get; set; }

        private const string INVENTORY_POST_BOOK = "inventory/books";

        private const string RENTALS_POST_Librarians = "librarians";

        private const string RENTALS_GET_ALL_BOOKS = "rentals/books/all";

        private readonly CustomWebApplicationFactory<Startup> _factory;

        public AcceptanceTest(CustomWebApplicationFactory<Startup> factory) => _factory = factory;

        protected HttpClient Client
        {
            get
            {
                return _factory.CreateClient();
            }
        }

        protected StringContent CreatePayload(object obj)
        {
            var payload = JsonSerializer.Serialize(obj);
            return new StringContent(payload, Encoding.UTF8, "application/json");
        }

        public async Task<HttpResponseMessage> CreateBookInInventory(StringContent content)
        {
            var token = await GetAuthToken();
            using var request = new HttpRequestMessage(HttpMethod.Post, INVENTORY_POST_BOOK);
            request.Content = content;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return await Client.SendAsync(request);
        }

        public async Task<HttpResponseMessage> GetAllBooksInRentals(int page = 1, string title = null) => await Client.GetAsync($"{RENTALS_GET_ALL_BOOKS}?page={page}&title={title}");

        public async Task<HttpResponseMessage> CreateLibrarian(StringContent content) => await Client.PostAsync(RENTALS_POST_Librarians, content);

        public async Task<string> GetAuthToken()
        {
            if (AuthenticationToken == null)
            {
                AuthenticationToken = await FetchAuthenticationToken();
                return AuthenticationToken.AccessToken;
            }

            if (AuthenticationToken.AccessTokenExpireTime < DateTime.Now)
            {
                AuthenticationToken = await FetchAuthenticationToken();
                return AuthenticationToken.AccessToken;
            }

            return AuthenticationToken.AccessToken;

            async Task<AuthenticationToken> FetchAuthenticationToken()
            {
                var endpoint = "http://localhost:8080/auth/realms/library/protocol/openid-connect/token";
                var response = await new HttpClient().PostAsync(endpoint, new FormUrlEncodedContent(GetUserAuthenticationParameters()));
                return await JsonSerializer.DeserializeAsync<AuthenticationToken>(await response.Content.ReadAsStreamAsync());
            }

            List<KeyValuePair<string, string>> GetUserAuthenticationParameters()
            {
                return new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("client_id", "library-api"),
                    new KeyValuePair<string, string>("grant_type", "password"),
                    new KeyValuePair<string, string>("username", "vmamore"),
                    new KeyValuePair<string, string>("password", "mamore123")
                };
            }
        }

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
