using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.Models;
using PracticalShooterApp.Services;
using PracticalShooterApp.Views;
using SQLitePCL;
using Syncfusion.XForms.Buttons;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using NavigationModel = PracticalShooterApp.Models.NavigationModel;

namespace PracticalShooterApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        #region Fields

        private Command<object> itemTappedCommand;
        private Command<object> filterCheckedCommand;

        private List<HomeTilesModel> _navigationList =
            new List<HomeTilesModel>();

        private IHomeTilesService _homeTilesService => DependencyService.Get<IHomeTilesService>();
        private IBrowserService _browserService => DependencyService.Get<IBrowserService>();
        private SettingsClient _settingsClient => DependencyService.Get<SettingsClient>();
        
        #endregion

        public HomePageViewModel()
        {
            Title = "Home";
                        
            PopulateHomeTiles();
        }

        #region Properties

        public Command<object> FilterCheckedCommand
        {
            get
            {
                return this.filterCheckedCommand ??
                       (this.filterCheckedCommand = new Command<object>(this.FilterChecked));
            }
        }
        
        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? 
                       (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        /// <summary>
        /// Gets or sets a collection of values to be displayed in the Navigation list page.
        /// </summary>
        [DataMember(Name = "navigationList")]
        public ObservableCollection<HomeTilesModel> NavigationList { get; set; } =
            new ObservableCollection<HomeTilesModel>();

        [DataMember(Name = "regionFilter")]
        public ObservableCollection<RegionFilter> RegionFilter { get; set; } =
            new ObservableCollection<RegionFilter>();

        #endregion

        #region Methods

        private async void FilterChecked(object sender)
        {
            var selectedRegions = new List<string>();
            
            foreach (var filter in RegionFilter)
            {
                if (filter.RegionSelected)
                {
                    selectedRegions.Add(filter.RegionName);
                }
            }

            _settingsClient.SelectedRegions = selectedRegions;
            
            RefreshUIFeed();
        }

        /// <summary>
        /// Invoked when an item is selected from the navigation list.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private async void NavigateToNextPage(object selectedItem)
        {
            var homeTile = (HomeTilesModel)selectedItem;

            var title = homeTile.ItemName;

            var contextSplit = homeTile.NavigationContext.Split(new String[] { "::" }, StringSplitOptions.RemoveEmptyEntries);

            var contextType = contextSplit[0];

            var contextString = contextSplit[1];

            switch (contextType)
            {
                case "PAGE":
                    await Shell.Current.GoToAsync($"{contextString}", true);
                    break;
                case "RSS":
                    await Shell.Current.GoToAsync($"{Shell.Current.CurrentState.Location}/{nameof(RSSPage)}?{nameof(RSSPageViewModel.ContextFeed)}={contextString}", true);
                    break;
                default: // WEB
                    //await NavigateToWebPage(title, contextString);
                    await NavigateToWebPage(contextString);
                    break;
            }
        }

        private async Task NavigateToWebPage(string title, string url)
        {
            var current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                await Application.Current.MainPage.DisplayAlert("Internet", "Need an internet connection to navigate here!", "Ok");
                return;
            }
            
            await Shell.Current.GoToAsync(
                $"{Shell.Current.CurrentState.Location}/{nameof(WebPage)}?{nameof(WebPageViewModel.Title)}={title}&{nameof(WebPageViewModel.Url)}={url}");
        }

        private async Task NavigateToWebPage(string url)
        {
            var uri = new Uri(url);
            var success = await _browserService.GoToLink(uri);
                    
            if (!success)
            {
                await Application.Current.MainPage.DisplayAlert("Internet", "Need an internet connection to navigate here!", "Ok");
            }    
        }
        
        public async void PopulateHomeTiles()
        {
            IsBusy = true;

            var cachedHomeTiles = await _homeTilesService.GetHomeTiles();

            UpdateTileList(cachedHomeTiles);

            if (cachedHomeTiles.Count > 0)
            {
                IsBusy = false;
            }

            var updatedHomeTiles = await _homeTilesService.UpdateHomeTiles();

            if (updatedHomeTiles.Count > 0)
            {
                UpdateTileList(updatedHomeTiles);
            }

            IsBusy = false;
        }

        private void UpdateTileList(List<HomeTilesModel> tileList)
        {
            _navigationList.Clear();
            
            foreach (var homeTile in tileList.OrderBy(o => o.ItemOrder))
            {
                var buildVersion = VersionTracking.CurrentVersion;
                
                if (String.IsNullOrWhiteSpace(homeTile.BuildVersion) || homeTile.BuildVersion == buildVersion)
                    _navigationList.Add(homeTile);
            }
            
            RefreshRegionUIFilters();
            RefreshUIFeed();
        }
        
        private void RefreshRegionUIFilters()
        {
            RegionFilter.Clear();
            
            var regionTextList = new List<string>();

            foreach (var region in _navigationList
                .Where(u => !string.IsNullOrWhiteSpace(u.Region))
                .SelectMany(o => o.Region.Split(',')))
            {
                if (!regionTextList.Contains(region))
                {
                    var selected = _settingsClient.SelectedRegions.Contains(region);
                    
                    regionTextList.Add(region);
                    RegionFilter.Add(new RegionFilter
                    {
                        RegionName = region,
                        RegionSelected = selected
                    });
                }
            }
        }

        private void RefreshUIFeed()
        {
            NavigationList.Clear();

            if (!_settingsClient.SelectedRegions.Any())
            {
                _navigationList.ForEach(o => NavigationList.Add(o));
            }
            else
            {
                foreach (var homeTilesModel in _navigationList)
                {
                    var selected = false;
                
                    if (string.IsNullOrWhiteSpace(homeTilesModel.Region))
                    {
                        selected = true;
                    }
                    else
                    {
                        foreach (var region in homeTilesModel.Region.Split(','))
                        {
                            selected = _settingsClient.SelectedRegions.Contains(region);
                        }
                    }

                    if (selected)
                    {
                        NavigationList.Add(homeTilesModel);
                    }
                }
            }
        }

        #endregion
    }
}
