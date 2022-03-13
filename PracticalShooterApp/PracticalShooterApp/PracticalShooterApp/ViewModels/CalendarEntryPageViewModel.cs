using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using PracticalShooterApp.Helpers;
using PracticalShooterApp.Services;
using Xamarin.Forms;

namespace PracticalShooterApp.ViewModels
{
    [QueryProperty(nameof(InlineIdentifier), nameof(InlineIdentifier))]
    public class CalendarEntryPageViewModel : BaseViewModel
    {
        private string _inlineIdentifier;
        private string _imageUrl;
        private string _eventName;
        private string _eventDate;
        private string _eventTimeRange;
        private string _eventCountry;
        private string _eventLocation;
        private string _disciplineName;
        private string _eventAssociation;
        private string _eventDescription;
        private string _calendarUrl;
        private bool _showCalendarUrl;

        private Command<object> backButtonCommand;
        private Command goToLinkCommand;
        private Command shareEntryCommad;

        private ICalendarEntriesService _calendarEntriesService => DependencyService.Get<ICalendarEntriesService>();
        private IBrowserService _browserService => DependencyService.Get<IBrowserService>();
        private IShareHelper _shareHelper => DependencyService.Get<IShareHelper>();

        public CalendarEntryPageViewModel()
        {    
        }

        public Command<object> BackButtonCommand => this.backButtonCommand ??
                                                    (this.backButtonCommand = new Command<object>(this.BackButtonClicked));

        public Command GoToLinkCommand => this.goToLinkCommand ??
                                                  (this.goToLinkCommand = new Command(this.GoToLinkClicked));
        
        public Command ShareEntryCommand => this.shareEntryCommad ??
                                          (this.shareEntryCommad = new Command(this.ShareEntryClicked));

        public string InlineIdentifier
        {
            get => _inlineIdentifier;
            set
            {
                _inlineIdentifier = value;
                LoadCalendarEntry(value);
            }
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        public string EventName
        {
            get => _eventName;
            set => SetProperty(ref _eventName, value);
        }

        public string EventDate
        {
            get => _eventDate;
            set => SetProperty(ref _eventDate, value);
        }

        public string EventTimeRange
        {
            get => _eventTimeRange;
            set => SetProperty(ref _eventTimeRange, value);
        }

        public string EventCountry
        {
            get => _eventCountry;
            set => SetProperty(ref _eventCountry, value);
        }

        public string EventLocation
        {
            get => _eventLocation;
            set => SetProperty(ref _eventLocation, value);
        }

        public string DisciplineName
        {
            get => _disciplineName;
            set => SetProperty(ref _disciplineName, value);
        }

        public string EventAssociation
        {
            get => _eventAssociation;
            set => SetProperty(ref _eventAssociation, value);
        }

        public string EventDescription
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_eventDescription))
                {
                    return "No Description Available.";
                }

                return _eventDescription;
            }
            set => SetProperty(ref _eventDescription, value);
        }

        public string CalendarUrl
        {
            get => _calendarUrl;
            set => SetProperty(ref _calendarUrl, value);
        }

        public bool ShowCalendarUrl
        {
            get => _showCalendarUrl;
            set => SetProperty(ref _showCalendarUrl, value);
        }

        private async void BackButtonClicked(object obj)
        {
            await Shell.Current.Navigation.PopModalAsync();
            //await Shell.Current.GoToAsync("..");
        }

        private void GoToLinkClicked()
        {
            var uri = new Uri(CalendarUrl);
            _browserService.GoToLink(uri);
        }

        private void ShareEntryClicked()
        {
            _shareHelper.ShareUri(EventName, CalendarUrl);
        }

        private async void LoadCalendarEntry(string inlineIdentifier)
        {
            try
            {
                var calendarEntry = await _calendarEntriesService.GetCalendarEntry(inlineIdentifier);

                ImageUrl = calendarEntry.EventImageUrl;
                EventName = calendarEntry.EventName;
                EventDate = FormatDate(calendarEntry.EventStartDate, calendarEntry.EventEndDate);
                EventTimeRange = FormatTime(calendarEntry.EventStartDate, calendarEntry.EventEndDate);
                EventCountry = calendarEntry.EventCountry;
                EventLocation = calendarEntry.EventLocation;
                DisciplineName = FormatDiscipline(calendarEntry.DisciplineName, calendarEntry.MatchLevel);
                EventAssociation = calendarEntry.AssociationName;
                EventDescription = calendarEntry.EventDescription;
                CalendarUrl = calendarEntry.CalendarUrl;
                ShowCalendarUrl = !string.IsNullOrWhiteSpace(_calendarUrl);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string FormatDate(DateTime startDateTime, DateTime endDateTime)
        {
            var startDate = startDateTime.ToString("dd MMM yyyy");
            var endDate = endDateTime.ToString("dd MMM yyyy");

            if (startDate == endDate)
            {
                return $"{startDateTime.DayOfWeek} {startDate}";
            }

            return $"{startDateTime.DayOfWeek} {startDate} - {endDate}";
        }

        public string FormatTime(DateTime startDate, DateTime endDate)
        {
            var startTimeOfDay = startDate.ToString("h:mm tt");
            var endTimeOfDay = endDate.ToString("h:mm tt");

            return $"{startTimeOfDay} - {endTimeOfDay}";
        }

        public string FormatDiscipline(string disciplineName, string matchLevel)
        {
            if (string.IsNullOrWhiteSpace(disciplineName))
                return "General Event";

            return $"{disciplineName} | Level {matchLevel}";
        }
    }
}
