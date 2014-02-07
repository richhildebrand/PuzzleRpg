using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Phone.Controls;
using SimpleMvvmToolkit;

namespace PuzzleRpg.CustomControls
{
    public partial class NavigationBar : UserControl
    {
        public NavigationBar()
        {
            InitializeComponent();
            //MessageBus.Default.Register("CurrentPage", GetCurrentPageAndCallHighlight);
            CreateNavigationItems();
        }

        private void CreateNavigationItems()
        {
            CreateNavItem("Heroes", "/HeroBox.xaml", 0);
            CreateNavItem("Team", "/TeamSelection.xaml", 1);
            CreateNavItem("Dungeons", "/DungeonSelection.xaml", 2);
        }

        private void CreateNavItem(string displayText, string url, int column)
        {
           var navItem = new NavigationItem();
           navItem.NavItemText.Text = displayText;
           navItem.SetValue(Grid.ColumnProperty, column);
           navItem.Tap += NavigateToPage;
           navItem.Tag += url;
           MainNavBar.Children.Add(navItem);
        }

        public void NavigateToPage(object sender, GestureEventArgs e)
        {
            var navItem = sender as NavigationItem;
            var url = (string)navItem.Tag;
            MessageBus.Default.Notify("NavigateToPage", new Object(), new NotificationEventArgs(url));
        }

        public void HighlightPage(string currentPageToHighlight)
        {
            var navItems = new List<NavigationItem>();
            var uiItems = MainNavBar.Children;

            foreach (var uiItem in uiItems)
            {
                var navItem = uiItem as NavigationItem;
                navItems.Add(navItem);
            }

            var activeItem = navItems.Single(ni => ni.Tag == currentPageToHighlight);
            activeItem.NavItemBorder.Background = new SolidColorBrush(Colors.Yellow); 
        }
    }
}
