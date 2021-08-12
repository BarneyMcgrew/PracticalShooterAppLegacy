using PracticalShooterApp.Shared.Enums;
using PracticalShooterApp.Shared.Interfaces.Repositories;
using Xamarin.Forms;

namespace PracticalShooterApp.Shared.Services
{
    public class SettingsService
    {
        private ISettingsRepository _settingsRepository;
        
        public SettingsService()
        {
            _settingsRepository = DependencyService.Get<ISettingsRepository>();
        }

        public void SetSetting(string key, string value)
        {
            _settingsRepository.SetSetting(key, value);
        }

        public object GetSetting(string key)
        {
            return _settingsRepository.GetSetting(key);
        }

        public void ClearSettings()
        {
            _settingsRepository.ClearSettings();
        }
    }
}