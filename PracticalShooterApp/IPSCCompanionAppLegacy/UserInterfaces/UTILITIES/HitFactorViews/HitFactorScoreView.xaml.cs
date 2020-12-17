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
    public partial class HitFactorScoreView : ContentView
    {
        public double Score;
        public int Alphas;
        public int Charlies;
        public int Deltas;
        public int Mikes;
        public int Penalties;
        public bool isMajor;

        public HitFactorScoreView()
        {
            InitializeComponent();
        }

        private void KeypadClicked(object sender, EventArgs e)
        {
            Vibration.Vibrate(50);

            var button = (ImageButton)sender;

            switch (button.StyleId  )
            {
                case "ButtonAlphaP":
                    Alphas++;
                    break;
                case "ButtonAlphaN":
                    Alphas--;
                    break;
                case "ButtonCharlieP":
                    Charlies++;
                    break;
                case "ButtonCharlieN":
                    Charlies--;
                    break;
                case "ButtonDeltaP":
                    Deltas++;
                    break;
                case "ButtonDeltaN":
                    Deltas--;
                    break;
                case "ButtonMikeP":
                    Mikes++;
                    break;
                case "ButtonMikeN":
                    Mikes--;
                    break;
                case "ButtonPenaltyP":
                    Penalties++;
                    break;
                case "ButtonPenaltyN":
                    Penalties--;
                    break;
                case "ButtonFactor":
                    if (isMajor)
                    {
                        isMajor = false;
                        button.Source = "minor_icon.png";
                    }
                    else
                    {
                        isMajor = true;
                        button.Source = "major_icon.png";
                    }
                    break;
                case "ButtonClear":
                    Score = 0;
                    Alphas = 0;
                    Charlies = 0;
                    Deltas = 0;
                    Mikes = 0;
                    Penalties = 0;
                    break;
                default:
                    break;                
            }

            if (Alphas < 0) Alphas = 0;
            if (Charlies < 0) Charlies = 0;
            if (Deltas < 0) Deltas = 0;
            if (Mikes < 0) Mikes = 0;
            if (Penalties < 0) Penalties = 0;

            var factor = isMajor ? 4 : 3;

            Score = (Alphas * 5) +
                    (Charlies * factor) +
                    (Deltas * 1) +
                    (Mikes * -10) +
                    (Penalties * -10);            

            ScoreValue.Text = Score < 0 ? "0" : Score.ToString();
            AlphaValue.Text = Alphas.ToString();
            CharlieValue.Text = Charlies.ToString();
            DeltaValue.Text = Deltas.ToString();
            MikeValue.Text = Mikes.ToString();
            PenaltyValue.Text = Penalties.ToString();

            MessagingCenter.Send(this, "Update Hit Factor");
        }
    }
}