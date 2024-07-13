using Library.Tests.AcceptanceTests.Drivers;
using Library.Tests.AcceptanceTests.Dtos;
using Library.Tests.AcceptanceTests.Extensions;
using TechTalk.SpecFlow;

namespace Library.Tests.AcceptanceTests.Steps
{
    [Binding]
    public sealed class LocatorStepDefinition
    {
        private readonly ScenarioContext _context;
        private readonly LibraryDriver _driver;

        public LocatorStepDefinition(ScenarioContext context, LibraryDriver driver)
        {
            _context = context;
            _driver = driver;
        }


        [When("creating a locator with")]
        public async Task GivenThatUserIsAuthorized(Table table)
        {
            var dictionaryRequest = TableExtensions.ToDictionary(table);

            var token = _context.Get<AuthenticationToken>("access_token");

            var response = await _driver.CreateLocator(dictionaryRequest, token);

            _context.Set(response.IsSuccessStatusCode, "response");
        }
    }
}
