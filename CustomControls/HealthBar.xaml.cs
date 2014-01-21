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
            SetHealthPercentage(100);
        }

        public void SetHealthPercentage(double healthPercentage) {
            HealthPercentage.ColumnDefinitions[0].Width = new System.Windows.GridLength(healthPercentage, System.Windows.GridUnitType.Star);
            HealthPercentage.ColumnDefinitions[1].Width = new System.Windows.GridLength(100 - healthPercentage, System.Windows.GridUnitType.Star);
        }
    }
}
