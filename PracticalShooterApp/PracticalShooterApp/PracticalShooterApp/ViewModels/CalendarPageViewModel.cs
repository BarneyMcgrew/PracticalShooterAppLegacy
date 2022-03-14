using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;
using PracticalShooterApp.Clients;
using PracticalShooterApp.Models;
using PracticalShooterApp.Services;
using PracticalShooterApp.Views;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;

namespace PracticalShooterApp.ViewModels
{
    public class CalendarPageViewModel : BaseViewModel
    {
        private Command<object> backButtonCommand;
        private Command<object> itemTappedCommand;
        private Command<object> filterCheckedCommand;

        private List<CalendarEntriesModel> _calendarEntriesList = 
            new List<CalendarEntriesModel>();

        private ICalendarEntriesService _calendarEntriesService => DependencyService.Get<ICalendarEntriesService>();
        private SettingsClient _settingsClient => DependencyService.Get<SettingsClient>();
        
        public CalendarPageViewModel()
        {
            Title = "Events Calendar";

            PopulateCalendarEntries();
        }
        
        public Command<object> FilterCheckedCommand
        {
            get
            {
                return this.filterCheckedCommand ??
                       (this.filterCheckedCommand = new Command<object>(this.FilterChecked));
            }
        }
        
        public Command<object> BackButtonCommand => this.backButtonCommand ?? 
                                                    (this.backButtonCommand = new Command<object>(this.BackButtonClicked));

        [DataMember(Name = "calendarEntries")]
        public CalendarEventCollection InlineCalendarEntries { get; set; } =
            new CalendarEventCollection();
        
        [DataMember(Name = "regionFilter")]
        public ObservableCollection<RegionFilter> RegionFilter { get; set; } =
            new ObservableCollection<RegionFilter>();
        
        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ?? (this.itemTappedCommand = new Command<object>(this.NavigateToCalendarEntry));
            }
        }
        
        private async void NavigateToCalendarEntry(object selectedItem)
        {
            var inlineEvent = ((InlineItemTappedEventArgs)selectedItem).InlineEvent;

            var inlineIdentifier = $"{inlineEvent.Subject} | {inlineEvent.StartTime}";

            await Shell.Current.Navigation.PushModalAsync(new CalendarEntryPage(inlineIdentifier));
        }
        
        private async void BackButtonClicked(object obj)
        {
            await Shell.Current.GoToAsync("..");
        }

        public async void PopulateCalendarEntries()
        {
            IsBusy = true;

            var cachedCalendarEntries = await _calendarEntriesService.GetCalendarEntries();

            UpdateCalendarListings(cachedCalendarEntries);

            if (cachedCalendarEntries.Count > 0)
            {
                IsBusy = false;
            }

            var updatedCalendarEntries = await _calendarEntriesService.UpdateCalendarEntries();

            if (updatedCalendarEntries.Count > 0)
            {
                UpdateCalendarListings(updatedCalendarEntries);
            }

            IsBusy = false;
        }

        public void UpdateCalendarListings(List<CalendarEntriesModel> calendarEntriesList)
        {
            _calendarEntriesList.Clear();

            foreach (var calendarEntriesModel in calendarEntriesList)
            {
                _calendarEntriesList.Add(calendarEntriesModel);
            }
            
            RefreshRegionUIFilters();
            RefreshUIFeed();
        }

        public async void FilterChecked(object sender)
        {
            var selectedCountries = new List<string>();
            
            foreach (var filter in RegionFilter)
            {
                if (filter.RegionSelected)
                {
                    selectedCountries.Add(filter.RegionName);
                }
            }

            _settingsClient.SelectedCountries = selectedCountries;
            
            RefreshUIFeed();
        }
        
        private void RefreshRegionUIFilters()
        {
            RegionFilter.Clear();
            
            var regionTextList = new List<string>();

            foreach (var region in _calendarEntriesList
                         .Where(u => !string.IsNullOrWhiteSpace(u.EventCountry))
                         .SelectMany(o => o.EventCountry.Split(',')))
            {
                if (!regionTextList.Contains(region))
                {
                    var selected = _settingsClient.SelectedCountries.Contains(region);
                    
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
            InlineCalendarEntries.Clear();

            if (!_settingsClient.SelectedCountries.Any())
            {
                _calendarEntriesList.ForEach(o => InlineCalendarEntries.Add(o.ToInlineEvent()));
            }
            else
            {
                foreach (var homeTilesModel in _calendarEntriesList)
                {
                    var selected = false;
                
                    if (string.IsNullOrWhiteSpace(homeTilesModel.EventCountry))
                    {
                        selected = true;
                    }
                    else
                    {
                        foreach (var region in homeTilesModel.EventCountry.Split(','))
                        {
                            selected = _settingsClient.SelectedCountries.Contains(region);
                        }
                    }

                    if (selected)
                    {
                        InlineCalendarEntries.Add(homeTilesModel.ToInlineEvent());
                    }
                }
            }
        }
    }
}