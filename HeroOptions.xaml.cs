﻿using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using SimpleMvvmToolkit;

namespace PuzzleRpg
{
    public partial class HeroOptions : PhoneApplicationPage
    {
        public HeroOptions()
        {
            InitializeComponent();
        }

        public void NavigateOnTap(object sender, GestureEventArgs e)
        {
            var target = sender as FrameworkElement;
            var destination = (string)target.Tag;
            NavigationService.Navigate(new Uri(destination, UriKind.RelativeOrAbsolute));
        }

        private void OnNavItemTapped(object sender, NotificationEventArgs e)
        {
            var url = e.Message;
            NavigationService.Navigate(new Uri(url, UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            NavBar.HighlightPage("/HeroOptions.xaml");
            MessageBus.Default.Register("NavigateToPage", OnNavItemTapped);

        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            MessageBus.Default.Unregister("NavigateToPage", OnNavItemTapped);
        }
    }
}