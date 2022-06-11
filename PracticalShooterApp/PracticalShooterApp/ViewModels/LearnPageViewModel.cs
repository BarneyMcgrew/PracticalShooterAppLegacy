using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using PracticalShooterApp.Clients;
using PracticalShooterApp.Extensions;
using PracticalShooterApp.Models;
using PracticalShooterApp.Services;
using PracticalShooterApp.Views;
using Xamarin.Forms;

namespace PracticalShooterApp.ViewModels
{
    public class LearnPageViewModel : BaseViewModel
    {
        private Command<object> itemTappedCommand;

        private string _rulebookName;
        
        private IChapterService _chapterService => DependencyService.Get<IChapterService>();
        private ISectionService _sectionService => DependencyService.Get<ISectionService>();
        private IAppendixService _appendixService => DependencyService.Get<IAppendixService>();

        private SettingsClient _settingsClient => DependencyService.Get<SettingsClient>();
        
        public LearnPageViewModel()
        {
        }
        
        [DataMember(Name = "sectionsList")]
        public ObservableCollection<LearnSectionsModel> SectionsList { get; set; } =
            new ObservableCollection<LearnSectionsModel>();

        public string RulebookName
        {
            get => _rulebookName;
            set => SetProperty(ref _rulebookName, value);
        }

        public async void PopulateSectionsList()
        {
            IsBusy = true;
            
            SectionsList.Clear();

            RulebookName = _settingsClient.CurrentDiscipline.GetAttribute<DisplayAttribute>().Name;

            var chapterList = await _chapterService.GetChaptersFromCurrentRulebook();

            foreach (var chapter in chapterList)
            {
                var sectionSubList = await _sectionService.GetSectionsByChapterId(chapter.Id);

                foreach (var section in sectionSubList)
                {
                    var learnSectionModel = new LearnSectionsModel()
                    {
                        SectionId = section.Id,
                        ChapterGrouping = $"{chapter.Numeric} - {chapter.Description}", // bug in rulebook converter, no name
                        SectionName = $"{section.Numeric} - {section.Name}",
                        IsAppendix = false
                    };
                    
                    SectionsList.Add(learnSectionModel);
                }
            }

            var appendixList = await _appendixService.GetAppendicesFromCurrentRulebook();

            foreach (var appendix in appendixList)
            {
                var learnSectionmodel = new LearnSectionsModel()
                {
                    SectionId = appendix.Id,
                    ChapterGrouping = "12.1 - Appendices",
                    SectionName = $"{appendix.Name} - {appendix.Description}",
                    IsAppendix = true
                };
                
                SectionsList.Add(learnSectionmodel);
            }

            IsBusy = false;
        }

        public async void GoToSearchPage()
        {
            await Shell.Current.GoToAsync($"{Shell.Current.CurrentState.Location}/{nameof(SearchPage)}");
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

            var model = (LearnSectionsModel)eventArgs.ItemData;
            if (model.IsAppendix)
            {
                // go to appendix page
                await Shell.Current.GoToAsync(
                    $"{Shell.Current.CurrentState.Location}/{nameof(PDFPage)}?{nameof(PDFPageViewModel.Title)}={model.SectionName}&{nameof(PDFPageViewModel.InlineIdentifier)}=PracticalShooterApp.PDFs.Appendix {model.SectionId}.pdf", true);
            }
            else
            {
                await Shell.Current.GoToAsync($"{Shell.Current.CurrentState.Location}/{nameof(RulesPage)}?{nameof(RulesPageViewModel.InlineIdentifier)}={model.SectionId}", true);
            }
        }
    }
}