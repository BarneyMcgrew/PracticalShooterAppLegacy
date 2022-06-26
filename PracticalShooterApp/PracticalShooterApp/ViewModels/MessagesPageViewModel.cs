using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using PracticalShooterApp.Models;
using Xamarin.Forms;

namespace PracticalShooterApp.ViewModels
{
    public class MessagesPageViewModel : BaseViewModel
    {
        private List<InAppMessageModel> _items = new List<InAppMessageModel>();

        private Command<object> backButtonCommand;
        private Command<object> itemTappedCommand;

        [DataMember(Name = "navigationList")]
        public ObservableCollection<InAppMessageModel> NavigationList { get; set; }
            = new ObservableCollection<InAppMessageModel>();

        public Command<object> BackButtonCommand => this.backButtonCommand ??
                                                    (this.backButtonCommand = new Command<object>(BackButtonClicked));

        public Command<object> ItemTappedCommand
        {
            get
            {
                return this.itemTappedCommand ??
                       (this.itemTappedCommand = new Command<object>(this.NavigateToNextPage));
            }
        }

        public MessagesPageViewModel()
        {
            
        }

        public async void BackButtonClicked(object obj)
        {
            await Shell.Current.GoToAsync("..", true);
        }

        private async void NavigateToNextPage(object selectedItem)
        {
            // add manual navigation
        }
    }
}