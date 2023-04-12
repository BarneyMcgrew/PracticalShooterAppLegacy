using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MauiPracticalShooterApp.Models;
using MauiPracticalShooterApp.Views;
using MauiPracticalShooterApp.Views.ContentViews;

namespace MauiPracticalShooterApp.ViewModels
{
    public class OnboardingPageViewModel : BaseViewModel
    {
        #region Fields

        private ObservableCollection<OnboardingContentModel> _boardings;

        private string _nextButtonText = "NEXT";

        private bool _isSkipButtonVisible = true;

        private int _selectedIndex;

        #endregion

        #region Constructor

        public OnboardingPageViewModel()
        {
            ValidateUserOnboarding();

            this.SkipCommand = new Command(this.Skip);
            this.NextCommand = new Command(this.Next);

            PopulateOnboardingPages();
        }

        private void PopulateOnboardingPages()
        {
            this.Boardings = new ObservableCollection<OnboardingContentModel>
            {
                new ()
                {
                    ImagePath = "BetaTesting.png",
                    Header = "BETA TESTING",
                    Content = "Welcome to The Practical Shooter App Beta Test.",
                    RotatorView = new WalkthroughItemView()
                },
                new ()
                {
                    ImagePath = "Feedback.png",
                    Header = "USER FEEDBACK",
                    Content = "We value your feedback, submit your feedback via our feedback page in the More tab.",
                    RotatorView = new WalkthroughItemView()
                },
                new ()
                {
                    ImagePath = "WorkInProgress.png",
                    Header = "WORK IN PROGRESS",
                    Content = "This app is still in development, and will be sculpted by your feedback and responses.",
                    RotatorView = new WalkthroughItemView()
                },
                new ()
                {
                    ImagePath = "BugReport.png",
                    Header = "BUG REPORTING",
                    Content = "As part of early access, we value your ability to report bugs. Please do as part of your feedback.",
                    RotatorView = new WalkthroughItemView()
                },
                new ()
                {
                    ImagePath = "Thanks.png",
                    Header = "THANK YOU",
                    Content = "Thank you for being a part of the process and supporting our development of the app. Enjoy!",
                    RotatorView = new WalkthroughItemView()
                }
            };

            // Set binding context to content view.
            foreach (var boarding in this.Boardings)
            {
                boarding.RotatorView.BindingContext = boarding;
            }
        }

        private void ValidateUserOnboarding()
        {
            this.Skip(null);

            // Disabled for future use
            // var alreadyOnboarded = !VersionTracking.IsFirstLaunchForCurrentBuild;
            //
            // if (alreadyOnboarded)
            //     this.Skip(null);
        }

        #endregion

        #region Properties

        public ObservableCollection<OnboardingContentModel> Boardings
        {
            get => this._boardings;

            private set
            {
                if (this._boardings == value)
                {
                    return;
                }

                this.SetProperty(ref this._boardings, value);
            }
        }

        public string NextButtonText
        {
            get => this._nextButtonText;

            set
            {
                if (this._nextButtonText == value)
                {
                    return;
                }

                this.SetProperty(ref this._nextButtonText, value);
            }
        }

        public bool IsSkipButtonVisible
        {
            get => this._isSkipButtonVisible;

            set
            {
                if (this._isSkipButtonVisible == value)
                {
                    return;
                }

                this.SetProperty(ref this._isSkipButtonVisible, value);
            }
        }

        public int SelectedIndex
        {
            get => this._selectedIndex;

            set
            {
                if (this._selectedIndex == value)
                {
                    return;
                }

                this.SetProperty(ref this._selectedIndex, value);
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
