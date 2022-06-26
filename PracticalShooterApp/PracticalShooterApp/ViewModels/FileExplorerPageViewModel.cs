using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using PracticalShooterApp.Models;
using PracticalShooterApp.Views;
using Xamarin.Forms;

namespace PracticalShooterApp.ViewModels
{
    public class FileExplorerPageViewModel : BaseViewModel
    {
        private List<FileExplorerModel> _items = new List<FileExplorerModel>();

        private Command<object> backButtonCommand;
        private Command<object> itemTappedCommand;

        [DataMember(Name = "navigationList")]
        public ObservableCollection<FileExplorerModel> NavigationList { get; set; }
            = new ObservableCollection<FileExplorerModel>();

        public Command<object> BackButtonCommand => this.backButtonCommand ??
                                                    (this.backButtonCommand = new Command<object>(BackButtonClicked));

        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ??
                       (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        public FileExplorerPageViewModel()
        {
            LoadFilesDirectory();
        }

        public async void LoadFilesDirectory()
        {
            IsBusy = true;
            
            _items = new List<FileExplorerModel>()
            {
                new FileExplorerModel()
                {
                    FileName = "IPSC Handgun Rulebook",
                    Description = "This is the main rulebook for IPSC Handgun revised for 2019 onwards.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.English-IPSCRulesHandgun.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "IPSC Action Air Rulebook",
                    Description = "This is the main rulebook for IPSC Action Air revised for 2019 onwards.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.English-IPSCRulesActionAir.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "IPSC Mini Rifle Rulebook",
                    Description = "This is the main rulebook for IPSC Mini Rifle revised for 2019 onwards.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.English-IPSCRulesMiniRifle.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "IPSC Rifle Rulebook",
                    Description = "This is the main rulebook for IPSC Rifle revised for 2019 onwards.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.English-IPSCRulesRifle.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "IPSC Shotgun Rulebook",
                    Description = "This is the main rulebook for IPSC Shotgun revised for 2019 onwards.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.English-IPSCRulesShotgun.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "IPSC Pistol Calibre Carbine Rulebook",
                    Description = "This is the main rulebook for IPSC Pistol Calibre Carbine for 2019 onwards.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.Englihs-IPSCRulesPCC.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "UKPSA Long Barrel Pistol Rules",
                    Description = "This is the main rulebook for UKPSA Long Barrel Pistol as dictated by the UKPSA.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.English-UKPSARulesLongBarrelPistol.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "IPSC Rules Frequently Asked Questions",
                    Description = "This is a small list of frequently asked questions regarding the IPSC Rules for all disciplines.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.IPSCRulesFAQ.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "Target Array Handbook",
                    Description = "This is the main handbook for illustrating target arrays approved for IPSC matches.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.TargetArrayHandbook.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "IPSC vs USPSA Rules",
                    Description = "This article demonstrates the differences between the IPSC and USPSA rulebooks.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.USPSAIPSCRules.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "IROA Range Officer Manual",
                    Description = "This is the IROA manual for Range Officers for sanctioned IPSC matches at any discipline.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.RangeOfficerManual.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "Handgun Equipment Check Manual",
                    Description = "This is the main checklist manual for equipment check at a sanctioned IPSC handgun match",
                    FileDirectory = "PracticalShooterApp.FileExplorer.HandgunEquipmentCheckManual.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "Range Officer Aide Memoire",
                    Description = "This is an aide/memoire to aide a Range Officer with their role at IPSC matches.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.ROAideMemoire.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "10 Reasons to Reshoot Article",
                    Description = "This is an article illustrating ten different reasons why a reshoot must be conducted.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.10ReshootReasons.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "IROA Chief Range Officer Manual",
                    Description = "This is the IROA manual for Chief Range Officers for sanctioned IPSC matches at any discipline.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.ChiefRangeOfficerGuide.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "IROA Range Master Manual",
                    Description = "This is the IROA manual for Range Masters for sanctioned IPSC matches at any discipline.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.RangeMasterGuide.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "IPSC Match Director Handbook",
                    Description = "This is the manual given to match director's to assist with organising and conducting sanctioned IPSC matches.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.MatchDirectorHandbook.pdf"
                },
                new FileExplorerModel()
                {
                    FileName = "IPSC Match Director Checklist",
                    Description = "This is a checklist to accomodate the match director handbook. This checklist will assist with organising and conducting sanctioned IPSC matches.",
                    FileDirectory = "PracticalShooterApp.FileExplorer.MatchDirectorChecklist.pdf"
                }
            };

            NavigationList.Clear();
            _items.ForEach(o => NavigationList.Add(o));

            IsBusy = false;
        }

        private async void BackButtonClicked(object obj)
        {
            await Shell.Current.GoToAsync("..", true);
        }

        private async void NavigateToNextPage(object selectedItem)
        {
            var fileItem = (FileExplorerModel) selectedItem;

            if (fileItem == null)
            {
                return;
            }

            var pageNavigation =
                $"{Shell.Current.CurrentState.Location}/{nameof(PDFPage)}?{nameof(PDFPageViewModel.Title)}={fileItem.FileName}&{nameof(PDFPageViewModel.InlineIdentifier)}={fileItem.FileDirectory}&{nameof(PDFPageViewModel.ShowControls)}=True";

            // do navigation to pdf with controls
            await Shell.Current.GoToAsync(pageNavigation, true);
        }
    }
}