using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using PracticalShooterApp.Models;
using PracticalShooterApp.Templates;
using PracticalShooterApp.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace PracticalShooterApp.ViewModels
{
    public class OnboardingPageViewModel : BaseViewModel
    {
        #region Fields

        private ObservableCollection<BoardingContentModel> boardings;

        private string nextButtonText = "NEXT";

        private bool isSkipButtonVisible = true;

        private int selectedIndex;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="OnBoardingAnimationViewModel" /> class.
        /// </summary>
        public OnboardingPageViewModel()
        {
            ValidateUserOnboarding();
            
            this.SkipCommand = new Command(this.Skip);
            this.NextCommand = new Command(this.Next);

            PopulateOnboardingPages();            
        }

        private void PopulateOnboardingPages()
        {
            this.Boardings = new ObservableCollection<BoardingContentModel>
            {
                new BoardingContentModel()
                {
                    ImagePath = "BetaTesting.png",
                    Header = "BETA TESTING",
                    Content = "Welcome to The Practical Shooter App Beta Test.",
                    RotatorView = new WalkthroughItemPage()
                },
                new BoardingContentModel()
                {
                    ImagePath = "Feedback.png",
                    Header = "USER FEEDBACK",
                    Content = "We value your feedback, submit your feedback via our feedback page in the More tab.",
                    RotatorView = new WalkthroughItemPage()
                },
                new BoardingContentModel()
                {
                    ImagePath = "WorkInProgress.png",
                    Header = "WORK IN PROGRESS",
                    Content = "This app is still in development, and will be sculpted by your feedback and responses.",
                    RotatorView = new WalkthroughItemPage()
                },
                new BoardingContentModel()
                {
                    ImagePath = "BugReport.png",
                    Header = "BUG REPORTING",
                    Content = "As part of early access, we value your ability to report bugs. Please do as part of your feedback.",
                    RotatorView = new WalkthroughItemPage()
                },
                new BoardingContentModel()
                {
                    ImagePath = "Thanks.png",
                    Header = "THANK YOU",
                    Content = "Thank you for being a part of the process and supporting our development of the app. Enjoy!",
                    RotatorView = new WalkthroughItemPage()
                }
            };

            // Set bindingcontext to content view.
            foreach (var boarding in this.Boardings)
            {
                boarding.RotatorView.BindingContext = boarding;
            }
        }

        private void ValidateUserOnboarding()
        {
            var alreadyOnboarded = !VersionTracking.IsFirstLaunchForCurrentBuild;
            
            if (alreadyOnboarded)
                this.Skip(null);
        }

        #endregion

        #region Properties

        public ObservableCollection<BoardingContentModel> Boardings
        {
            get
            {
                return this.boardings;
            }

            private set
            {
                if (this.boardings == value)
                {
                    return;
                }

                this.SetProperty(ref this.boardings, value);
            }
        }

        public string NextButtonText
        {
            get
            {
                return this.nextButtonText;
            }

            set
            {
                if (this.nextButtonText == value)
                {
                    return;
                }

                this.SetProperty(ref this.nextButtonText, value);
            }
        }

        public bool IsSkipButtonVisible
        {
            get
            {
                return this.isSkipButtonVisible;
            }

            set
            {
                if (this.isSkipButtonVisible == value)
                {
                    return;
                }

                this.SetProperty(ref this.isSkipButtonVisible, value);
            }
        }

        public int SelectedIndex
        {
            get
            {
                return this.selectedIndex;
            }

            set
            {
                if (this.selectedIndex == value)
                {
                    return;
                }

                this.SetProperty(ref this.selectedIndex, value);
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that is executed when the Skip button is clicked.
        /// </summary>
        public ICommand SkipCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Done button is clicked.
        /// </summary>
        public ICommand NextCommand { get; set; }

        #endregion

        #region Methods

        private static async void MoveToNextPage()
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        private bool ValidateAndUpdateSelectedIndex(int itemCount)
        {
            if (this.SelectedIndex >= itemCount - 1)
            {
                return true;
            }

            this.SelectedIndex++;
            return false;
        }

        /// <summary>
        /// Invoked when the Skip button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void Skip(object obj)
        {
            MoveToNextPage();
        }

        /// <summary>
        /// Invoked when the Done button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        public void Next(object obj)
        {
            if (this.ValidateAndUpdateSelectedIndex(this.Boardings.Count))
            {
                MoveToNextPage();
            }
        }

        #endregion
    }
}
