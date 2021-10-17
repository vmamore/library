using FluentAssertions;
using Library.AcceptanceTests.Drivers;
using Library.AcceptanceTests.Support;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace Library.AcceptanceTests.StepDefinitions
{
    [Binding]
    public sealed class LibrarianStepDefinition
    {
        private readonly ScenarioContext _context;
        private readonly LibrarianDriver _driver;

        public LibrarianStepDefinition(ScenarioContext context, LibrarianDriver driver)
        {
            _context = context;
            _driver = driver;
        }


        [When("creating a librarian with")]
        public async Task GivenThatUserIsAuthorized(Table table)
        {
            var dictionaryRequest = TableExtensions.ToDictionary(table);

            var token = _context.Get<AuthenticationToken>("access_token");

            var response = await _driver.CreateLibrarian(dictionaryRequest, token);

            _context.Set(response.IsSuccessStatusCode, "response");
        }

        [Then("should create with (success|false)")]
        public void ThenResponseShouldBeAsExpected(string expectedResultString)
        {
            var expectedResult = expectedResultString.Equals("success");

            var response = _context.Get<bool>("response");

            response.Should().Be(expectedResult);
        }
    }
}
