using Foundation;
using UIKit;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.PopupLayout;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfCalendar.XForms.iOS;
using Syncfusion.SfPdfViewer.XForms.iOS;
using Syncfusion.SfRangeSlider.XForms.iOS;
using Syncfusion.SfRotator.XForms.iOS;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.XForms.iOS.Cards;
using Syncfusion.XForms.iOS.Core;
using Syncfusion.XForms.iOS.EffectsView;
using Syncfusion.XForms.iOS.Graphics;
using Syncfusion.XForms.iOS.ProgressBar;
using Syncfusion.XForms.iOS.TextInputLayout;
using Xamarin;

namespace PracticalShooterApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            SfEffectsViewRenderer.Init();
            this.LoadApplication(new App());
            SfTextInputLayoutRenderer.Init();
            SfButtonRenderer.Init();
            SfCheckBoxRenderer.Init();
            SfBorderRenderer.Init();
            SfGradientViewRenderer.Init();
            SfPdfDocumentViewRenderer.Init();
            SfRangeSliderRenderer.Init();
            SfListViewRenderer.Init();
            SfRotatorRenderer.Init();
            SfRadioButtonRenderer.Init();
            SfPopupLayoutRenderer.Init();
            SfCardViewRenderer.Init();
            SfSegmentedControlRenderer.Init();
            Core.Init();
            SfCalendarRenderer.Init();
            SfLinearProgressBarRenderer.Init();
            SfBusyIndicatorRenderer.Init();

            IQKeyboardManager.SharedManager.Enable = true;

            LoadApplication(new App());
            app.SetStatusBarStyle(UIStatusBarStyle.LightContent, true);

            return base.FinishedLaunching(app, options);
        }
    }
}
