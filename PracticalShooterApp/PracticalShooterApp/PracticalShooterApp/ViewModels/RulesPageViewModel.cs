using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Net.Cache;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.Extensions;
using PracticalShooterApp.Helpers;
using PracticalShooterApp.Models;
using PracticalShooterApp.Services;
using Xamarin.Forms;

namespace PracticalShooterApp.ViewModels
{
    [QueryProperty(nameof(InlineIdentifier), nameof(InlineIdentifier))]
    public class RulesPageViewModel : BaseViewModel
    {
        private string _inlineIdentifier;
        private string _sectionName;
        private string _sectionNumeric;
        private string _sectionDescription;

        private Command<object> shareRuleCommand;
        private Command<object> backButtonCommand;
        
        private IShareHelper _shareHelper => DependencyService.Get<IShareHelper>();
        private ISectionService _sectionService => DependencyService.Get<ISectionService>();
        private IRulesService _rulesService => DependencyService.Get<IRulesService>();
        private SettingsClient _settingsClient => DependencyService.Get<SettingsClient>();

        [DataMember(Name = "navigationList")]
        public ObservableCollection<RuleModel> RulesList { get; set; } =
            new ObservableCollection<RuleModel>();

        public RulesPageViewModel()
        {
            
        }

        public Command<object> ShareRuleCommand => this.shareRuleCommand ??
                                           (this.shareRuleCommand = new Command<object>(this.ShareRuleClicked));
        
        public Command<object> BackButtonCommand => this.backButtonCommand ?? 
                                                    (this.backButtonCommand = new Command<object>(this.BackButtonClicked));

        public string InlineIdentifier
        {
            get => _inlineIdentifier;
            set
            {
                _inlineIdentifier = value;
                LoadRules(Int32.Parse(value));
            }
        }

        public string SectionName
        {
            get => _sectionName;
            set => SetProperty(ref _sectionName, value);
        }

        public string SectionNumeric
        {
            get => _sectionNumeric;
            set => SetProperty(ref _sectionNumeric, value);
        }

        public string SectionDescription
        {
            get => _sectionDescription;
            set => SetProperty(ref _sectionDescription, value);
        }

        private async void LoadRules(int sectionId)
        {
            IsBusy = true;

            RulesList.Clear();
            
            var section = await _sectionService.GetSectionBySectionId(sectionId);

            SectionName = section.Name;
            SectionNumeric = section.Numeric;

            if (!string.IsNullOrWhiteSpace(section.Description))
            {
                RulesList.Add(new RuleModel()
                {
                    IsNotDescription = false,
                    IsNotSubRule = false,
                    Description = section.Description
                });
            }
            
            var rules = await _rulesService.GetRulesBySectionId(sectionId);

            foreach (var rule in rules)
            {
                var ruleModel = new RuleModel()
                {
                    Id = rule.Id,
                    Name = rule.Name,
                    Numeric = rule.Numeric,
                    Description = rule.Description,
                    SectionId = rule.SectionId,
                    IsNotDescription = true,
                    IsNotSubRule = true,
                    SubRules = new List<SubRuleModel>()
                };
                
                RulesList.Add(ruleModel);
                
                var subRules = await _rulesService.GetSubRulesByRuleId(rule.Id);

                foreach (var subRule in subRules)
                {
                    var subRuleModel = new RuleModel()
                    {
                        Id = subRule.Id,
                        Name = subRule.Name,
                        Numeric = subRule.Numeric,
                        Description = subRule.Description,
                        IsNotSubRule = false,
                        IsNotDescription = true
                    };
                    
                    RulesList.Add(subRuleModel);
                }
            }
            
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
            await Shell.Current.GoToAsync("..");
        }

        public async Task LookUpSection()
        {
            var section = await _sectionService.GetSectionBySectionNumeric(_sectionNumeric);
            
            if (section == null)
                BackButtonCommand.Execute(null);
            else
                InlineIdentifier = section.Id.ToString();
        }
    }
}
