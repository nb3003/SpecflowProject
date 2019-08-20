using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;

namespace SpecflowTestFramework.Stepdefinition
{
    [Binding]
    public sealed class StepDefinition1
    {
        // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

        private readonly ScenarioContext context;

        public StepDefinition1(ScenarioContext injectedContext)
        {
            context = injectedContext;
        }

        [Given(@"I have '(.*)' test case written")]
        public void GivenIHaveTestCaseWritten(int number)
        {
            Console.WriteLine(number);
        }

        [Given(@"I set date as '(.*)'")]
        public void GivenISetDateAs(DateTimeOffset date)
        {
            Console.WriteLine(date.ToString());
        }
    }
}
