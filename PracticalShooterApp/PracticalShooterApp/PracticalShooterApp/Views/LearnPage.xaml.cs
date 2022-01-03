using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PracticalShooterApp.Clients;
using PracticalShooterApp.DataModels;
using PracticalShooterApp.Enums;
using PracticalShooterApp.Models;
using PracticalShooterApp.Services.Interfaces;
using PracticalShooterApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooterApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LearnPage : ContentPage
    {
        private readonly LearnPageViewModel _viewModel = new LearnPageViewModel();
        
        public LearnPage()
        {
            InitializeComponent();

            this.BindingContext = _viewModel;

            SetupListViewGrouping();
        }

        public void SetupListViewGrouping()
        {
            sectionList.DataSource.GroupDescriptors.Add(new Syncfusion.DataSource.GroupDescriptor()
            {
                PropertyName = "ChapterGrouping",
                KeySelector = (object obj1) =>
                {
                    var item = (obj1 as LearnSectionsModel);
                    return item.ChapterGrouping;
                }
            });
        }
    }
}