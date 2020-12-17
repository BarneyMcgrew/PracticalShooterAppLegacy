using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace PracticalShooterLibrary.StaticResources
{
    public class GlobalColor
    {
        public static GlobalColor Current = new GlobalColor();

        public Color TutorialsColor
        {
            get
            {
                var color = Color.FromHex("#a3297a");

                return color;
            }
        }

        public Color RulebooksColor
        {
            get
            {
                var color = Color.FromHex("#ffd11a");

                return color;
            }
        }

        public Color BarBackgroundColor
        {
            get
            {
                var color = Color.FromHex("#998200");

                return color;
            }
        }

        public Color BarTextColor
        {
            get
            {
                var color = Color.White;

                return color;
            }
        }

        public Color MainBackgroundColor
        {
            get
            {
                var color = Color.FromHex("#f2f2f2");

                return color;
            }
        }

        public Color SubBackgroundColor
        {
            get
            {
                var color = Color.FromHex("#8c8c8c");

                return color;
            }
        }

        public Color MainTextColor
        {
            get
            {
                var color = Color.FromHex("fff5f5f5");

                return color;
            }
        }

        public Color LearnMainColor
        {
            get
            {
                var color = Color.FromHex("#ffd700");

                return color;
            }
        }

        public Color RulebooksMainColor
        {
            get
            {
                var color = Color.FromHex("#e6c300");

                return color;
            }
        }

        public Color UtilitiesMainColor
        {
            get
            {
                var color = Color.FromHex("#ccad00");

                return color;
            }
        }

        public Color PracticeMainColor
        {
            get
            {
                var color = Color.FromHex("#b39800"); 

                return color;
            }
        }

        public Color SettingsMainColor
        {
            get
            {
                var color = Color.FromHex("#8c8c8c");

                return color;
            }
        }
    }
}
