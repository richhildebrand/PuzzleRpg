using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace PuzzleRpg.Utils
{
    public static class AnimatedMoves
    {
        public static void DropOrbs(List<PuzzlePiece> puzzlePieces)
        {
            foreach (var piece in puzzlePieces)
            {
                MovePiece(piece);
            }
            AppGlobals.PuzzleStoryBoard.Completed += EndAnimation; // += new System.EventHandler(storyboard_Completed);
            AppGlobals.PuzzleStoryBoard.Begin();
        }
  
        private static void EndAnimation(object sender, EventArgs e)
        {
            AppGlobals.PuzzleStoryBoard.Stop();
            //empty storyboard?
            AppGlobals.PuzzleStoryBoard = new Storyboard();
            //TODO: reactivate touch 
        }

        private static void MovePiece(PuzzlePiece piece)
        {
            var image = piece.Element;
            image.RenderTransformOrigin = new Point(0.5, 0.5);
            image.RenderTransform = piece._dragTranslation;

            DoubleAnimation moveAnim = new DoubleAnimation();
            moveAnim.Duration = TimeSpan.FromMilliseconds(400);

            moveAnim.From = piece._dragTranslation.Y;

            piece.SetPosition(piece.Location.Row, piece.Location.Column);
            moveAnim.To = piece._dragTranslation.Y;

            Storyboard.SetTarget(moveAnim, image);
            Storyboard.SetTargetProperty(moveAnim, new PropertyPath("(UIElement.RenderTransform).(TranslateTransform.Y)"));
            AppGlobals.PuzzleStoryBoard.Children.Add(moveAnim);
        }
    }
}