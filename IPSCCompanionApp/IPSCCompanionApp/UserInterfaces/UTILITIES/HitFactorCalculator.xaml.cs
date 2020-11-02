using IPSCCompanionApp.UserInterfaces.UTILITIES.HitFactorViews;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPSCCompanionApp.UserInterfaces.UTILITIES
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HitFactorCalculator : ContentPage
    {
        private HitFactorScoreView _scoreView;
        private HitFactorTimeView _timeView;
        private double _hitFactor;

        public HitFactorCalculator()
        {
            InitializeComponent();
                        
            _timeView = new HitFactorTimeView();
            _scoreView = new HitFactorScoreView();

            var viewList = new List<ContentView>() 
            {
                _timeView,
                _scoreView
            };
                        
            HitFactorViews.ItemsSource = viewList;

            MessagingCenter.Subscribe<HitFactorScoreView>(this, "Update Hit Factor", (sender) =>
            {
                UpdateHitFactor();
            });

            MessagingCenter.Subscribe<HitFactorTimeView>(this, "Update Hit Factor", (sender) =>
            {
                UpdateHitFactor();
            });

            HeaderTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => GoBackClicked()));
        }

        private void UpdateHitFactor()
        {
            var time = _timeView.Time;
            var score = _scoreView.Score;

            _hitFactor = Math.Round(score / (time / 100), 2);

            if (_hitFactor > 0 && _hitFactor < 20)
            {
                HitFactorValue.Text = _hitFactor.ToString();
            }
            else
            {
                HitFactorValue.Text = "0";
            }
            
        }

        private void GoBackClicked()
        {
            Navigation.PopModalAsync();
        }

        private void SettingsButton_Clicked(object sender, EventArgs e)
        {

        }

        private void HistoryButton_Clicked(object sender, EventArgs e)
        {

        }
    }
}