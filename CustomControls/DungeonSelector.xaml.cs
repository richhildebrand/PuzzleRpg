using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using PuzzleRpg.Database;
using PuzzleRpg.Interface;
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
                var errorModalControl = GetErrorMessage();
                var errorModal = new ModalContainer(errorModalControl);
                errorModal.Show();
            }
        }

        private IModalControl GetErrorMessage()
        {
            if (!PlayerHasEnoughStamina())
            {
                return new NotEnoughStaminaError();
            }
            else if (TeamIsEmpty())
            {
                return new EmptyTeamError();
            }
            else
            {
                var message = "You have more heroes than you have space for.";
                message += "Please delete some heroes before proceding your next dungeon";
                return null;
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
