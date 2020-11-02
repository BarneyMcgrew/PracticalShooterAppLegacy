using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPSCCompanionApp.UserInterfaces.UTILITIES
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PDFPage : ContentPage
    {
        public PDFPage(string fileName, string friendlyName)
        {
            InitializeComponent();

            HeaderTab.Text = friendlyName;

            var pdfName = $"{fileName}.pdf";
            PDFView.Uri = pdfName;

            HeaderTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => GoBackClicked()));
        }

        private void GoBackClicked()
        {
            Navigation.PopModalAsync();
        }
    }
}