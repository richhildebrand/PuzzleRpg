using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;
using PuzzleRpg.Database;
using PuzzleRpg.Models;

namespace PuzzleRpg.CustomControls
{
    public partial class PlayerStats : UserControl
    {
        public PlayerStats()
        {
            var playerRepository = new PlayerRepository();
            var player = playerRepository.GetPlayer();

            InitializeComponent();
            ColorStatBars(player);
        }

        private void ColorStatBars(Player player)
        {
            var expFillColor = new SolidColorBrush(Color.FromArgb(250, 254, 226, 116));
            ExpBar.SetColor(expFillColor);
            ExpBar.SetFillPercentage(player.Exp.Current, player.Exp.Max);

            var stamfillColor = new SolidColorBrush(Color.FromArgb(250, 77, 212, 255));
            StamBar.SetColor(stamfillColor);
            StamBar.SetFillPercentage(player.Stam.Current, player.Stam.Max);

        }
    }
}
