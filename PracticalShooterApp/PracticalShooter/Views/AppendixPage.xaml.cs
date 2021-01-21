using PracticalShooter.ViewModels;
using PracticalShooterLibrary.ObjectClasses.Rules;
using PracticalShooterLibrary.StaticResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppendixPage : ContentPage
    {
        private AppendixViewModel _viewModel;

        private PracticalShooterLibrary.ObjectClasses.Rules.Appendices _appendices;
        private string _searchQuery;

        public AppendixPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new AppendixViewModel();

            this._appendices = GlobalResourceCache.Current.GetAppendices();
            this._searchQuery = RulebookSession.Current.SearchQuery;

            InitialisePage();
        }

        private void InitialisePage()
        {
            var selectedDiscipline = GlobalSettings.Current.SelectedDiscipline;
            DisciplineName.Text = selectedDiscipline;
            SelectedDisciplinePicker.SelectedItem = selectedDiscipline;

            HeaderTab.Text = "APPENDICES";
            SearchBarOnly.Text = _searchQuery;
            HeaderTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => GoBackClicked()));
            DisciplineName.GestureRecognizers.Add(new TapGestureRecognizer((view) => DisciplineNameClicked()));
            PopUpGrid.GestureRecognizers.Add(new TapGestureRecognizer((view) => ChangePopUpVisibility(false)));

            SearchAppendices();

            LoadingDisplay.IsVisible = false;
        }

        private void DisciplineNameClicked()
        {
            SelectedDisciplinePicker.Focus();
        }

        private void ChangePopUpVisibility(bool visible)
        {
            PopUpGrid.IsVisible = visible;
        }

        private void GoBackClicked()
        {
            AppShell.Current.Navigation.PopAsync();
        }

        private void DisciplineButton_Clicked(object sender, System.EventArgs e)
        {
            DisciplineNameClicked();
        }

        private void SelectedDisciplinePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedDiscipline = (string)SelectedDisciplinePicker.SelectedItem;
            DisciplineName.Text = selectedDiscipline;
            GlobalSettings.Current.SelectedDiscipline = selectedDiscipline;

            GlobalSettings.Current.UpdateSettings();

            ReloadAppendices();
        }

        private void ReloadAppendices()
        {
            _appendices = GlobalResourceCache.Current.GetAppendices();

            SearchAppendices();
        }

        private void SearchButton_Clicked(object sender, System.EventArgs e)
        {
            SearchSection.IsVisible = !SearchSection.IsVisible;
            DisciplineSection.IsVisible = false;
        }

        private void SearchBarOnly_TextChanged(object sender, TextChangedEventArgs e)
        {
            _searchQuery = e.NewTextValue;

            SearchAppendices();
        }

        private void SearchAppendices()
        {
            if (string.IsNullOrWhiteSpace(_searchQuery))
            {
                SearchFlag.IsVisible = false;
                
                LoadAppendices();
            }
            else
            {
                SearchFlag.IsVisible = true;
                SearchName.Text = $"Search Results: \"{_searchQuery}\"";

                var searchedAppendices = SearchAppendices(_searchQuery);

                PopulateTableView(searchedAppendices, true);
            }
        }

        private List<AppendixImage> SearchAppendices(string searchQuery)
        {
            var searchCriteria = searchQuery.ToLowerInvariant();

            var searchResults = _appendices.AppendixList.Where(o => o.SearchableString.Contains(searchCriteria));

            return searchResults.ToList();
        }

        private void LoadAppendices()
        {
            var appendices = _appendices;

            PopulateTableView(appendices.AppendixList, false);
        }

        private void PopulateTableView(List<AppendixImage> appendixImages, bool isSearch)
        {
            SearchResults.Clear();

            foreach (var appendixImage in appendixImages)
            {
                SearchResults.Add(CreateImageViewCell(appendixImage));
            }
        }

        private ViewCell CreateImageViewCell(AppendixImage appendix)
        {
            var viewCell = new ViewCell();

            var image = new Image()
            {
                Source = $"{appendix.Filename}.jpg"
            };

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            image.GestureRecognizers.Add(panGesture);

            var pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += OnPinchUpdated;
            image.GestureRecognizers.Add(pinchGesture);

            viewCell.View = image;

            return viewCell;
        }

        private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            var s = (ContentView)sender;

            // do not allow pan if the image is in its intial size
            if (currentScale == 1)
                return;

            switch (e.StatusType)
            {
                case GestureStatus.Running:
                    double xTrans = xOffset + e.TotalX, yTrans = yOffset + e.TotalY;
                    // do not allow verical scorlling unless the image size is bigger than the screen
                    s.Content.TranslateTo(xTrans, yTrans, 0, Easing.Linear);
                    break;

                case GestureStatus.Completed:
                    // Store the translation applied during the pan
                    xOffset = s.Content.TranslationX;
                    yOffset = s.Content.TranslationY;

                    // center the image if the width of the image is smaller than the screen width
                    if (originalWidth * currentScale < ScreenWidth && ScreenWidth > ScreenHeight)
                        xOffset = (ScreenWidth - originalWidth * currentScale) / 2 - s.Content.X;
                    else
                        xOffset = System.Math.Max(System.Math.Min(0, xOffset), -System.Math.Abs(originalWidth * currentScale - ScreenWidth));

                    // center the image if the height of the image is smaller than the screen height
                    if (originalHeight * currentScale < ScreenHeight && ScreenHeight > ScreenWidth)
                        yOffset = (ScreenHeight - originalHeight * currentScale) / 2 - s.Content.Y;
                    else
                        //yOffset = System.Math.Max(System.Math.Min((originalHeight - (ScreenHeight)) / 2, yOffset), -System.Math.Abs((originalHeight * currentScale - ScreenHeight - (originalHeight - ScreenHeight) / 2)) + (NavBar.Height + App.StatusBarHeight));
                        yOffset = System.Math.Max(System.Math.Min((originalHeight - (ScreenHeight)) / 2, yOffset), -System.Math.Abs((originalHeight * currentScale - ScreenHeight - (originalHeight - ScreenHeight) / 2)));

                    // bounce the image back to inside the bounds
                    s.Content.TranslateTo(xOffset, yOffset, 500, Easing.BounceOut);
                    break;
            }
        }

        private void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            var s = (ContentView)sender;

            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                startScale = s.Content.Scale;

                s.Content.AnchorX = 0;
                s.Content.AnchorY = 0;
            }
            if (e.Status == GestureStatus.Running)
            {

                // Calculate the scale factor to be applied.
                currentScale += (e.Scale - 1) * startScale;
                currentScale = System.Math.Max(1, currentScale);
                currentScale = System.Math.Min(currentScale, 5);

                //scaleLabel.Text = "Scale: " + currentScale.ToString ();

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = s.Content.X + xOffset;
                double deltaX = renderedX / App.ScreenWidth;
                double deltaWidth = App.ScreenWidth / (s.Content.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = s.Content.Y + yOffset;

                double deltaY = renderedY / App.ScreenHeight;
                double deltaHeight = App.ScreenHeight / (s.Content.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = xOffset - (originX * s.Content.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * s.Content.Height) * (currentScale - startScale);

                // Apply translation based on the change in origin.
                var transX = targetX.Clamp(-s.Content.Width * (currentScale - 1), 0);
                var transY = targetY.Clamp(-s.Content.Height * (currentScale - 1), 0);


                s.Content.TranslateTo(transX, transY, 0, Easing.Linear);
                // Apply scale factor.
                s.Content.Scale = currentScale;
            }
            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation applied during the pan
                xOffset = s.Content.TranslationX;
                yOffset = s.Content.TranslationY;

                // center the image if the width of the image is smaller than the screen width
                if (originalWidth * currentScale < ScreenWidth && ScreenWidth > ScreenHeight)
                    xOffset = (ScreenWidth - originalWidth * currentScale) / 2 - s.Content.X;
                else
                    xOffset = System.Math.Max(System.Math.Min(0, xOffset), -System.Math.Abs(originalWidth * currentScale - ScreenWidth));

                // center the image if the height of the image is smaller than the screen height
                if (originalHeight * currentScale < ScreenHeight && ScreenHeight > ScreenWidth)
                    yOffset = (ScreenHeight - originalHeight * currentScale) / 2 - s.Content.Y;
                else
                    yOffset = System.Math.Max(System.Math.Min((originalHeight - ScreenHeight) / 2, yOffset), -System.Math.Abs(originalHeight * currentScale - ScreenHeight - (originalHeight - ScreenHeight) / 2));

                // bounce the image back to inside the bounds
                s.Content.TranslateTo(xOffset, yOffset, 500, Easing.BounceOut);
            }
        }

    }
}