using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PracticalShooterApp.Shared.Enums
{
    public enum Language
    {
        [Display(Name = "English",
            ShortName = "en")]
        English,
        // [Display(Name = "French",
        //     ShortName = "fr")]
        // French,
        // [Display(Name = "Spanish",
        //     ShortName = "es")]
        // Spanish,
        // [Display(Name = "German",
        //     ShortName = "de")]
        // German,
        // [Display(Name = "Arabic",
        //     ShortName = "ar")]
        // Arabic
    }
}
