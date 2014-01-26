using System;
using System.Collections.Generic;
using System.Linq;
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
            _monsterGrid = monsterGrid;
            _activeTeam = activeTeam;
            _playerHealth = playerHealth;
            _puzzleGrid = puzzleGrid;
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

            PlayerAttacksMonster(_monsterGrid, _activeTeam, matches);
            MonsterAttacksPlayer(_monsterGrid.ActiveMonster, _activeTeam, _playerHealth);

            //PlayerDiesOrNewGameStarts
            if (_activeTeam.CurrentHealth >= 0)
            {                
                StartTurn();
            }
            else
            {
                //This was annoying me so I commented it out lol
                //DisplayDeathDialog();
                EndGame();
            }
        }
  
        private void PlayerAttacksMonster(MonsterGrid monsterGrid, Team activeTeam, List<OrbMatch> matches)
        {
            var playerAttack = activeTeam.CalculateDamage(matches);
            var monster = monsterGrid.ActiveMonster;
            monster.TakeDamage(playerAttack);
            monsterGrid.MonsterHealth.SetHealthPercentage(monster.CurrentHealth, monster.TotalHealth);
        }
  
        private void DoHealing(int currentHealth)
        {
            _playerHealth.SetHealthPercentage(_activeTeam.CurrentHealth, _activeTeam.TotalHealth);
        }

        private void MonsterAttacksPlayer(Monster monster, Team activePlayerTeam, HealthBar playerHealth)
        {
            var monsterAttackDamage = monster.AttackDamage;
            activePlayerTeam.TakeDamage(1000);
            playerHealth.SetHealthPercentage(activePlayerTeam.CurrentHealth, activePlayerTeam.TotalHealth);
        }

        private void DisplayDeathDialog()
        {
            var teamDeathDialog = new TeamDeathScreen();
            teamDeathDialog.Show();
        }
    }
}
