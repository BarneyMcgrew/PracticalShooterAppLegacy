using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.Enums;
using PracticalShooterApp.Extensions;
using PracticalShooterApp.Helpers;
using PracticalShooterApp.Models;
using PracticalShooterApp.ViewModels;
using Syncfusion.ListView.XForms;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ScrollToPosition = Syncfusion.ListView.XForms.ScrollToPosition;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RulesPage : ContentPage
    {
        private IActionBarHelper _actionBarHelper => DependencyService.Get<IActionBarHelper>();
        private SettingsClient _settingsClient => DependencyService.Get<SettingsClient>();
        private RulesPageViewModel _viewModel = new RulesPageViewModel();

        public RulesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel;
            rulesList.PropertyChanged += RulesListOnPropertyChanged;
        }

        private void RulesListOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var selectedIndex = _viewModel.RulesList.ToList().FindIndex(o => o.IsSearched);
            
            rulesList.LayoutManager.ScrollToRowIndex(selectedIndex, ScrollToPosition.Center, true);
        }

        protected override bool OnBackButtonPressed()
        {
            var backButtonCommand = _viewModel.BackButtonCommand;
            backButtonCommand.Execute(null);
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetHeaderSafeArea();
            SetDisciplinePicker();
        }

        private void SetHeaderSafeArea()
        {
            popupRules.Margin = new Thickness(0, _actionBarHelper.GetTopSafeArea(), 0,0);
        }

        private void DisciplineButton_OnClicked(object sender, EventArgs e)
        {
            disciplinePicker.Focus();
        }

        private async void DisciplinePicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedDisciplineName = (String)disciplinePicker.SelectedItem;
            var selectedDiscipline = Discipline.Handgun;
            
            switch (selectedDisciplineName)
            {
                case ".22LR Handgun":
                    selectedDiscipline = Discipline.Handgun22;
                    break;
                case "Handgun":
                    selectedDiscipline = Discipline.Handgun;
                    break;
                case "Action Air Handgun":
                    selectedDiscipline = Discipline.ActionAirHandgun;
                    break;
                case "Action Air PCC":
                    selectedDiscipline = Discipline.ActionAirPCC;
                    break;
                case "Action Air Rifle":
                    selectedDiscipline = Discipline.ActionAirRifle;
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

            if (selectedDiscipline != _settingsClient.CurrentDiscipline)
            {
                _settingsClient.CurrentDiscipline = selectedDiscipline;

                await _viewModel.LookUpSection();
            }
        }
        
        private void SetDisciplinePicker()
        {
            disciplinePicker.SelectedItem = _settingsClient.CurrentDiscipline.GetAttribute<DisplayAttribute>().Name;
        }

        private void SfButton_OnClicked(object sender, EventArgs e)
        {
            var button = (SfButton)sender;
            
            _viewModel.ShareRuleCommand.Execute(button.BindingContext);
        }
    }
}