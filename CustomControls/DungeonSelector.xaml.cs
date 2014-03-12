using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using PuzzleRpg.Database;
using PuzzleRpg.Modals.EnterDungeonErrors;
using PuzzleRpg.Models;
using PuzzleRpg.Screens;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg.CustomControls
{
    public partial class DungeonSelector : UserControl
    {
        private Dungeon _selectedDungeon;
        private Player _player;

        public DungeonSelector()
        {
            InitializeComponent();
        }

        public void OnDungeonSelected(object sender, GestureEventArgs e)
        {
            var selectedGrid = sender as Grid;
            var dungeonId = (int)selectedGrid.Tag;
            _selectedDungeon = GetDungeon(dungeonId);

            var playerRepository = new PlayerRepository();
            _player = playerRepository.GetPlayer();

            if (PlayerCanEnterDungeon())
            {
                _player.Stam.Current = _player.Stam.Current - _selectedDungeon.StaminaCost;
                playerRepository.SavePlayer(_player);
                MessageBus.Default.Notify("EnterDungeon", new Object(), new NotificationEventArgs(dungeonId.ToString()));
            }
            else
            {
                var errorMessage = GetErrorMessage();
                var errorModalControl = new EmptyTeamError();
                var errorModal = new ModalContainer(errorModalControl);
                errorModal.Show();
            }
        }

        private string GetErrorMessage()
        {
            if (!PlayerHasEnoughStamina())
            {
                var message = "Sorry, you do not have enough stamina to enter the dungeon.";
                message += "You will gain " + AppSettings.AmountOfStaminaToAddInterval;
                message += " stamina every " + AppSettings.GainStaminaIntervalLength;
                message += " minutes";
                return message;
            }
            else if (TeamIsEmpty())
            {
                return "Please add heroes to your team.";
            }
            else
            {
                var message = "You have more heroes than you have space for.";
                message += "Please delete some heroes before proceding your next dungeon";
                return message;
            }
        }

        private bool PlayerCanEnterDungeon()
        {
            return PlayerHasEnoughStamina()
                && !TeamIsEmpty();
        }

        private bool TeamIsEmpty()
        {
            var teamRepository = new TeamRepository();
            var team = teamRepository.GetTeam();
            return team.TeamMembers.Count <= 0;
        }

        private bool PlayerHasEnoughStamina()
        {
            return _player.Stam.Current >= _selectedDungeon.StaminaCost;
        }

        private Dungeon GetDungeon(int dungeonId)
        {
            var dungeonDatabase = new DungeonDatabase();
            return dungeonDatabase.AllDungeons.Single(d => d.Id == dungeonId);
        }
    }
}
