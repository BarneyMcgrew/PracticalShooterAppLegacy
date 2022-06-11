using PracticalShooterApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using PracticalShooterApp.Chips;

namespace PracticalShooterApp.ViewModels
{
    [QueryProperty(nameof(ContextFeed), nameof(ContextFeed))]
    public class RSSPageViewModel : BaseViewModel
    {
        private string _contextFeed;

        private string _description;
        private string _imageUrl;
        private List<RSSFeedModel.RSSFeedItem> _items = 
            new List<RSSFeedModel.RSSFeedItem>();

        private Command<object> backButtonCommand;
        private Command<object> itemTappedCommand;
        private Command<object> filterSelectionCommand;

        private IWebService _webService => DependencyService.Get<IWebService>();
        private IBrowserService _browserService => DependencyService.Get<IBrowserService>();

        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                SetProperty(ref _description, value);
            }
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set
            {
                _imageUrl = value;
                SetProperty(ref _imageUrl, value);
            }
        }

        [DataMember(Name = "navigationList")]
        public ObservableCollection<RSSFeedModel.RSSFeedItem> NavigationList { get; set; } =
            new ObservableCollection<RSSFeedModel.RSSFeedItem>();

        [DataMember(Name = "categoryFilterList")]
        public ObservableCollection<RSSFilterChip> CategoryFilterList { get; set; } =
            new ObservableCollection<RSSFilterChip>();

        [DataMember(Name = "selectedCategories")]
        public ObservableCollection<RSSFilterChip> SelectedCategories { get; set; } =
            new ObservableCollection<RSSFilterChip>();

        public RSSPageViewModel()
        {
        }

        public string ContextFeed
        {
            get => _contextFeed;
            set
            {
                _contextFeed = value;
                LoadRSSFeed(value);
            }
        }

        private async void LoadRSSFeed(string rssFeed)
        {
            try
            {
                IsBusy = true;

                var cachedRSSFeed = await _webService.GetRSSFeed(rssFeed);                

                if (cachedRSSFeed.Items != null && cachedRSSFeed.Items.Count > 0)
                {
                    UpdateRSSFeed(cachedRSSFeed);

                    IsBusy = false;
                }

                var newRSSFeed = await _webService.UpdateRSSFeed(rssFeed);

                if (newRSSFeed != null && newRSSFeed.Items != null && newRSSFeed.Items.Count > 0)
                {
                    UpdateRSSFeed(newRSSFeed);
                }

                IsBusy = false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private void UpdateRSSFeed(RSSFeedModel model)
        {
            if (model.Items.Count == 0)
            {
                BackButtonCommand.Execute(null);
            }

            Title = model.Title;
            Description = model.Description;

            _items.Clear();

            var categories = new List<string>();

            foreach (var item in model.Items)
            {
                foreach (var category in item.Categories)
                {
                    if (!categories.Contains(category))
                    {
                        categories.Add(category);
                    }
                }

                _items.Add(item);
            }

            CategoryFilterList.Clear();
            SelectedCategories.Clear();

            foreach (var category in categories)
            {
                CategoryFilterList.Add(new RSSFilterChip() { Name = category });
            }

            RefreshUIFeed();
        }

        private void RefreshUIFeed()
        {
            var categories = SelectedCategories.Select(c => c.Name);

            NavigationList.Clear();

            foreach (var item in _items.OrderByDescending(o => o.PublishDate))
            {
                if (!SelectedCategories.Any() ||
                    item.Categories
                    .Any(o => categories.Contains(o)))
                {
                    NavigationList.Add(item);
                }
            }
        }

        private async void BackButtonClicked(object obj)
        {
            await Shell.Current.GoToAsync("..", true);
        }

        private async void FilterSelectionChanged(object obj)
        {
            RefreshUIFeed();
        }

        private async void NavigateToNextPage(object selectedItem)
        {
            var rssItem = (RSSFeedModel.RSSFeedItem)selectedItem;

            if (rssItem == null)
                return;
            
            var uri = new Uri(rssItem.PostLink);
            var success =  await _browserService.GoToLink(uri);

            if (!success)
            {
                await Application.Current.MainPage.DisplayAlert("Internet", "Need an internet connection to navigate here!", "Ok");
            }
        }

        public Command<object> BackButtonCommand => this.backButtonCommand ??
            (this.backButtonCommand = new Command<object>(BackButtonClicked));

        public Command<object> ItemTappedCommand
        {
            get 
            { 
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage)); 
            }
        }

        public Command<object> FilterSelectionCommand => this.filterSelectionCommand ??
            (this.filterSelectionCommand = new Command<object>(FilterSelectionChanged));
    }
}
