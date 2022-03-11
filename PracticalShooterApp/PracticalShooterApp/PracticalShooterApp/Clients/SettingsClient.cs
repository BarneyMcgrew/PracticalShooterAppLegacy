using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using PracticalShooterApp.Enums;
using PracticalShooterApp.Models;

namespace PracticalShooterApp.Clients
{
    public class SettingsClient : ISettingsClient
    {
        private readonly IBlobCache _settingsCache;
        
        public SettingsClient()
        {
            _settingsCache = BlobCache.UserAccount;
        }

        public Language CurrentLanguage
        {
            get
            {
                var task = Task.Run(async () =>
                    await _settingsCache.GetOrCreateObject(
                        nameof(CurrentLanguage), 
                        () => Enums.Language.English, 
                        null));

                return task.Result;
            }
            set
            {
                Task.Run(async () =>
                    await _settingsCache.InsertObject(
                        nameof(CurrentLanguage), 
                        value, 
                        null));
            }
        }

        public Discipline CurrentDiscipline
        {
            get
            {
                var task = Task.Run(async () =>
                    await _settingsCache.GetOrCreateObject(
                        nameof(CurrentDiscipline), 
                        () => Enums.Discipline.Handgun, 
                        null));

                return task.Result;
            }
            set
            {
                Task.Run(async () =>
                    await _settingsCache.InsertObject(
                        nameof(CurrentDiscipline),
                        value,
                        null));
            }
        }

        public List<String> SelectedRegions
        {
            get
            {
                var task = Task.Run(async () =>
                    await _settingsCache.GetOrCreateObject(
                        nameof(SelectedRegions),
                        () => new List<String>(),
                        null));

                return task.Result;
            }
            set
            {
                Task.Run(async () =>
                    await _settingsCache.InsertObject(
                        nameof(SelectedRegions),
                        value,
                        null));
            }
        }
        
        public List<String> SelectedCountries
        {
            get
            {
                var task = Task.Run(async () =>
                    await _settingsCache.GetOrCreateObject(
                        nameof(SelectedCountries),
                        () => new List<String>(),
                        null));

                return task.Result;
            }
            set
            {
                Task.Run(async () =>
                    await _settingsCache.InsertObject(
                        nameof(SelectedCountries),
                        value,
                        null));
            }
        }
    }
}
