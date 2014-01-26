﻿using System;
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

            //Player attacks 
            PlayerAttacksMonster(_monsterGrid, _activeTeam, matches);

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
                //This was annoying me so I commented it out lol
                //DisplayDeathDialog();
                EndGame();
            }
        }
  
        private void PlayerAttacksMonster(MonsterGrid monsterGrid, Team activeTeam, List<OrbMatch> matches)
        {
            monsterGrid.ActiveMonster.TakeDamage(100);
            var remainingHealthPercent = monsterGrid.ActiveMonster.GetTotalPercentageOfHealPoints(); 
            monsterGrid.MonsterHealth.SetHealthPercentage(remainingHealthPercent);
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
