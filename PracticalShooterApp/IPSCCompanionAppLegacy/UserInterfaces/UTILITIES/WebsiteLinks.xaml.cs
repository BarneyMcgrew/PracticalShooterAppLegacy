using IPSCCompanionLibrary.StaticResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPSCCompanionApp.UserInterfaces.UTILITIES
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebsiteLinks : ContentPage
    {
        public WebsiteLinks()
        {
            InitializeComponent();

            LoadWebsitesView();

            HeaderTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => GoBackClicked()));
        }

        public void LoadWebsitesView()
        {
            var websiteCollection = GlobalResourceCache.Current.WebsitesCollection;

            foreach (var website in websiteCollection.Websites)
            {
                var websiteFrame = new Frame()
                {
                    Margin = new Thickness(25, 25, 25, 0),
                    Padding = new Thickness(10),
                    CornerRadius = 25,
                    BackgroundColor = Color.FromHex("#0066ff")
                };

                if (website.DisplayName == "Retailers")
                {
                    websiteFrame.BackgroundColor = Color.Gold;
                }

                var frameStack = new StackLayout()
                {
                    Margin = new Thickness(10)
                };

                var websiteHeader = new Label()
                {
                    Text = website.DisplayName.ToUpper(),
                    TextColor = Color.White,
                    Margin = new Thickness(10),
                    FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                    FontAttributes = FontAttributes.Bold,
                    HorizontalOptions = LayoutOptions.Center
                };                

                var linksFrame = new StackLayout()
                {
                    Margin = new Thickness(10),
                    IsVisible = false
                };

                foreach (var webLink in website.Weblinks)
                {
                    var button = new Button()
                    {
                        BackgroundColor = Color.White,
                        TextColor = Color.DarkSlateGray,
                        Margin = new Thickness(5),
                        Padding = new Thickness(5),
                        CornerRadius = 10,
                        FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                        Text = webLink.DisplayName                        
                    };

                    button.Clicked += async (sender, args) => Device.OpenUri(new Uri(webLink.UrlLink));

                    linksFrame.Children.Add(button);
                }

                frameStack.GestureRecognizers.Add(new TapGestureRecognizer((view) =>
                {
                    linksFrame.IsVisible = !linksFrame.IsVisible;
                }));

                frameStack.Children.Add(websiteHeader);
                frameStack.Children.Add(linksFrame);

                websiteFrame.Content = frameStack;

                WebsitesView.Children.Add(websiteFrame);
            }
        }

        private void GoBackClicked()
        {
            Navigation.PopModalAsync();
        }
    }
}