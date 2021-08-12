using Xamarin.Essentials;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using PracticalShooterApp.Droid.Repositories;
using PracticalShooterApp.Shared.Interfaces.Repositories;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettingsRepository))]
namespace PracticalShooterApp.Droid.Repositories
{
    public class SettingsRepository
    {
        public void SetSetting(string key, string value)
        {
            Preferences.Set(key, value);
        }

        public string GetSetting(string key)
        {
            return Preferences.Get(key, "");
        }

        public void ClearSettings()
        {
            Preferences.Clear();
        }
    }
}