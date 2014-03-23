using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace PuzzleRpg.CustomControls
{
    public partial class StatBar : UserControl
    {
        public StatBar()
        {
            InitializeComponent();
        }

        public void SetColor(SolidColorBrush fillColor)
        {
            ThisStatBar.Fill = fillColor;
        }

        public void SetFillPercentage(int current, int total)
        {
            current = PreventZero(current);
            var fillPercentage = ((double)current / (double)total) * 100;
            fillPercentage = (fillPercentage < 0) ? 0 : fillPercentage;
            fillPercentage = (fillPercentage > 100) ? 100 : fillPercentage;

            StatPercentage.ColumnDefinitions[0].Width = new System.Windows.GridLength(fillPercentage, System.Windows.GridUnitType.Star);
            StatPercentage.ColumnDefinitions[1].Width = new System.Windows.GridLength(100 - fillPercentage, System.Windows.GridUnitType.Star);
        }

        private int PreventZero(int number) 
        {
            if (number == 0)
            {
                number = 1;
            }
            return number;
        }
    }
}
