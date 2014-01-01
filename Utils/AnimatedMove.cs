using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PuzzleRpg.Utils
{
    public static class AnimatedMove
    {
        public static void MoveImage(Image orbImage)
        {
            TranslateTransform trans = new TranslateTransform() { X = 1.0, Y = 1.0 };
            orbImage.RenderTransformOrigin = new Point(0.5, 0.5);
            orbImage.RenderTransform = trans;

            DoubleAnimation moveAnim = new DoubleAnimation();
            moveAnim.Duration = TimeSpan.FromMilliseconds(1200);
            moveAnim.From = -100;
            moveAnim.To = 100;
            Storyboard.SetTarget(moveAnim, orbImage);
            Storyboard.SetTargetProperty(moveAnim, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
            //storyboard.Completed += new System.EventHandler(storyboard_Completed);
            AppGlobals.PuzzleStoryBoard.Children.Add(moveAnim);
            AppGlobals.PuzzleStoryBoard.Begin();
        }
    }
}
