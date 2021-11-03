using System.Runtime.CompilerServices;
using PracticalShooterApp.Shared.Enums.Attributes;

namespace PracticalShooterApp.Shared.Enums
{
    public enum Setting
    {
        [Setting(defaultValue: "Shooter")]
        WelcomeName,
        [Setting(defaultValue: true)]
        ShowTooltips,
        [Setting(defaultValue: true)]
        ShowReferences,
        [Setting(defaultValue: false)]
        ShowAllRulebooks,
        [Setting(defaultValue: Discipline.Handgun)]
        DefaultDiscipline,
        [Setting(defaultValue: Language.English)]
        DefaultLanguage,
        [Setting(defaultValue: true)]
        ShowOnboarding
    }
}