using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using PracticalShooterApp.Models;
using PracticalShooterApp.Services;
using PracticalShooterApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using NavigationModel = PracticalShooterApp.Models.NavigationModel;

namespace PracticalShooterApp.ViewModels
{
    public class HomePageViewModel : BaseViewModel
    {
        #region Fields

        private Command<object> itemTappedCommand;

        private IHomeTilesService _homeTilesService => DependencyService.Get<IHomeTilesService>();
        private IBrowserService _browserService => DependencyService.Get<IBrowserService>();
        
        #endregion

        public HomePageViewModel()
        {
            Title = "Home";
                        
            PopulateHomeTiles();
        }

        #region Properties

        /// <summary>
        /// Gets the command that will be executed when an item is selected.
        /// </summary>
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        /// <summary>
        /// Gets or sets a collection of values to be displayed in the Navigation list page.
        /// </summary>
        [DataMember(Name = "navigationList")]
        public ObservableCollection<HomeTilesModel> NavigationList { get; set; } =
            new ObservableCollection<HomeTilesModel>();

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected from the navigation list.
        /// </summary>
        /// <param name="selectedItem">Selected item from the list view.</param>
        private async void NavigateToNextPage(object selectedItem)
        {
            var homeTile = (HomeTilesModel)selectedItem;

            var contextSplit = homeTile.NavigationContext.Split(new String[] { "::" }, StringSplitOptions.RemoveEmptyEntries);

            var contextType = contextSplit[0];

            var contextString = contextSplit[1];

            switch (contextType)
            {
                case "PAGE":
                    await Shell.Current.GoToAsync(contextString);
                    break;
                case "RSS":
                    await Shell.Current.GoToAsync($"{nameof(RSSPage)}?{nameof(RSSPageViewModel.ContextFeed)}={contextString}");
                    break;
                default: // WEB
                    var uri = new Uri(contextString);
                    _browserService.GoToLink(uri);
                    break;
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
            NavigationList.Clear();

            foreach (var homeTile in tileList.OrderBy(o => o.ItemOrder))
            {
                NavigationList.Add(homeTile);
            }
        }

        #endregion
    }
}
