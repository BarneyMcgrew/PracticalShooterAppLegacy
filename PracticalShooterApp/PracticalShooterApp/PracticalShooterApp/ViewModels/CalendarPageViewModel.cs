using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Windows.Input;
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

        private ICalendarEntriesService _calendarEntriesService => DependencyService.Get<ICalendarEntriesService>();
        
        public CalendarPageViewModel()
        {
            Title = "Events Calendar";

            PopulateCalendarEntries();
        }
        
        public Command<object> BackButtonCommand => this.backButtonCommand ?? 
                                                    (this.backButtonCommand = new Command<object>(this.BackButtonClicked));

        [DataMember(Name = "calendarEntries")]
        public CalendarEventCollection InlineCalendarEntries { get; set; } =
            new CalendarEventCollection();

        public List<CalendarEntriesModel> CalendarEntries { get; set; } =
            new List<CalendarEntriesModel>();
        
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

            await Shell.Current.GoToAsync($"{nameof(CalendarEntryPage)}?{nameof(CalendarEntriesModel.InlineIdentifier)}={inlineIdentifier}");
        }
        
        private async void BackButtonClicked(object obj)
        {
            await Shell.Current.GoToAsync("//HomePage");
        }

        public async void PopulateCalendarEntries()
        {
            IsBusy = true;

            var cachedCalendarEntries = await _calendarEntriesService.GetCalendarEntries();

            UpdateCalendar(cachedCalendarEntries);

            if (cachedCalendarEntries.Count > 0)
            {
                IsBusy = false;
            }

            var updatedCalendarEntries = await _calendarEntriesService.UpdateCalendarEntries();

            if (updatedCalendarEntries.Count > 0)
            {
                UpdateCalendar(updatedCalendarEntries);
            }

            IsBusy = false;
        }

        public void UpdateCalendar(List<CalendarEntriesModel> calendarEntriesList)
        {
            InlineCalendarEntries.Clear();
            CalendarEntries.Clear();

            foreach (var calendarEntriesModel in calendarEntriesList)
            {
                CalendarEntries.Add(calendarEntriesModel);
                InlineCalendarEntries.Add(calendarEntriesModel.ToInlineEvent());
            }
        }
    }
}