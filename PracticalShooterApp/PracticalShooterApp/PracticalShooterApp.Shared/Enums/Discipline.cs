using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PracticalShooterApp.Shared.Enums
{
    public enum Discipline
    {
        [Display(Name = "Action Air")]
        ActionAir,
        [Display(Name = "Handgun")]
        Handgun,
        [Display(Name = "Mini Rifle")]
        MiniRifle,
        [Display(Name = "Pistol Calibre Carbine")]
        Pcc,
        [Display(Name = "Rifle")]
        Rifle,
        [Display(Name = "Shotgun")]
        Shotgun,
        // [Display(Name = "Long Barrel Pistol"]
        // [Filename("UKPSARulesLongBarrelPistol")]
        // LongBarrelPistol
    }
}
