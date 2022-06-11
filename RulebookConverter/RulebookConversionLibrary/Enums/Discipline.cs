using System.ComponentModel.DataAnnotations;
using RulebookConversionLibrary.Enums.Attributes;

namespace RulebookConversionLibrary.Enums
{
    public enum Discipline
    {
        [Display(Name = "Action Air")]
        [Filename("IPSCRulesActionAir")]
        ActionAir,
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