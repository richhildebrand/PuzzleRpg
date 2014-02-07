using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using SimpleMvvmToolkit;

namespace PuzzleRpg.CustomControls
{
    public partial class NavigationItem : UserControl
    {
        public string navigationItemText { get; set; }

        public NavigationItem()
        {
            InitializeComponent();
        }

        public void FirstNavigationItem_Tap(object sender, GestureEventArgs e)
        {
            MessageBus.Default.Notify("FirstNavigationItem", new Object(), new NotificationEventArgs());
        }

        public void GenerateNavItem(NavigationItem item)
        {
            NavItemText.Text = navigationItemText;
        }
    }
}
