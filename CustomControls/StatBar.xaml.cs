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
    }
}
