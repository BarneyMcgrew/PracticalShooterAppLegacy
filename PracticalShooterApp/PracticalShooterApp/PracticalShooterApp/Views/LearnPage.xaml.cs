using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.DataModels;
using PracticalShooterApp.Enums;
using PracticalShooterApp.Services.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LearnPage : ContentPage
    {
        private IChapterService _chapterService => DependencyService.Get<IChapterService>();
        private SettingsClient _settingsClient => DependencyService.Get<SettingsClient>();
        
        public LearnPage()
        {
            InitializeComponent();

            var task = Task.Run(async () => await _chapterService.GetChaptersFromCurrentRulebook());
            var result = task.Result;
        }
    }
}