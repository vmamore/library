namespace Library.Tests.FunctionalTests
{
    public class RentalsApiTests : BaseTest
    {
        public RentalsApiTests(ApiFactory factory) : base(factory) { }

        [Fact]
        public async Task PostRental_ShouldReturn_200()
        {
            // Act
            var createBookPayload = CreatePayload(new { Title = "Fight Club", Author = "Chuck Palahniuk", ReleasedYear = "1996", ISBN = "12345678910", PhotoUrl = "www.photo.com/1", Pages = 208, Version = 1 });
            var createdBookResponse = await CreateBookInInventory(createBookPayload);
            createdBookResponse.EnsureSuccessStatusCode();

            var createLibrarianPayload = CreatePayload(new { FirstName = "Vinicius", LastName = "Mamoré", BirthDate = new DateTime(1997, 10, 25), CPF = "00011122233", Street = "Rua Alegria", City = "São Paulo", Number = "200", District = "São José" });
            var createLibrarianResponse = await CreateLibrarian(createLibrarianPayload);
            createLibrarianResponse.EnsureSuccessStatusCode();
        }
    }
}
