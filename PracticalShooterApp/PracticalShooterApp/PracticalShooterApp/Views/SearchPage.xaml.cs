using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Shared.Enums;
using PracticalShooterApp.Shared.Extensions;
using PracticalShooterApp.Shared.Interfaces.Repositories;
using PracticalShooterApp.Shared.Interfaces.Services;
using PracticalShooterApp.Shared.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private readonly IRulebooksService _rulebooksService = DependencyService.Get<IRulebooksService>();
        
        public SearchPage()
        {
            InitializeComponent();
            
            var settingsService = new SettingsService();

            rulebookPicker.ItemsSource = Enum.GetValues(typeof(Discipline));
            rulebookPicker.SelectedItem = settingsService.DefaultDiscipline;
            var rulebookName = settingsService.DefaultDiscipline.GetAttribute<DisplayAttribute>().Name;
            rulebookSelector.Text = $"{rulebookName} rulebook";

            var rulebookTapGesture = new TapGestureRecognizer();
            rulebookTapGesture.Tapped += (s, e) =>
            {
                rulebookPicker.SelectedItem = settingsService.DefaultDiscipline;
                
                rulebookPicker.Focus();
            };
            
            rulebookSelector.GestureRecognizers.Add(rulebookTapGesture);

            rulebookPicker.SelectedIndexChanged += (s, e) =>
            {
                settingsService.DefaultDiscipline = (Discipline)rulebookPicker.SelectedItem;

                rulebookName = settingsService.DefaultDiscipline.GetAttribute<DisplayAttribute>().Name;
                
                rulebookSelector.Text = $"{rulebookName} rulebook";
            };
        }

        private void SearchBar_OnSearchButtonPressed(object sender, EventArgs e)
        {
            var searchBar = (SearchBar) sender;
            var searchText = searchBar.Text;

            var rulesSearchResults = _rulebooksService.SearchRules(searchText);
            var glossariesSearchResults = _rulebooksService.SearchGlossaries(searchText);
            var appendicesSearchResults = _rulebooksService.SearchGlossaries(searchText);
        }
    }
}