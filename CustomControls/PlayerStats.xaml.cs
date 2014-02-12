using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PuzzleRpg.Database;
using PuzzleRpg.Models;

namespace PuzzleRpg.CustomControls
{
    public partial class PlayerStats : UserControl
    {
        private PlayerRepository _playerRepository;
        private Player _player;

        public PlayerStats()
        {
            InitializeComponent();
            this.Loaded += StatBars;
        }

        private void StatBars(object sender, RoutedEventArgs e)
        {
            _playerRepository = new PlayerRepository();
            _player = _playerRepository.GetPlayer();

            UpdateStamina(_player);
            ColorStatBars(_player);
        }
  
        private void UpdateStamina(Player player)
        {
            if (ShouldUpdateStamina())
            {
                var additionalStamina = AmountToUpdateStaminaBy();
                player.Stam.Current += additionalStamina;
                _playerRepository.SavePlayer(player);
            }
        }

        private int AmountToUpdateStaminaBy()
        {
            var timeElapsed = DateTime.Now - _player.Stam.LastGainedStamina;
            var intervalsPassed = Convert.ToInt32(timeElapsed.Ticks / AppSettings.GainStaminaIntervalLength.Ticks);
            return intervalsPassed * AppSettings.AmountOfStaminaToAddInterval;
        }

        private bool ShouldUpdateStamina()
        {
            var shouldHaveUpdatedStamina = _player.Stam.LastGainedStamina + AppSettings.GainStaminaIntervalLength;
           return shouldHaveUpdatedStamina < DateTime.Now; 
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
