using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Models;
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

        public void Unregister()
        {
            MessageBus.Default.Unregister("EndTurn", OnEndTurn);
        }

        public async void StartGame()
        {
            await _puzzleGrid.MatchAndReplacePuzzlePieces();
            AppGlobals.ActiveDungeonScore.DungeonInProgress = true;
            StartNewTurn();
        }

        public void EndGame() {
            PopupUtils.UncoverScreen();
            MessageBus.Default.Notify("EndGame", new Object(), new NotificationEventArgs());
        }

        private void StartNewTurn()
        {
            PopupUtils.UncoverScreen();
        }

        private async void OnEndTurn(object sender, NotificationEventArgs e)
        {
            PopupUtils.CoverScreen(5);
            await _puzzleGrid.MatchAndReplacePuzzlePieces();
            var matches = _puzzleGrid.MatchedOrbs;

            await PlayerHeals(_activeTeam, matches, _playerHealth);
            await PlayerAttacksMonster(_monsterGrid, _activeTeam, matches);

            if (MonsterIsAlive())
            {
                await MonsterAttacksPlayer(_monsterGrid.ActiveMonster, _activeTeam, _playerHealth);
                if (PlayerIsAlive())
                {
                    StartNewTurn();
                }
                else 
                {
                    //This was annoying me so I commented it out lol
                    //DisplayDeathDialog();
                    AppGlobals.ActiveDungeonScore.PlayerWins = false;
                    EndGame();
                }
            }
            else
            {
                await _monsterGrid.AnimateMonsterDeath();
                AppGlobals.ActiveDungeonScore.MonstersSlain.Add(_monsterGrid.ActiveMonster);
                var hasAnotherFloor = _monsterGrid.LoadNextFloor();
                if (hasAnotherFloor)
                {
                    StartNewTurn();
                }
                else
                {
                    //This was annoying me so I commented it out lol
                    //DisplayDeathDialog();
                    AppGlobals.ActiveDungeonScore.PlayerWins = true;
                    EndGame();
                }
            }
        }

        private bool PlayerIsAlive()
        {
            return _activeTeam.CurrentHealth > 0;
        }

        private bool MonsterIsAlive()
        {
            return _monsterGrid.ActiveMonster.CurrentHealth >= 0;
        }
  
        private Task PlayerHeals(Team activeTeam, List<OrbMatch> matches, HealthBar playerHealth)
        {
            activeTeam.Heal(matches);
            return playerHealth.SetHealthPercentage(_activeTeam.CurrentHealth, _activeTeam.TotalHealth);
        }

        private Task PlayerAttacksMonster(MonsterGrid monsterGrid, Team activeTeam, List<OrbMatch> matches)
        {
            var playerAttack = activeTeam.CalculateDamage(matches);
            var monster = monsterGrid.ActiveMonster;
            monster.TakeDamage(playerAttack);
            return monsterGrid.MonsterHealth.SetHealthPercentage(monster.CurrentHealth, monster.TotalHealth);
        }

        private Task MonsterAttacksPlayer(Monster monster, Team activePlayerTeam, HealthBar playerHealth)
        {
            var monsterAttackDamage = monster.AttackDamage;
            activePlayerTeam.TakeDamage(1000);
            return playerHealth.SetHealthPercentage(activePlayerTeam.CurrentHealth, activePlayerTeam.TotalHealth);
        }

        private void DisplayDeathDialog()
        {
            var teamDeathDialog = new TeamDeathScreen("your dead");
            teamDeathDialog.Show();
        }
    }
}
