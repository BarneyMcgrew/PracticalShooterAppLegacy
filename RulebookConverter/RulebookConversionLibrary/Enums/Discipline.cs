using System.ComponentModel.DataAnnotations;
using RulebookConversionLibrary.Enums.Attributes;

namespace RulebookConversionLibrary.Enums
{
    public enum Discipline
    {
        [Display(Name = "Action Air Handgun")]
        [Filename("IPSCRulesActionAirHandgun")]
        ActionAirHandgun,
        [Display(Name = "Action Air PCC")]
        [Filename("IPSCRulesActionAirPCC")]
        ActionAirPCC,
        [Display(Name = "Action Air Rifle")]
        [Filename("IPSCRulesActionAirRifle")]
        ActionAirRifle,
        [Display(Name= ".22LR Handgun")]
        [Filename("IPSCRules22Handgun")]
        Handgun22,
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
        // [Display(Name = "Long Barrel Pistol"]
        // [Filename("UKPSARulesLongBarrelPistol")]
        // LongBarrelPistol
    }
}