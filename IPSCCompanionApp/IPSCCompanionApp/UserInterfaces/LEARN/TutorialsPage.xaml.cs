using FormsControls.Base;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IPSCCompanionApp.UserInterfaces.PRACTICE
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TutorialsPage : ContentPage, IAnimationPage
    {
        public IPageAnimation PageAnimation { get; } = new PushPageAnimation() { Duration = AnimationDuration.Long, Subtype = AnimationSubtype.FromTop };

        public void OnAnimationStarted(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public void OnAnimationFinished(bool isPopAnimation)
        {
            // Put your code here but leaving empty works just fine
        }

        public TutorialsPage()
        {
            InitializeComponent();

            HeaderTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => GoBackClicked()));            
        }

        private void GoBackClicked()
        {
            Navigation.PopModalAsync();
        }

        private void SearchButton_Clicked(object sender, System.EventArgs e)
        {
            SearchSection.IsVisible = !SearchSection.IsVisible;
            TagsSection.IsVisible = false;
        }

        private void TagsButton_Clicked(object sender, System.EventArgs e)
        {
            TagsSection.IsVisible = !TagsSection.IsVisible;
            SearchSection.IsVisible = false;
        }

        private void SearchBarOnly_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}