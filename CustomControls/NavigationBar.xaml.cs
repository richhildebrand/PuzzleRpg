using System;
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
            MessageBus.Default.Register("CurrentPage", GetCurrentPageAndCallHighlight);
        }

        private void GetCurrentPageAndCallHighlight(object sender, NotificationEventArgs e)
        {
            string currentPageUrl = ((PhoneApplicationFrame)Application.Current.RootVisual).CurrentSource.ToString();
            HighlightCurrentPage(currentPageUrl);
        }

        private void HighlightCurrentPage(string currentPageToHighlight)
        {
            
            if (currentPageToHighlight == "/TeamSelection.xaml")
            {
                FirstNavBorder.BorderBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }
            
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
