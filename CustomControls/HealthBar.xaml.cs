using System;
using System.Linq;
using System.Windows.Controls;

namespace PuzzleRpg.CustomControls
{
    public partial class HealthBar : UserControl
    {
        public HealthBar()
        {
            InitializeComponent();
            DrawHealthBar(100);
        }

        public void SetHealthPercentage(int currentHealth, int totalHealth)
        {
            var updatedPercentage = CalculateHealthPercentage(currentHealth, totalHealth);
            DrawHealthBar(updatedPercentage);
        }

        private void DrawHealthBar(double healthPercentage) {
            HealthPercentage.ColumnDefinitions[0].Width = new System.Windows.GridLength(healthPercentage, System.Windows.GridUnitType.Star);
            HealthPercentage.ColumnDefinitions[1].Width = new System.Windows.GridLength(100 - healthPercentage, System.Windows.GridUnitType.Star);
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
