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

            //This will probably change :)
            var remainingPlayerHealthPercentageAfterHeals = CalculatePercentageOfHealthToReturn(remainingPlayerHealthPercentage);

            if (remainingPlayerHealthPercentage >= 0)
            {
                //This will probably change as well
                _playerHealth.SetHealthPercentage(remainingPlayerHealthPercentageAfterHeals);
                _playerHealth.SetHealthPercentage(remainingPlayerHealthPercentage);
                StartTurn();
            }
            else
            {
                EndGame();
            }
        }
  
        //This method is obviously need to change but I just wanted to see if the HealsFor prop was working.
        //It will need to check on the orb type that was matched.
        //If it is heal orb then heal the "TotalHealsFor" amount up to 100%
        private int CalculatePercentageOfHealthToReturn(int remainingPlayerHealthPercentage)
        {
            var percentageToReturn = 0;
            //Currently will heal automatically if the team's health percentage drops under 80%
            if (remainingPlayerHealthPercentage < 80)
            {
                percentageToReturn = HealTeam(_activeTeam);
            }
            else
            {
                percentageToReturn = 0;
            }
            return percentageToReturn;
        }

        //When it comes to this card, can't we remove the first parameter? Above when this
        //Function is called it passes in the same _monsterGrid.ActiveMonster (which is global)
        //Unless this is going to change at some point.
        private int MonsterAttacks(Monster monster, Team activePlayerTeam)
        {
            var monsterAttackDamage = _monsterGrid.ActiveMonster.AttackDamage;
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
