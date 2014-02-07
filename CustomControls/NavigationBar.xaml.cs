﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using SimpleMvvmToolkit;

namespace PuzzleRpg.CustomControls
{
    public partial class NavigationBar : UserControl
    {
        public NavigationBar()
        {
            InitializeComponent();
            MessageBus.Default.Register("CurrentPage", GetCurrentPageAndCallHighlight);
            CreateNavigationItems();
        }

        private void GetCurrentPageAndCallHighlight(object sender, NotificationEventArgs e)
        {
            string currentPageUrl = ((PhoneApplicationFrame)Application.Current.RootVisual).CurrentSource.ToString();
            HighlightCurrentPage(currentPageUrl);
        }

        private void CreateNavigationItems()
        {
            CreateNavItem("Heroes", "HeroBox.xaml", 0);
            CreateNavItem("Team", "TeamSelection.xaml", 1);
            CreateNavItem("Dungeons", "PlayDungeon.xaml", 2);
        }

        private void CreateNavItem(string displayText, string url, int column)
        {
            var navItem = new NavigationItem();
            navItem.NavItemText.Text = displayText;
           navItem.SetValue(Grid.ColumnProperty, column);
           MainNavBar.Children.Add(navItem);
        }

        private void HighlightCurrentPage(string currentPageToHighlight)
        {
            
            //if (currentPageToHighlight == "/TeamSelection.xaml")
            //{
            //    FirstNavBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            //}
            //if (currentPageToHighlight == "/HeroBox.xaml")
            //{
            //    SecondNavBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            //}
            
        }

        public void FirstNavigationItem_Tap(object sender, GestureEventArgs e)
        {
            MessageBus.Default.Notify("FirstNavigationItem", new Object(), new NotificationEventArgs());    
        }
    }
}
