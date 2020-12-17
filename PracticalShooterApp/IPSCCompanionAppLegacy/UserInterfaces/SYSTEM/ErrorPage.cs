using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace IPSCCompanionApp.UserInterfaces.SYSTEM
{
    public class ErrorPage : ContentPage
    {
        public ErrorPage(Exception e)
        {
            Content = new StackLayout
            {
                Children = {
                    new Label(){Text = JsonConvert.SerializeObject(e)}
                }
            };
        }
    }
}