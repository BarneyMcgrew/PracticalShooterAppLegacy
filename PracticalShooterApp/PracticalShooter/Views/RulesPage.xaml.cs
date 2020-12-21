using PracticalShooter.ViewModels;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PracticalShooter.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RulesPage : ContentPage
    {
        private RulesViewModel _viewModel;

        public RulesPage()
        {
            InitializeComponent();

            BindingContext = _viewModel = new RulesViewModel();

            HeaderTab.GestureRecognizers.Add(new TapGestureRecognizer((view) => DisciplineNameTapped()));
        }

        private void DisciplineNameTapped()
        {
            SelectedDisciplinePicker.Focus();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.OnAppearing();
        }

        private void SelectedDisciplinePicker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            _viewModel.SelectedDisciplinePickerChanged.Execute(null);
        }

        private void SearchBarOnly_TextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.SearchBarTextChanged.Execute(null);
        }

        private void SectionSelected(object sender, SelectedItemChangedEventArgs e)
        {
            _viewModel.SectionSelected.Execute(null);
        }
    }
}