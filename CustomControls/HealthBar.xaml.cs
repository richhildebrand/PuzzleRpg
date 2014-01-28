using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using PuzzleRpg.Utils;

namespace PuzzleRpg.CustomControls
{
    public partial class HealthBar : UserControl
    {
        public HealthBar()
        {
            InitializeComponent();
            HealthPercentage.ColumnDefinitions[0].MaxWidth = HealthPercentage.ActualWidth;
        }

        public Task SetHealthPercentage(int currentHealth, int totalHealth)
        {
            var percentage = CalculateHealthPercentage(currentHealth, totalHealth);
            var healthPixels = TranslatePercentageToPixels(percentage, HealthPercentage.ActualWidth);
            return Task.WhenAll(DrawHealthBar(healthPixels));
        }
  
        private double TranslatePercentageToPixels(double percentage, double fullGridSize)
        {
            var multiplyer = percentage / 100;
            return fullGridSize * multiplyer;
        }

        private Task DrawHealthBar(double newHealthPercentage) {
            var healthColumn = HealthPercentage.ColumnDefinitions[0];
            var oldMaxWidth = healthColumn.MaxWidth;
            var newMaxWidth = newHealthPercentage;

            healthColumn.MaxWidth = newMaxWidth;
            return Task.WhenAll(AnimateHealthBar.Animate(healthColumn, oldMaxWidth, newMaxWidth));
        }

        private double CalculateHealthPercentage(int current, int total)
        {
            var percentageToReturn = ((double)current / (double)total) * 100;
            
            percentageToReturn = (percentageToReturn < 0) ? 0 : percentageToReturn;
            percentageToReturn = (percentageToReturn > 100) ? 100: percentageToReturn;

            return percentageToReturn;
        }
    }
}
