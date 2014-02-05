using System;
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
            MessageBus.Default.Register("CurrentPage", GetCurrentPage);
        }

        private void GetCurrentPage(object sender, NotificationEventArgs e)
        {
            string thing = ((PhoneApplicationFrame)Application.Current.RootVisual).CurrentSource.ToString();
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
