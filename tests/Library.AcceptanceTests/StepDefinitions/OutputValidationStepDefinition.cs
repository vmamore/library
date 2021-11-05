using FluentAssertions;
using TechTalk.SpecFlow;

namespace Library.AcceptanceTests.StepDefinitions
{
    [Binding]
    public class OutputValidationStepDefinition
    {
        private readonly ScenarioContext _context;

        public OutputValidationStepDefinition(ScenarioContext context)
        {
            _context = context;
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
