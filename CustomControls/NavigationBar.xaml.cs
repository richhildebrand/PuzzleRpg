using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace PuzzleRpg.CustomControls
{
    public partial class NavigationBar : UserControl
    {
        public NavigationBar()
        {
            InitializeComponent();
        }

        public void FirstNavigationItem_Tap(object sender, GestureEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/HeroBox.xaml", UriKind.RelativeOrAbsolute));
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
