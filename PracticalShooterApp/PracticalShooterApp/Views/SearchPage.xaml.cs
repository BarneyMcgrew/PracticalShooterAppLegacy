using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.Enums;
using PracticalShooterApp.Extensions;
using PracticalShooterApp.Helpers;
using PracticalShooterApp.ViewModels;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private IActionBarHelper _actionBarHelper => DependencyService.Get<IActionBarHelper>();
        private SettingsClient _settingsClient => DependencyService.Get<SettingsClient>();
        private SearchPageViewModel _viewModel = new SearchPageViewModel();
        public SearchPage()
        {
            InitializeComponent();

            BindingContext = _viewModel;
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
            popupSearch.Margin = new Thickness(0, _actionBarHelper.GetTopSafeArea(), 0, 0);
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
                _viewModel.RulebookName = _settingsClient.CurrentDiscipline.GetAttribute<DisplayAttribute>().Name;

                _viewModel.Search();
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

        private void VisualElement_OnUnfocused(object sender, FocusEventArgs e)
        {
            _viewModel.Search();
        }

    }
}