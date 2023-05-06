using System.ComponentModel.DataAnnotations;
using PracticalShooterApp.Enums.Attributes;

namespace PracticalShooterApp.Enums
{
    public enum Discipline
    {
        [Display(Name = "Action Air Handgun")]
        [Filename("IPSCRulesActionAirHandgun")]
        ActionAirHandgun,
        [Display(Name = "Handgun")]
        [Filename("IPSCRulesHandgun")]
        Handgun,
        [Display(Name = "Mini Rifle")]
        [Filename("IPSCRulesMiniRifle")]
        MiniRifle,
        [Display(Name = "Pistol Calibre Carbine")]
        [Filename("IPSCRulesPCC")]
        Pcc,
        [Display(Name = "Rifle")]
        [Filename("IPSCRulesRifle")]
        Rifle,
        [Display(Name = "Shotgun")]
        [Filename("IPSCRulesShotgun")]
        Shotgun,
        [Display(Name= ".22LR Handgun")]
        [Filename("IPSCRules22Handgun")]
        Handgun22,
        [Display(Name = "Action Air PCC")]
        [Filename("IPSCRulesActionAirPCC")]
        ActionAirPCC,
        [Display(Name = "Action Air Rifle")]
        [Filename("IPSCRulesActionAirRifle")]
        ActionAirRifle,
        [Display(Name = "Grand Tournament")]
        [Filename("IPSCRulesGrandTournament")]
        GrandTournament
        // [Display(Name = "Long Barrel Pistol"]
        // [Filename("UKPSARulesLongBarrelPistol")]
        // LongBarrelPistol
    }
}