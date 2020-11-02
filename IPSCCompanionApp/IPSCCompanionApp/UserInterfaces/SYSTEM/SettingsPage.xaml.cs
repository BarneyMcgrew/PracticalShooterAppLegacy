using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FormsControls.Base;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPSCCompanionApp.UserInterfaces.SYSTEM
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage, IAnimationPage
    {
        public IPageAnimation PageAnimation { get; } = new PushPageAnimation() { Duration = AnimationDuration.Long, Subtype = AnimationSubtype.FromRight };

        public void OnAnimationStarted(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public SettingsPage()
        {
            InitializeComponent();

            VersionLabel.Text = "ALPHA";

            WebsiteAddressCell.GestureRecognizers.Add(new TapGestureRecognizer((view) => OnLabelClicked()));
            HeaderTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => GoBackClicked()));
        }

        private void OnLabelClicked()
        {
            var uri = new Uri("https://practipanion.com/");
            Browser.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
        }

        private void GoBackClicked()
        {
            Navigation.PopModalAsync();
        }
    }
}