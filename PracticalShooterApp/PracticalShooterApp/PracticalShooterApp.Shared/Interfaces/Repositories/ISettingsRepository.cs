namespace PracticalShooterApp.Shared.Interfaces.Repositories
{
    public interface ISettingsRepository
    {
        void SetSetting(string key, string value);

        string GetSetting(string key);

        void ClearSettings();
    }
}