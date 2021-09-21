using Library.AcceptanceTests.Tests.Configurations;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Library.AcceptanceTests.Configurations
{
    public abstract class AcceptanceTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
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

        public async Task<HttpResponseMessage> CreateBookInInventory(StringContent content) => await Client.PostAsync(INVENTORY_POST_BOOK, content);

        public async Task<HttpResponseMessage> GetAllBooksInRentals(int page = 1, string title = null) => await Client.GetAsync($"{RENTALS_GET_ALL_BOOKS}?page={page}&title={title}");

        public async Task<HttpResponseMessage> CreateLibrarian(StringContent content) => await Client.PostAsync(RENTALS_POST_Librarians, content);


    }
}
