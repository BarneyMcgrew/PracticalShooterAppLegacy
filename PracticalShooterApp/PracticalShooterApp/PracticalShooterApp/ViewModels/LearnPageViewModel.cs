using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using PracticalShooterApp.Models;
using PracticalShooterApp.Services;
using Xamarin.Forms;

namespace PracticalShooterApp.ViewModels
{
    public class LearnPageViewModel : BaseViewModel
    {
        private Command<object> itemTappedCommand;
        private IChapterService _chapterService => DependencyService.Get<IChapterService>();
        private ISectionService _sectionService => DependencyService.Get<ISectionService>();
        
        public LearnPageViewModel()
        {
            Title = "Learn";

            PopulateSectionsList();
        }
        
        [DataMember(Name = "sectionsList")]
        public ObservableCollection<LearnSectionsModel> SectionsList { get; set; } =
            new ObservableCollection<LearnSectionsModel>();

        public async void PopulateSectionsList()
        {
            IsBusy = true;

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
                        SectionName = $"{section.Numeric} - {section.Name}"
                    };
                    
                    SectionsList.Add(learnSectionModel);
                }
            }

            IsBusy = false;
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
        }
    }
}