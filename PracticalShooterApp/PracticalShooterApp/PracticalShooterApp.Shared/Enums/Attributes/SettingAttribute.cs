using System;
using Xamarin.Forms;

namespace PracticalShooterApp.Shared.Enums.Attributes
{
    public class SettingAttribute : Attribute
    {
        public string Name { get; }
        public object DefaultValue { get; }

        public SettingAttribute(string name)
        {
            Name = name;
        }
        
        public SettingAttribute(object defaultValue)
        {
            DefaultValue = defaultValue;
        }
    }
}