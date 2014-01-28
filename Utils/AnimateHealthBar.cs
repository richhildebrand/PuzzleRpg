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
        private static TaskCompletionSource<bool> _taskSource;
        private static Storyboard _healthStoryBoard;

        public static Task Animate(ColumnDefinition column, double oldValue, double newValue)
        {
            _taskSource = new TaskCompletionSource<bool>();
            _healthStoryBoard = new Storyboard();

            DoAnimation(column, oldValue, newValue);
            _healthStoryBoard.Completed += EndAnimation;
            _healthStoryBoard.Begin();

            return _taskSource.Task;
        }

        private static void EndAnimation(object sender, EventArgs e)
        {
            _healthStoryBoard.Stop();
            _taskSource.SetResult(true);
        }
  
        private static void DoAnimation(ColumnDefinition column, double oldValue, double newValue)
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
