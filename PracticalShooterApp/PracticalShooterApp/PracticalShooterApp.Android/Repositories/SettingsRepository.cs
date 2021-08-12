using Xamarin.Essentials;
using PracticalShooterApp.Droid.Repositories;
using PracticalShooterApp.Shared.Interfaces.Repositories;
using Xamarin.Forms;

[assembly: Dependency(typeof(SettingsRepository))]
namespace PracticalShooterApp.Droid.Repositories
{
    public class SettingsRepository : ISettingsRepository
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