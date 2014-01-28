using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace PuzzleRpg.Utils
{
    public class AnimateHealthBar
    {
        private TaskCompletionSource<bool> _taskSource;
        private Storyboard _healthStoryBoard;

        public Task Animate(ColumnDefinition column, double oldValue, double newValue)
        {
            _taskSource = new TaskCompletionSource<bool>();
            _healthStoryBoard = new Storyboard();

            DoAnimation(column, oldValue, newValue);
            _healthStoryBoard.Completed += EndAnimation;
            _healthStoryBoard.Begin();

            return _taskSource.Task;
        }

        private void EndAnimation(object sender, EventArgs e)
        {
            _healthStoryBoard.Stop();
            _taskSource.SetResult(true);
        }
  
        private void DoAnimation(ColumnDefinition column, double oldValue, double newValue)
        {
            Duration duration = new Duration(TimeSpan.FromMilliseconds(1000));
            CubicEase ease = new CubicEase { EasingMode = EasingMode.EaseOut };

            DoubleAnimation animation = new DoubleAnimation();
            animation.EasingFunction = ease;
            animation.Duration = duration;
            _healthStoryBoard.Children.Add(animation);
            animation.From = oldValue;
            animation.To = newValue;
            Storyboard.SetTarget(animation, column);
            Storyboard.SetTargetProperty(animation, new PropertyPath("(ColumnDefinition.MaxWidth)"));
        }
    }
}
