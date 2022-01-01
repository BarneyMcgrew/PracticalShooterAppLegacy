using System;

using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.Runtime;
using Android.OS;
using Android.Views;
using AndroidX.AppCompat.App;
using Xamarin.Essentials;
using Xamarin.Forms;
using Color = Android.Graphics.Color;

namespace PracticalShooterApp.Droid
{
    [Activity(Label = "Practical Shooter App", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            if (Xamarin.Essentials.Platform.CurrentActivity.Window != null)
                Xamarin.Essentials.Platform.CurrentActivity.Window.SetStatusBarColor(Color.ParseColor("#353535"));

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    [Activity(Label = "Practical Shooter App", Icon = "@drawable/ic_launcher_round", Theme = "@style/splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : AppCompatActivity
    {
        protected override void OnResume()
        {
            base.OnResume();
            StartActivity(typeof(MainActivity));
        }
    }
}