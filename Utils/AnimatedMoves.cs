using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Animation;
using PuzzleRpg.Models;
using SimpleMvvmToolkit;

namespace PuzzleRpg.Utils
{
    public static class AnimatedMoves
    {
        private static List<PuzzlePiece> _puzzlePieces;
        private static TaskCompletionSource<bool> _taskSource;

        public static Task DropOrbs(List<PuzzlePiece> puzzlePieces)
        {
            _taskSource = new TaskCompletionSource<bool>();

            _puzzlePieces = puzzlePieces;
            foreach (var piece in puzzlePieces)
            {
                MovePiece(piece);
            }
            AppGlobals.PuzzleStoryBoard.Completed += EndAnimation;
            AppGlobals.PuzzleStoryBoard.Begin();
            return _taskSource.Task;
        }
  
        private static void EndAnimation(object sender, EventArgs e)
        {
            AppGlobals.PuzzleStoryBoard.Stop();
            AppGlobals.PuzzleStoryBoard = new Storyboard();
            RestoreTouchEvents(_puzzlePieces);
            _taskSource.SetResult(true);
        }

        private static void RestoreTouchEvents(List<PuzzlePiece> puzzlePieces)
        {
            foreach (var piece in puzzlePieces)
            {
                piece.AddTouchEvents(piece.Element);
            }
        }

        private static void MovePiece(PuzzlePiece piece)
        {
            var image = piece.Element;
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