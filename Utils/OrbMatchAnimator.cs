using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using PuzzleRpg.Models;

namespace PuzzleRpg.Utils
{
    public class OrbMatchAnimator
    {
        private TaskCompletionSource<bool> _taskSource;
        private int _matchCount;

        public OrbMatchAnimator(int matchCount)
        {
            _matchCount = matchCount;
        }

        public Task FadeOrbs(List<PuzzlePiece> puzzlePieces)
        {
            _taskSource = new TaskCompletionSource<bool>();

            var storyboard = new Storyboard();

            foreach (var puzzlePiece in puzzlePieces)
            {
                var animation = GetAnimation(puzzlePiece);
                storyboard.Children.Add(animation);
            }

            storyboard.Completed += EndAnimation;
            storyboard.Begin();

            return _taskSource.Task;
        }

        private DoubleAnimation GetAnimation(PuzzlePiece puzzlePiece)
        {
             var opacityAnimation = new DoubleAnimation
            {
                To = 0,
                From = 1,
                Duration = TimeSpan.FromMilliseconds(400)
            };

            Storyboard.SetTarget(opacityAnimation, puzzlePiece.Element);
            Storyboard.SetTargetProperty(opacityAnimation,
                                            new PropertyPath(Image.OpacityProperty));

            return opacityAnimation;
        }

        private void EndAnimation(object sender, EventArgs e)
        {
            _taskSource.SetResult(true);
        }
    }
}
