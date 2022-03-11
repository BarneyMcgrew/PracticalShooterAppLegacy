using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Helpers;
using PracticalShooterApp.ViewModels;
using Syncfusion.SfCalendar.XForms;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CalendarPage : ContentPage
    {
        private IActionBarHelper _actionBarHelper => DependencyService.Get<IActionBarHelper>();

        private readonly CalendarPageViewModel _viewModel = new CalendarPageViewModel();
    
        public CalendarPage()
        {
            InitializeComponent();

            BindingContext = _viewModel;

            calendar.MonthViewSettings = new MonthViewSettings()
            {
                CurrentMonthBackgroundColor = (Color)Application.Current.Resources["PrimaryLight"],
                CurrentMonthTextColor = (Color)Application.Current.Resources["Secondary"],
                PreviousMonthBackgroundColor = (Color)Application.Current.Resources["PrimaryLight"],
                PreviousMonthTextColor = (Color)Application.Current.Resources["PrimaryDark"],
                DateSelectionColor = (Color)Application.Current.Resources["Secondary"],
                SelectedDayTextColor = (Color)Application.Current.Resources["Tertiary"],
                DayHeaderBackgroundColor = (Color)Application.Current.Resources["Primary"],
                TodaySelectionTextColor = (Color)Application.Current.Resources["Secondary"],
                TodaySelectionBackgroundColor = (Color)Application.Current.Resources["Tertiary"],
                DayHeaderTextColor = (Color)Application.Current.Resources["Tertiary"],
                TodayBorderColor = (Color)Application.Current.Resources["Tertiary"],
                HeaderBackgroundColor = (Color)Application.Current.Resources["Primary"],
                HeaderTextColor = (Color)Application.Current.Resources["Tertiary"],
                TodayTextColor = (Color)Application.Current.Resources["Tertiary"],
                InlineBackgroundColor = (Color)Application.Current.Resources["Primary"]
            };

            calendar.YearViewSettings = new YearViewSettings()
            {
                LayoutBackground = (Color)Application.Current.Resources["PrimaryLight"],
                DateTextColor = (Color)Application.Current.Resources["Secondary"],
                HeaderBackground = (Color)Application.Current.Resources["Primary"],
                YearHeaderTextColor = (Color)Application.Current.Resources["Secondary"],
                MonthHeaderBackground = (Color)Application.Current.Resources["PrimaryLight"],
                MonthHeaderTextColor = (Color)Application.Current.Resources["Secondary"],
                MonthLayoutBackground = (Color)Application.Current.Resources["PrimaryLight"]
            };
        }
        
        protected override bool OnBackButtonPressed()
        {
            var backButtonCommand = _viewModel.BackButtonCommand;
            backButtonCommand.Execute(null);
            return true;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            SetHeaderSafeArea();

            try
            {
                calendar.CollapseInlineView((DateTime)calendar.SelectedDate);
            }
            catch (Exception)
            {
                // Expected error due to initialisation issues on Syncfusion
            }

            calendar.SelectedDate = DateTime.Now.Date;
            _viewModel.PopulateCalendarEntries();            
        }

        private void calendar_InlineItemTapped(object sender, InlineItemTappedEventArgs e)
        {
            _viewModel.ItemTappedCommand.Execute(e);
        }

        private void calendar_MonthChanged(object sender, MonthChangedEventArgs e)
        {
            calendar.SelectedDate = DateTime.Now;
        }

        private void filterButton_OnClicked(object sender, EventArgs e)
        {
            popupCalendar.Show();
        }
        
        private void SetHeaderSafeArea()
        {
            popupCalendar.Margin = new Thickness(0, _actionBarHelper.GetTopSafeArea(), 0,0);
        }
        
        private void ToggleButton_OnStateChanged(object sender, StateChangedEventArgs e)
        {
            _viewModel.FilterCheckedCommand.Execute(sender);
        }
    }
}