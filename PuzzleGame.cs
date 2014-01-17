using System;
using System.Linq;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Heroes;
using PuzzleRpg.Monsters;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg
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

            //PlayerHeals
            //MonsterTakesDamage

            var remainingPlayerHealthPercentage = MonsterAttacks(_monsterGrid.ActiveMonster,
                                                                 _activeTeam);

            if (remainingPlayerHealthPercentage >= 0)
            {
                _playerHealth.SetHealthPercentage(remainingPlayerHealthPercentage);
                StartTurn();
            }
            else
            {
                EndGame();
            }
        }

        private int MonsterAttacks(Monster monster, Team activePlayerTeam)
        {
            var monsterAttackDamage = _monsterGrid.ActiveMonster.AttackDamage;
            activePlayerTeam.TakeDamage(monsterAttackDamage);
            return activePlayerTeam.GetPercentageOfRemainingHealth();
        }
    }
}
