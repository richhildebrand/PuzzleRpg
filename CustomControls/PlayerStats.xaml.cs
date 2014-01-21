using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace PuzzleRpg.CustomControls
{
    public partial class PlayerStats : UserControl
    {
        public PlayerStats()
        {
            InitializeComponent();
            ColorStatBars();
        }
        
        private void ColorStatBars()
        {

            var expFillColor = new SolidColorBrush(Color.FromArgb(250, 254, 226, 116));
            ExpBar.SetColor(expFillColor);

            var stamfillColor = new SolidColorBrush(Color.FromArgb(250, 77, 212, 255));
            StamBar.SetColor(stamfillColor);
        }
    }
}
