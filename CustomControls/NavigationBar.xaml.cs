using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using SimpleMvvmToolkit;

namespace PuzzleRpg.CustomControls
{
    public partial class NavigationBar : UserControl
    {
        public NavigationBar()
        {
            InitializeComponent();
        }

        private void FirstNavigationItem_Tap(object sender, GestureEventArgs e)
        {
            var target = sender as TextBlock;
            if (target.Tag != null)
            {
                var targetId = target.Tag.ToString();
                MessageBus.Default.Notify("FirstNavigationItem", new Object(), new NotificationEventArgs(targetId));
            }
        }

        private void SecondNavigationItem_Tap(object sender, GestureEventArgs e)
        {

        }

        private void ThirdNavigationItem_Tap(object sender, GestureEventArgs e)
        {

        }

        private void FourthNavigationItem_Tap(object sender, GestureEventArgs e)
        {

        }
    }
}
