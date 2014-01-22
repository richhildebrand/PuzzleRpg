using System;
using System.Linq;
using System.Windows.Input;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Models;
using PuzzleRpg.Monsters;
using PuzzleRpg.Screens;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg.Logic
{
    public class PuzzleGame
    {
        private readonly PuzzleGrid _puzzleGrid;
        private readonly HealthBar _playerHealth;
        private readonly Team _activeTeam;
        private readonly MonsterGrid _monsterGrid;

        public PuzzleGame(PuzzleGrid puzzleGrid, HealthBar playerHealth, Team activeTeam, MonsterGrid monsterGrid)
        {
            this._monsterGrid = monsterGrid;
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

        public void EndGame() {
            //Clear loot / drops
            PopupUtils.UncoverScreen();
            MessageBus.Default.Notify("EndGame", new Object(), new NotificationEventArgs());
        }

        private void StartTurn()
        {
            PopupUtils.UncoverScreen();
        }

        private async void OnEndTurn(object sender, NotificationEventArgs e)
        {
            PopupUtils.CoverScreen(0);
            await _puzzleGrid.MatchAndReplacePuzzlePieces();
            var matches = _puzzleGrid.MatchedOrbs;
            var currentHealth = _activeTeam.CurrentHealth;

            //PlayerHeals
            var listContainsHeals = matches.Any(s => s.Type.ToString() == "Heal");

            if (listContainsHeals)
            {
                DoHealing(currentHealth);
            }

            //MonsterTakesDamage

            //MonsterAttacks
            var remainingPlayerHealthPercentage = MonsterAttacks(_monsterGrid.ActiveMonster,
                                                                 _activeTeam);

            //PlayerDiesOrNewGameStarts
            if (remainingPlayerHealthPercentage >= 0)
            {                
                _playerHealth.SetHealthPercentage(remainingPlayerHealthPercentage);
                StartTurn();
            }
            else
            {
                DisplayDeathDialog();
                EndGame();
            }
        }
  
        private void DoHealing(int currentHealth)
        {
            var remainingPlayerHealthPercentageAfterHeals = _activeTeam.GetPercentageOfRemainingHealth(currentHealth);
            _playerHealth.SetHealthPercentage(remainingPlayerHealthPercentageAfterHeals);
        }

        private int MonsterAttacks(Monster monster, Team activePlayerTeam)
        {
            var monsterAttackDamage = monster.AttackDamage;
            activePlayerTeam.TakeDamage(1000);
            return activePlayerTeam.GetPercentageOfRemainingHealth();
        }

        private void DisplayDeathDialog()
        {
            var teamDeathDialog = new TeamDeathScreen();
            teamDeathDialog.Show();
        }
    }
}
