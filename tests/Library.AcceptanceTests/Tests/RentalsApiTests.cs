using FluentAssertions;
using Library.AcceptanceTests.Configurations;
using Library.AcceptanceTests.Tests.Configurations;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Library.AcceptanceTests.Tests
{
    public class RentalsApiTests : AcceptanceTest
    {
        public RentalsApiTests(CustomWebApplicationFactory<Startup> factory) : base(factory) { }

        [Fact]
        public async Task PostRental_ShouldReturn_200()
        {
        }
    }
}
