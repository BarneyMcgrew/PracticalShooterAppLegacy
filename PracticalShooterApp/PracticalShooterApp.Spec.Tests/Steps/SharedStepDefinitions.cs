using TechTalk.SpecFlow;

namespace PracticalShooterApp.Spec.Tests.Steps
{
    [Binding]
    public class SharedStepDefinitions
    {
        [Given(@"the language is '(.*)'")]
        public void GivenTheLanguageIs(string english)
        {
            ScenarioContext.StepIsPending();
        }

        [Given(@"the discipline is '(.*)'")]
        public void GivenTheDisciplineIs(string handgun)
        {
            ScenarioContext.StepIsPending();
        }
    }
}