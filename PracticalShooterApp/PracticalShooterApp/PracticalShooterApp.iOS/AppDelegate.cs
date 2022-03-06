using Syncfusion.SfCarousel.XForms.iOS;
using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;
using Syncfusion.XForms.iOS.Buttons;
using Syncfusion.XForms.iOS.PopupLayout;
using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfBusyIndicator.XForms.iOS;
using Syncfusion.SfCalendar.XForms.iOS;
using Syncfusion.SfChart.XForms.iOS.Renderers;
using Syncfusion.SfDataGrid.XForms.iOS;
using Syncfusion.SfGauge.XForms.iOS;
using Syncfusion.SfMaps.XForms.iOS;
using Syncfusion.SfNumericUpDown.XForms.iOS;
using Syncfusion.SfPullToRefresh.XForms.iOS;
using Syncfusion.SfRating.XForms.iOS;
using Syncfusion.SfRotator.XForms.iOS;
using Syncfusion.XForms.iOS.BadgeView;
using Syncfusion.XForms.iOS.Border;
using Syncfusion.XForms.iOS.Cards;
using Syncfusion.XForms.iOS.ComboBox;
using Syncfusion.XForms.iOS.Core;
using Syncfusion.XForms.iOS.EffectsView;
using Syncfusion.XForms.iOS.Expander;
using Syncfusion.XForms.iOS.Graphics;
using Syncfusion.XForms.iOS.ProgressBar;
using Syncfusion.XForms.iOS.TabView;

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
            SfButtonRenderer.Init();
            SfCheckBoxRenderer.Init();
            SfDataGridRenderer.Init();
            SfBorderRenderer.Init();
            SfGradientViewRenderer.Init();
            SfListViewRenderer.Init();
            SfRatingRenderer.Init();
            SfRotatorRenderer.Init();
            SfComboBoxRenderer.Init();
            SfRadioButtonRenderer.Init();
            SfPopupLayoutRenderer.Init();
            SfExpanderRenderer.Init();
            SfCardViewRenderer.Init();
            SfBadgeViewRenderer.Init();
            SfSegmentedControlRenderer.Init();
            Core.Init();
            SfMapsRenderer.Init();
            SfTabViewRenderer.Init();
            SfCalendarRenderer.Init();
            SfLinearProgressBarRenderer.Init();
            SfChartRenderer.Init();
            SfGaugeRenderer.Init();
            SfBusyIndicatorRenderer.Init();

            LoadApplication(new App());
            app.SetStatusBarStyle(UIStatusBarStyle.LightContent, true);

            return base.FinishedLaunching(app, options);
        }
    }
}
