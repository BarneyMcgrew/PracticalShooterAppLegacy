using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.DataModels;
using PracticalShooterApp.Enums;
using PracticalShooterApp.Extensions;
using PracticalShooterApp.Helpers;
using PracticalShooterApp.Models;
using PracticalShooterApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LearnPage : ContentPage
    {
        private IActionBarHelper _actionBarHelper => DependencyService.Get<IActionBarHelper>();
        private SettingsClient _settingsClient => DependencyService.Get<SettingsClient>();

        private readonly LearnPageViewModel _viewModel = new LearnPageViewModel();
        
        public LearnPage()
        {
            InitializeComponent();

            
            this.BindingContext = _viewModel;

            SetupListViewGrouping();
        }

        private void SetupListViewGrouping()
        {
            sectionList.DataSource.GroupDescriptors.Add(new Syncfusion.DataSource.GroupDescriptor()
            {
                PropertyName = "ChapterGrouping",
                KeySelector = (object obj1) =>
                {
                    var item = (obj1 as LearnSectionsModel);
                    return item.ChapterGrouping;
                }
            });
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetHeaderSafeArea();
            SetDisciplinePicker();
        }

        private void SetHeaderSafeArea()
        {
            popUpLearn.Margin = new Thickness(0, _actionBarHelper.GetTopSafeArea(), 0,0);
        }

        private void SetDisciplinePicker()
        {
            var currentDiscipline = _settingsClient.CurrentDiscipline;
            disciplinePicker.SelectedItem = currentDiscipline.GetAttribute<DisplayAttribute>().Name;
        }

        private void SearchButton_OnClicked(object sender, EventArgs e)
        {
            
        }

        private void DisciplineButton_OnClicked(object sender, EventArgs e)
        {
            disciplinePicker.Focus();
        }

        private void DisciplinePicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedDisciplineName = disciplinePicker.SelectedItem;
            var selectedDiscipline = Discipline.Handgun;

            switch (selectedDisciplineName)
            {
                case "Handgun":
                    selectedDiscipline = Discipline.Handgun;
                    break;
                case "Action Air":
                    selectedDiscipline = Discipline.ActionAir;
                    break;
                case "Rifle":
                    selectedDiscipline = Discipline.Rifle;
                    break;
                case "Shotgun":
                    selectedDiscipline = Discipline.Shotgun;
                    break;
                case "Mini Rifle":
                    selectedDiscipline = Discipline.MiniRifle;
                    break;
                case "PCC":
                    selectedDiscipline = Discipline.Pcc;
                    break;
            }

            _settingsClient.CurrentDiscipline = selectedDiscipline;

            _viewModel.PopulateSectionsList();
        }
    }
}