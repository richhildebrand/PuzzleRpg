using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PuzzleRpg.Resources;

namespace PuzzleRpg
{
    public partial class MainPage : PhoneApplicationPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void MouseMoving(object sender, MouseEventArgs e)
        {
            var image = (System.Windows.Controls.Image)sender;
            Canvas.SetZIndex(image, 1);
            var x = 7;
        }

        public void OnDragEnd(object sender, ManipulationCompletedEventArgs e)
        {
            var image = (System.Windows.Controls.Image)sender;
            Canvas.SetZIndex(image, 0);
            var x = 8;
        }
    }
}