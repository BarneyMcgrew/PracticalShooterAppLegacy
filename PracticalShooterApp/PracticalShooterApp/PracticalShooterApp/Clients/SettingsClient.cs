using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Akavache;
using PracticalShooterApp.Enums;

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
        
        
    }
}
