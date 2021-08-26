using System;
using Newtonsoft.Json;
using PracticalShooterApp.Shared.Enums;
using PracticalShooterApp.Shared.Enums.Attributes;
using PracticalShooterApp.Shared.Extensions;
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

        [Setting("Welcome Name")]
        public string WelcomeName
        {
            get
            {
                var value = _settingsRepository.GetSetting($"{Setting.WelcomeName}");

                if (string.IsNullOrWhiteSpace(value))
                {
                    value = Setting.WelcomeName.GetDefaultValue<string>();
                }
                
                return value;
        }
            set => _settingsRepository.SetSetting($"{Setting.WelcomeName}", value);
        }

        [Setting("Show Tooltips")]
        public bool ShowTooltips
        {
            get
            {
                var serialisedValue = _settingsRepository.GetSetting($"{Setting.ShowTooltips}");

                var value = string.IsNullOrWhiteSpace(serialisedValue) 
                    ? Setting.ShowTooltips.GetDefaultValue<bool>() 
                    : JsonConvert.DeserializeObject<bool>(serialisedValue);
                
                return value;
            }
            set => _settingsRepository.SetSetting($"{Setting.ShowTooltips}", JsonConvert.SerializeObject(value));
        }

        [Setting("Show References")]
        public bool ShowReferences
        {
            get
            {
                var serialisedValue = _settingsRepository.GetSetting($"{Setting.ShowReferences}");

                var value = string.IsNullOrWhiteSpace(serialisedValue) 
                    ? Setting.ShowReferences.GetDefaultValue<bool>() 
                    : JsonConvert.DeserializeObject<bool>(serialisedValue);
                
                return value;
            }            
            set => _settingsRepository.SetSetting($"{Setting.ShowReferences}", JsonConvert.SerializeObject(value));
        }

        [Setting("Show All Rulebooks")]
        public bool ShowAllRulebooks
        {
            get
            {
                var serialisedValue = _settingsRepository.GetSetting($"{Setting.ShowAllRulebooks}");

                var value = string.IsNullOrWhiteSpace(serialisedValue)
                    ? Setting.ShowAllRulebooks.GetDefaultValue<bool>()
                    : JsonConvert.DeserializeObject<bool>(serialisedValue);

                return value;
            }
            set => _settingsRepository.SetSetting($"{Setting.ShowAllRulebooks}", JsonConvert.SerializeObject(value));
        }

        [Setting("Default Discipline")]
        public Discipline DefaultDiscipline
        {
            get
            {
                var serialisedValue = _settingsRepository.GetSetting($"{Setting.DefaultDiscipline}");

                var value = string.IsNullOrWhiteSpace(serialisedValue)
                    ? Setting.DefaultDiscipline.GetDefaultValue<Discipline>()
                    : JsonConvert.DeserializeObject<Discipline>(serialisedValue);

                return value;
            }
            set => _settingsRepository.SetSetting($"{Setting.DefaultDiscipline}", JsonConvert.SerializeObject(value));
        }

        [Setting("Always Show Onboarding")]
        public bool ShowOnboarding
        {
            get
            {
                var serialisedValue = _settingsRepository.GetSetting($"{Setting.ShowOnboarding}");

                var value = string.IsNullOrWhiteSpace(serialisedValue)
                    ? Setting.ShowOnboarding.GetDefaultValue<bool>()
                    : JsonConvert.DeserializeObject<bool>(serialisedValue);

                return value;
            }
            set => _settingsRepository.SetSetting($"{Setting.ShowOnboarding}", JsonConvert.SerializeObject(value));
        }
    }
}