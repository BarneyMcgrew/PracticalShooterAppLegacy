using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;

namespace PracticalShooterApp.Droid
{
    public class SplashActivity
    {
        [Activity(Label = "P Shooter", Icon = "@mipmap/ic_launcher", MainLauncher = true, NoHistory = true, Theme = "@style/MyTheme.Splash")]
        public class SplashScreen : Activity, Android.Animation.Animator.IAnimatorListener
        {
            public void OnAnimationCancel(Lottie.Forms.Resource.Animator animation)
            {

            }

            public void OnAnimationCancel(Animator? animation)
            {
                
            }

            public void OnAnimationEnd(Animator animation)
            {
                StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            }

            void Animator.IAnimatorListener.OnAnimationRepeat(Animator? animation)
            {
                OnAnimationRepeat(animation);
            }

            void Animator.IAnimatorListener.OnAnimationStart(Animator? animation)
            {
                OnAnimationStart(animation);
            }

            void Animator.IAnimatorListener.OnAnimationEnd(Animator? animation)
            {
                OnAnimationEnd(animation);
            }

            public void OnAnimationRepeat(Animator animation)
            {

            }

            public void OnAnimationStart(Animator animation)
            {

            }

            protected override void OnCreate(Bundle savedInstanceState)
            {
                base.OnCreate(savedInstanceState);


                SetContentView(Resource.Layout.SplashLayout);

                var animation = FindViewById<Com.Airbnb.Lottie.LottieAnimationView>(Resource.Id.animation_view);

                animation.AddAnimatorListener(this);
            }
        }
    }
}