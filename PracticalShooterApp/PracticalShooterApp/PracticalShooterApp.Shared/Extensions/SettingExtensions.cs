using System;
using System.Linq;
using PracticalShooterApp.Shared.Enums;
using PracticalShooterApp.Shared.Enums.Attributes;

namespace PracticalShooterApp.Shared.Extensions
{
    public static class SettingExtensions
    {
        public static string GetName(this Setting setting)
        {
            return setting.GetAttribute<SettingAttribute>().Name;
        }

        public static T GetDefaultValue<T>(this Setting setting)
        {
            return (T)setting.GetAttribute<SettingAttribute>().DefaultValue;
        }
    }
}