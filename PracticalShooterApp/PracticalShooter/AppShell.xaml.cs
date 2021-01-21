using PracticalShooter.ViewModels;
using PracticalShooter.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PracticalShooter
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

            Routing.RegisterRoute(nameof(SectionsPage), typeof(SectionsPage));
            Routing.RegisterRoute(nameof(PDFPage), typeof(PDFPage));
            Routing.RegisterRoute(nameof(AppendixPage), typeof(AppendixPage));
        }

    }
}
