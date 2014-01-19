﻿using System;
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
            var matches = _puzzleGrid.MatchedOrbs;
            //PlayerHeals
            //MonsterTakesDamage

            var remainingPlayerHealthPercentage = MonsterAttacks(_monsterGrid.ActiveMonster,
                                                                 _activeTeam);

            var listContainsHeals = matches.Any(s => s.Type.ToString() == "Heal");            

            if (remainingPlayerHealthPercentage >= 0)
            {
                IfHealOrbsMatchedSetNewHealthPercentage(listContainsHeals, remainingPlayerHealthPercentage);
                
                _playerHealth.SetHealthPercentage(remainingPlayerHealthPercentage);
                StartTurn();
            }
            else
            {
                EndGame();
            }
        }
  
        //Rich - I don't like the name for this, what do you think? FIX IT :).
        private void IfHealOrbsMatchedSetNewHealthPercentage(bool listContainsHeals, int remainingPlayerHealthPercentage)
        {
            if (listContainsHeals)
            {
                var remainingPlayerHealthPercentageAfterHeals = CalculatePercentageOfHealthToReturn(remainingPlayerHealthPercentage);
                _playerHealth.SetHealthPercentage(remainingPlayerHealthPercentageAfterHeals);
            }
        }
  
        //Rich - This works for now. What do you think?
        //Basically if the total percentage of health will be above 100%, I just set the total percent to be 100% to avoid any weird errors.
        private int CalculatePercentageOfHealthToReturn(int remainingPlayerHealthPercentage)
        {
            var percentageToReturn = HealTeam(_activeTeam);
            var totalPercent = percentageToReturn + remainingPlayerHealthPercentage;
            if (totalPercent > 100)
            {
                percentageToReturn = 100;
            }
            return percentageToReturn;
        }

        private int MonsterAttacks(Monster monster, Team activePlayerTeam)
        {
            var monsterAttackDamage = monster.AttackDamage;
            activePlayerTeam.TakeDamage(monsterAttackDamage);
            return activePlayerTeam.GetPercentageOfRemainingHealth();
        }

        private int HealTeam(Team activePlayerTeam)
        {
            var amountToHeal = _activeTeam.GetTotalAmountOfHealPoints();
            activePlayerTeam.Heal(amountToHeal);
            return activePlayerTeam.GetPercentageOfRemainingHealth();
        }
    }
}
