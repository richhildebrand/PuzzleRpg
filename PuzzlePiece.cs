using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public class PuzzlePiece
    {
        private readonly TranslateTransform _dragTranslation;
        private double _xLocation;
        private double _yLocation;

        public Image Element { get; set; }

        public PuzzlePiece()
        {
            _dragTranslation = new TranslateTransform();
            SetPosition(0, 0);

            Element = new Image();
            Element = StyleOrb(Element);
            Element = AddTouchEvents(Element);
        }

        private void SetPosition(int x, int y) 
        {
            _xLocation = x;
            _yLocation = y;
        }

        private Image StyleOrb(Image orb)
        {
            orb = ImageUtils.GetImageFromPath("Assets/Orbs/FireOrb.png");
            orb.Stretch = System.Windows.Media.Stretch.Fill;
            return orb;
        }

        private Image AddTouchEvents(Image orb)
        {
            orb.ManipulationDelta += new EventHandler<ManipulationDeltaEventArgs>(Drag_ManipulationDelta);
            orb.ManipulationCompleted += new EventHandler<ManipulationCompletedEventArgs>(DropPuzzlePiece);
            orb.RenderTransform = this._dragTranslation;
            //orb.SetValue(MouseDragElementBehavior.ConstrainToParentBoundsProperty, true);
            return orb;
        }
  
        private void DropPuzzlePiece(object sender, ManipulationCompletedEventArgs e)
        {
            var image = sender as Image;
            GetDropRow(_yLocation,  image.ActualHeight);
            SetCorrectDropX(_xLocation, image.ActualWidth);

            // probably need to keep track of correct grid location
            //image.SetValue(Grid.ColumnProperty, GetDropRow(_yLocation));
            //image.SetValue(Grid.RowProperty, GetDropColumn(_xLocation));
        }

        private int GetDropRow(double y, double height)
        {
            string ScreenHeight = Application.Current.Host.Content.ActualHeight.ToString();
            return 0;
        }

        private void SetCorrectDropX(double x, double itemWidth)
        {
            var screenWidth  = Application.Current.Host.Content.ActualWidth;
            Debug.WriteLine(screenWidth);
            var numberOfRows = 6;
            var rowSize = screenWidth / numberOfRows;
            var nearestRow = Math.Round(x / rowSize);
            var nearestCorner = (nearestRow * rowSize);
            _dragTranslation.X += nearestCorner - x;
            _xLocation = _dragTranslation.X;
            Debug.WriteLine(_xLocation.ToString() + "x location");
            Debug.WriteLine(_dragTranslation.X.ToString() + "dragTranslation.X");
        }

        void Drag_ManipulationDelta(object sender, ManipulationDeltaEventArgs e)
        {
            Debug.WriteLine(e.DeltaManipulation.Translation.X);
            _xLocation += e.DeltaManipulation.Translation.X;
            _yLocation += e.DeltaManipulation.Translation.Y;
            Debug.WriteLine(_xLocation);

            _dragTranslation.X += e.DeltaManipulation.Translation.X;
            _dragTranslation.Y += e.DeltaManipulation.Translation.Y;
        }
    }
}
