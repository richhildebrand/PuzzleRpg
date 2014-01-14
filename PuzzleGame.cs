using System;
using System.Linq;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Heroes;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg
{
    public class PuzzleGame
    {
        private readonly PuzzleGrid _puzzleGrid;
        private readonly HealthBar _playerHealth;
        private readonly Team _activeTeam;

        public PuzzleGame(PuzzleGrid puzzleGrid, HealthBar playerHealth, Team activeTeam)
        {
            this._activeTeam = activeTeam;
            this._playerHealth = playerHealth;
            this._puzzleGrid = puzzleGrid;
            MessageBus.Default.Register("EndTurn", OnEndTurn);
        }

        public async void StartGame()
        {
            await _puzzleGrid.MatchAndReplacePuzzlePieces();
            StartTurn();
        }

        private void StartTurn()
        {
            PopupUtils.UncoverScreen();
        }

        private async void OnEndTurn(object sender, NotificationEventArgs e)
        {
            PopupUtils.CoverScreen(0);
            await _puzzleGrid.MatchAndReplacePuzzlePieces();
            //PlayerHeals
            //MonsterTakesDamage

            //MonsterAttacks
            var monsterAttackDamage = 10;
            _activeTeam.TakeDamage(monsterAttackDamage);
            _playerHealth.SetHealthPercentage(_activeTeam.GetPercentageOfRemainingHealth());

            StartTurn();
        }
    }
}
