using System;
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
            var listOfNavItems = new List<NavigationItem>();
            listOfNavItems.Add(new NavigationItem { navigationItemText = "Hey 1" });
            listOfNavItems.Add(new NavigationItem { navigationItemText = "Hey 2" });
            listOfNavItems.Add(new NavigationItem { navigationItemText = "Hey 3" });
            listOfNavItems.Add(new NavigationItem { navigationItemText = "Hey 4" });
            listOfNavItems.Add(new NavigationItem { navigationItemText = "Hey 5" });

            foreach (var item in listOfNavItems)
            {
                item.NavItemText.Text = item.navigationItemText;
                item.NavItemText.Width = 500 / listOfNavItems.Count;
                NavItemStackPanel.Children.Add(item);
            }
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

        public void SecondNavigationItem_Tap(object sender, GestureEventArgs e)
        {
            MessageBus.Default.Notify("SecondNavigationItem_Tap", new Object(), new NotificationEventArgs());
        }

        public void ThirdNavigationItem_Tap(object sender, GestureEventArgs e)
        {
            MessageBus.Default.Notify("ThirdNavigationItem", new Object(), new NotificationEventArgs());
        }

        public void FourthNavigationItem_Tap(object sender, GestureEventArgs e)
        {
            MessageBus.Default.Notify("FourthNavigationItem", new Object(), new NotificationEventArgs());
        }

        public void FifthNavigationItem_Tap(object sender, GestureEventArgs e)
        {
            MessageBus.Default.Notify("FifthNavigationItem", new Object(), new NotificationEventArgs());
        }
    }
}
