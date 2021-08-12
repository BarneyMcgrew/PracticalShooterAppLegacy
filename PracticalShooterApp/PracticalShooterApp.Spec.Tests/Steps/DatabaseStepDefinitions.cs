using TechTalk.SpecFlow;

namespace PracticalShooterApp.Spec.Tests.Steps
{
    [Binding, Scope(Tag = "DatabaseTests")]
    public class DatabaseStepDefinitions
    {
        [When(@"the retrieving the chapters from the database")]
        public void WhenTheRetrievingTheChaptersFromTheDatabase()
        {
            ScenarioContext.StepIsPending();
        }

        [Then(@"the chapters should be populated")]
        public void ThenTheChaptersShouldBePopulated()
        {
            ScenarioContext.StepIsPending();
        }
    }
}