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
        private readonly LibraryDriver _driver;

        public LibrarianStepDefinition(ScenarioContext context, LibraryDriver driver)
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
    }
}
