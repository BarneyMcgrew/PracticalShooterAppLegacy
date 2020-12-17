using System;
using System.IO;
using FormsControls.Base;
using IPSCCompanionApp.UserInterfaces.SYSTEM;
using IPSCCompanionLibrary.GlobalResources;
using IPSCCompanionLibrary.StaticResources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPSCCompanionApp
{
    public partial class App : Application
    {
        public static string FolderPath;

        public App(Exception e)
        {
            InitializeComponent();

            var errorPage = new ErrorPage(e);

            var navigation = new NavigationPage(errorPage);

            navigation.BarBackgroundColor = Color.Gold;
            navigation.BarTextColor = Color.White;

            MainPage = navigation;
        }

        public App(GlobalEnums.AppVersions appVersion, GlobalEnums.OSVersions osVersion)
        {
            InitializeComponent();

            FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));

            var mainMenu = new MainMenuLight();

            var navigation = new AnimationNavigationPage(mainMenu);

            navigation.BarBackgroundColor = GlobalColor.Current.BarBackgroundColor;
            navigation.BarTextColor = GlobalColor.Current.BarTextColor;

            MainPage = navigation;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
