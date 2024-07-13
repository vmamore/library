using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Library.Tests.AcceptanceTests.Dtos;

namespace Library.Tests.AcceptanceTests.Drivers
{
    public class LibraryDriver
    {
        public async Task<HttpResponseMessage> CreateLibrarian(Dictionary<string, string> dictionaryRequest, AuthenticationToken token)
        {
            var requestBody = JsonSerializer.Serialize(dictionaryRequest);

            using var request = new HttpRequestMessage(HttpMethod.Post, "librarians");
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            return await new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000")
            }.SendAsync(request);
        }

        public async Task<HttpResponseMessage> CreateLocator(Dictionary<string, string> dictionaryRequest, AuthenticationToken token)
        {
            var requestBody = JsonSerializer.Serialize(dictionaryRequest);

            using var request = new HttpRequestMessage(HttpMethod.Post, "locators");
            request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token.AccessToken);
            return await new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5000")
            }.SendAsync(request);
        }
    }
}
