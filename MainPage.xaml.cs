﻿using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;

namespace PuzzleRpg
{
    public partial class MainPage : PhoneApplicationPage
    {
        PuzzleGrid _puzzleGrid;

        public MainPage()
        {
            InitializeComponent();
            _puzzleGrid = new PuzzleGrid(PuzzleGrid, 5, 6);
            _puzzleGrid.AddOrbs();
        }

        private void MouseMoving(object sender, MouseEventArgs e)
        {
            var image = (System.Windows.Controls.Image)sender;
            Canvas.SetZIndex(image, 1);
        }
        
        public void OnDragEnd(object sender, ManipulationCompletedEventArgs e)
        {
            var image = (System.Windows.Controls.Image)sender;
            Canvas.SetZIndex(image, 0);
        }
    }
}