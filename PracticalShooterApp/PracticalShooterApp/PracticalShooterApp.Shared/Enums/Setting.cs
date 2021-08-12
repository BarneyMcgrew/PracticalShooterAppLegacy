using System.Runtime.CompilerServices;
using PracticalShooterApp.Shared.Enums.Attributes;

namespace PracticalShooterApp.Shared.Enums
{
    public enum Setting
    {
        [Setting("Welcome Name", typeof(string), "Shooter")]
        WelcomeName,
        [Setting("Show Tooltips", typeof(bool), true)]
        ShowTooltips,
        [Setting("Show References", typeof(bool), true)]
        ShowReferences,
        [Setting("Show All Rulebooks", typeof(bool), false)]
        ShowAllRulebooks,
        [Setting("Default Discipline", typeof(Discipline), Discipline.Handgun)]
        DefaultDiscipline
    }
}