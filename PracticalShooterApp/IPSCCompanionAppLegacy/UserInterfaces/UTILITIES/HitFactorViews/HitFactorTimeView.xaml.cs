using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPSCCompanionApp.UserInterfaces.UTILITIES.HitFactorViews
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HitFactorTimeView : ContentView
    {
        public double Time = 0; // Milliseconds

        public HitFactorTimeView()
        {
            InitializeComponent();
        }

        private void KeypadClicked(object sender, EventArgs e)
        {
            var button = (ImageButton)sender;            

            switch (button.StyleId)
            {
                case "ButtonOne":
                    Time = (Time * 10) + 1;
                    break;
                case "ButtonTwo":
                    Time = (Time * 10) + 2;
                    break;
                case "ButtonThree":
                    Time = (Time * 10) + 3;
                    break;
                case "ButtonFour":
                    Time = (Time * 10) + 4;
                    break;
                case "ButtonFive":
                    Time = (Time * 10) + 5;
                    break;
                case "ButtonSix":
                    Time = (Time * 10) + 6;
                    break;
                case "ButtonSeven":
                    Time = (Time * 10) + 7;
                    break;
                case "ButtonEight":
                    Time = (Time * 10) + 8;
                    break;
                case "ButtonNine":
                    Time = (Time * 10) + 9;
                    break;
                case "ButtonZero":
                    Time = (Time * 10);
                    break;
                case "ButtonDoubleZero":
                    Time = (Time * 100);
                    break;
                case "ButtonClear":
                    Time = 0;
                    break;
                default:
                    break;
            }

            if (Time > 9900)
            {
                if (button.StyleId == "ButtonDoubleZero")
                    Time = Time / 10;

                Time = Time / 10;
                Time = Math.Floor(Time);
            }
            else
            {
                Vibration.Vibrate(50);
            }

            var timeSeconds = Time / 100;

            TimeValue.Text = timeSeconds.ToString("0.00");

            MessagingCenter.Send(this, "Update Hit Factor");
        }
    }
}