using Library.AcceptanceTests.StepDefinitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library.AcceptanceTests.Drivers
{
    public class LibrarianDriver
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
    }
}
