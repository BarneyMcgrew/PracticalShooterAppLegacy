using PracticalShooterApp.iOS.Repositories;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettingsRepository))]
namespace PracticalShooterApp.iOS.Repositories
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