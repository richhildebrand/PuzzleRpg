using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using PuzzleRpg.Database;
using PuzzleRpg.Models;
using SimpleMvvmToolkit;

namespace PuzzleRpg.CustomControls
{
    public partial class DungeonSelector : UserControl
    {
        public DungeonSelector()
        {
            InitializeComponent();
        }

        public void OnDungeonSelected(object sender, GestureEventArgs e)
        {
            var selectedGrid = sender as Grid;
            var dungeonId = (int)selectedGrid.Tag;
            var dungeon = GetDungeon(dungeonId);

            var playerRepository = new PlayerRepository();
            var player = playerRepository.GetPlayer();

            if (player.Stam.Current >= dungeon.StaminaCost)
            {
                player.Stam.Current = player.Stam.Current - dungeon.StaminaCost;
                playerRepository.SavePlayer(player);
                MessageBus.Default.Notify("EnterDungeon", new Object(), new NotificationEventArgs(dungeonId.ToString()));
            }
            else
            {
                //TODO: Show correct error message
            }
        }

        private Dungeon GetDungeon(int dungeonId)
        {
            var dungeonDatabase = new DungeonDatabase();
            return dungeonDatabase.AllDungeons.Single(d => d.Id == dungeonId);
        }
    }
}
