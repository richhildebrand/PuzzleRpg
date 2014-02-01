using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using PuzzleRpg.Models;

namespace PuzzleRpg.Utils
{
    public static class AnimateMatch
    {
        private static List<PuzzlePiece> _puzzlePieces;
        private static TaskCompletionSource<bool> _taskSource;

        public static Task FadeOrbs(List<PuzzlePiece> puzzlePieces)
        {
            _taskSource = new TaskCompletionSource<bool>();

            _puzzlePieces = puzzlePieces;
            foreach (var piece in puzzlePieces)
            {
                FadePiece(piece);
            }
            AppGlobals.PuzzleStoryBoard.Completed += EndAnimation;
            AppGlobals.PuzzleStoryBoard.Begin();
            return _taskSource.Task;
        }
  
        private static void EndAnimation(object sender, EventArgs e)
        {
            AppGlobals.PuzzleStoryBoard.Stop();
            AppGlobals.PuzzleStoryBoard = new Storyboard();
            _taskSource.SetResult(true);
        }

        private static void FadePiece(PuzzlePiece piece)
        {
            var image = piece.Element;
            image.RenderTransform = piece._dragTranslation;

            DoubleAnimation moveAnim = new DoubleAnimation();
            moveAnim.Duration = TimeSpan.FromMilliseconds(400);

            moveAnim.From = 1;
            moveAnim.To = 0;

            Storyboard.SetTarget(moveAnim, image);
            Storyboard.SetTargetProperty(moveAnim, new PropertyPath(SolidColorBrush.OpacityProperty));
            AppGlobals.PuzzleStoryBoard.Children.Add(moveAnim);
        }
    }
}
