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
            HealthPercentage.ColumnDefinitions[0].MaxWidth = 100;
            DrawHealthBar(100);
        }

        public Task SetHealthPercentage(int currentHealth, int totalHealth)
        {
            var updatedPercentage = CalculateHealthPercentage(currentHealth, totalHealth);
            return Task.WhenAll(DrawHealthBar(updatedPercentage));
        }

        private Task DrawHealthBar(double newHealthPercentage) {
            var healthColumn = HealthPercentage.ColumnDefinitions[0];
            var oldMaxWidth = healthColumn.MaxWidth;
            var newMaxWidth = newHealthPercentage;

            //HealthPercentage.ColumnDefinitions[1].Width = new System.Windows.GridLength(100 - newHealthPercentage, System.Windows.GridUnitType.Star);
            var booHiss = new AnimateHealthBar();
            return Task.WhenAll(booHiss.Animate(healthColumn, oldMaxWidth, newMaxWidth));
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
