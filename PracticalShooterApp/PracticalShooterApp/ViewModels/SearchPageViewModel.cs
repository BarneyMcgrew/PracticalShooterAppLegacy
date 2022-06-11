using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.Extensions;
using PracticalShooterApp.Helpers;
using PracticalShooterApp.Models;
using PracticalShooterApp.Services;
using PracticalShooterApp.Views;
using Xamarin.Forms;

namespace PracticalShooterApp.ViewModels
{
    public class SearchPageViewModel : BaseViewModel
    {
        private string _rulebookName;
        private string _searchParameters;

        private Command<object> shareRuleCommand;
        private Command<object> backButtonCommand;
        private Command<object> itemTappedCommand;

        private IShareHelper _shareHelper => DependencyService.Get<IShareHelper>();
        private ISearchService _searchService => DependencyService.Get<ISearchService>();
        private SettingsClient _settingsClient => DependencyService.Get<SettingsClient>();

        // Should consider making a SearchModel
        [DataMember(Name = "navigationList")]
        public ObservableCollection<SearchModel> SearchList { get; set; } =
            new ObservableCollection<SearchModel>();

        public string RulebookName
        {
            get => _rulebookName;
            set => SetProperty(ref _rulebookName, value);
        }
        
        public string SearchParameters
        {
            get => _searchParameters;
            set => SetProperty(ref _searchParameters, value);
        }

        public SearchPageViewModel()
        {
            UpdateDisciplineName();
        }

        private void UpdateDisciplineName()
        {
            RulebookName = _settingsClient.CurrentDiscipline.GetAttribute<DisplayAttribute>().Name;
        }

        public Command<object> ShareRuleCommand => this.shareRuleCommand ??
                                                   (this.shareRuleCommand = 
                                                       new Command<object>(this.ShareRuleClicked));

        public Command<object> BackButtonCommand => this.backButtonCommand ??
                                                    (this.backButtonCommand =
                                                        new Command<object>(this.BackButtonClicked));

        public async Task Search()
        {
            RulebookName = _settingsClient.CurrentDiscipline.GetAttribute<DisplayAttribute>().Name;

            
            // make search parameters
            if (_searchParameters.Length < 3)
                return;

            IsBusy = true;

            var results = await _searchService.SearchRules(_searchParameters);

            SearchList.Clear();

            results.ForEach(result => SearchList.Add(result));

            IsBusy = false;
        }

        private void ShareRuleClicked(object obj)
        {
            var ruleModel = (RuleModel)obj;
            
            var text =
                $"{_settingsClient.CurrentDiscipline.GetAttribute<DisplayAttribute>().Name} IPSC Rules" +
                $"{Environment.NewLine}{Environment.NewLine}" +
                $"Rule: {ruleModel.Numeric} {ruleModel.Name}" +
                $"{Environment.NewLine}{Environment.NewLine}" +
                $"{ruleModel.Description}" +
                $"{Environment.NewLine}{Environment.NewLine}" +
                $"Shared via" +
                $"{Environment.NewLine}" +
                $"The Practical Shooter App"; // need to add url to app stores
            
            _shareHelper.ShareText($"Share rule {ruleModel.Numeric} {ruleModel.Name}", text);
        }
        
        private async void BackButtonClicked(object obj)
        {
            await Shell.Current.GoToAsync("..", true);
        }
        
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }
        
        private async void NavigateToNextPage(object selectedItem)
        {
            var eventArgs = (Syncfusion.ListView.XForms.ItemTappedEventArgs)selectedItem;
            if (eventArgs.ItemType == Syncfusion.ListView.XForms.ItemType.GroupHeader)
                return;

            var model = (SearchModel)eventArgs.ItemData;
            
            if (model.SectionName == "Appendices")
            {
                // go to appendix page
                 await Shell.Current.GoToAsync(
                     $"{Shell.Current.CurrentState.Location}/{nameof(PDFPage)}?{nameof(PDFPageViewModel.Title)}={model.Numeric}&{nameof(PDFPageViewModel.InlineIdentifier)}=PracticalShooterApp.PDFs.Appendix {model.Id}.pdf&{nameof(PDFPageViewModel.SearchCriteria)}={SearchParameters}", true);
            }
            else if (model.SectionName == "Glossary")
            {
                // go to glossary? not worth it!
            }
            else
            {
                await Shell.Current.GoToAsync($"{Shell.Current.CurrentState.Location}/{nameof(RulesPage)}?{nameof(RulesPageViewModel.InlineIdentifier)}={model.SectionId}&{nameof(RulesPageViewModel.SearchRuleNumeric)}={model.Numeric}", true);
            }
        }
    }
}
