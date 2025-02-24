﻿using PracticalShooterApp.ViewModels;
using PracticalShooterApp.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace PracticalShooterApp
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));

            Routing.RegisterRoute(nameof(OnboardingPage), typeof(OnboardingPage));

            Routing.RegisterRoute(nameof(CalendarEntryPage), typeof(CalendarEntryPage));            
            Routing.RegisterRoute(nameof(CalendarPage), typeof(CalendarPage));

            Routing.RegisterRoute(nameof(RSSPage), typeof(RSSPage));
            
            Routing.RegisterRoute(nameof(RulesPage), typeof(RulesPage));

            Routing.RegisterRoute(nameof(SearchPage), typeof(SearchPage));
            
            Routing.RegisterRoute(nameof(PDFPage), typeof(PDFPage));
            
            Routing.RegisterRoute(nameof(WebPage), typeof(WebPage));
            
            Routing.RegisterRoute(nameof(FileExplorerPage), typeof(FileExplorerPage));
            
            Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
            Routing.RegisterRoute(nameof(LearnPage), typeof(LearnPage));
            Routing.RegisterRoute(nameof(MorePage), typeof(MorePage));

            Routing.RegisterRoute(nameof(ItemsPage), typeof(ItemsPage));
            Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        }

    }
}
