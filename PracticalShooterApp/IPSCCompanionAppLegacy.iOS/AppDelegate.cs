using System;
using Foundation;
using IPSCCompanionApp;
using IPSCCompanionLibrary.GlobalResources;
using UIKit;

namespace CompetitorCompanion.iOS
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
            FormsControls.Touch.Main.Init();

            UIDevice.CurrentDevice.SetValueForKey(NSNumber.FromNInt((int)(UIInterfaceOrientation.Portrait)), new NSString("orientation"));

            try
            {
                LoadApplication(new App(GlobalEnums.AppVersions.Competitor, GlobalEnums.OSVersions.Android));

                return base.FinishedLaunching(app, options);
            }
            catch (Exception e)
            {
                LoadApplication(new App(e));

                return base.FinishedLaunching(app, options);
            }
        }
    }
}
