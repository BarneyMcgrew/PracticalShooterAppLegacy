using PracticalShooter.ViewModels;
using PracticalShooterLibrary.ObjectClasses.Rules;
using PracticalShooterLibrary.StaticResources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
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

            var panGesture = new PanGestureRecognizer();
            panGesture.PanUpdated += OnPanUpdated;
            ImageOverlay.GestureRecognizers.Add(panGesture);

            var pinchGesture = new PinchGestureRecognizer();
            pinchGesture.PinchUpdated += OnPinchUpdated;
            ImageOverlay.GestureRecognizers.Add(pinchGesture);

            var doubleTapGesture = new TapGestureRecognizer();
            doubleTapGesture.NumberOfTapsRequired = 2;
            doubleTapGesture.Tapped += OnDoubleTappedUpdate;
            ImageOverlay.GestureRecognizers.Add(doubleTapGesture);

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

            var doubleTapGesture = new TapGestureRecognizer();
            doubleTapGesture.NumberOfTapsRequired = 2;
            doubleTapGesture.Tapped += OnDoubleTappedUpdate;
            image.GestureRecognizers.Add(doubleTapGesture);

            viewCell.View = image;

            return viewCell;
        }

        void OnDoubleTappedUpdate(object sender, EventArgs args)
        {
            var imageSender = (Image)sender;

            ImageOverlay.Source = imageSender.Source;
            PopUpImage.IsVisible = !PopUpImage.IsVisible;

            PopUpImage.TranslationX = 0;
            PopUpImage.TranslationY = 0;
            PopUpImage.Scale = 1;
        }

        double currentScale = 1;
        double startScale = 1;
        double xOffset = 0;
        double yOffset = 0;
        bool blnDisableMove = false;

        void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            if (PopUpImage.Scale == 1)
            {
                return;
            }

            switch (e.StatusType)
            {
                case GestureStatus.Running:

                    if (!blnDisableMove)
                    {
                        PopUpImage.TranslationX = Math.Max(Math.Min(0, xOffset + (e.TotalX * Scale)), -Math.Abs((PopUpImage.Width * PopUpImage.Scale) - Application.Current.MainPage.Width));
                        PopUpImage.TranslationY = Math.Max(Math.Min(0, yOffset + (e.TotalY * Scale)), -Math.Abs((PopUpImage.Height * PopUpImage.Scale) - Application.Current.MainPage.Height));
                    }

                    break;

                case GestureStatus.Completed:

                    if (blnDisableMove)
                    {
                        blnDisableMove = false;
                        return;
                    }
                    // Store the translation applied during the pan
                    xOffset = PopUpImage.TranslationX;
                    yOffset = PopUpImage.TranslationY;
                    break;
            }
        }

        void OnPinchUpdated(object sender, PinchGestureUpdatedEventArgs e)
        {
            if (e.Status == GestureStatus.Started)
            {
                // Store the current scale factor applied to the wrapped user interface element,
                // and zero the components for the center point of the translate transform.
                startScale = PopUpImage.Scale;
                PopUpImage.AnchorX = 0;
                PopUpImage.AnchorY = 0;
                blnDisableMove = true;
            }
            if (e.Status == GestureStatus.Running)
            {
                // Calculate the scale factor to be applied.
                currentScale += (e.Scale - 1) * startScale;
                currentScale = Math.Max(1, currentScale);

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the X pixel coordinate.
                double renderedX = PopUpImage.X + xOffset;
                double deltaX = renderedX / Width;
                double deltaWidth = Width / (PopUpImage.Width * startScale);
                double originX = (e.ScaleOrigin.X - deltaX) * deltaWidth;

                // The ScaleOrigin is in relative coordinates to the wrapped user interface element,
                // so get the Y pixel coordinate.
                double renderedY = PopUpImage.Y + yOffset;
                double deltaY = renderedY / Height;
                double deltaHeight = Height / (PopUpImage.Height * startScale);
                double originY = (e.ScaleOrigin.Y - deltaY) * deltaHeight;

                // Calculate the transformed element pixel coordinates.
                double targetX = xOffset - (originX * PopUpImage.Width) * (currentScale - startScale);
                double targetY = yOffset - (originY * PopUpImage.Height) * (currentScale - startScale);

                // Apply translation based on the change in origin.
                PopUpImage.TranslationX = targetX.Clamp(-PopUpImage.Width * (currentScale - 1), 0);
                PopUpImage.TranslationY = targetY.Clamp(-PopUpImage.Height * (currentScale - 1), 0);

                // Apply scale factor.
                PopUpImage.Scale = currentScale;

                blnDisableMove = true;
            }
            if (e.Status == GestureStatus.Completed)
            {
                // Store the translation delta's of the wrapped user interface element.
                xOffset = PopUpImage.TranslationX;
                yOffset = PopUpImage.TranslationY;

                blnDisableMove = true;
            }
        }

    }
}