using System;
using Xamarin.Forms;

namespace PracticalShooterApp.Shared.Enums.Attributes
{
    public class SettingAttribute : Attribute
    {
        public string Name { get; }
        public Type Type { get; }
        public object DefaultValue { get; }

        public SettingAttribute(string name,Type type, object defaultValue)
        {
            Name = name;
            Type = type;
            DefaultValue = defaultValue;
        }
    }
}