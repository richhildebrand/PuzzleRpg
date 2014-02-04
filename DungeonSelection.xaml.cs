using System;
using System.Linq;
using Microsoft.Phone.Controls;
using PuzzleRpg.Database;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public partial class DungeonSelection : PhoneApplicationPage
    {
        private DungeonDatabase _dungeonDatabase;

        public DungeonSelection()
        {
            _dungeonDatabase = new DungeonDatabase();

            InitializeComponent();
            PopupUtils.UncoverScreen(); //just to be safe
            LoadUnlockedDungeons();
        }

        private void LoadUnlockedDungeons()
        {
            var unlockedDungeons = _dungeonDatabase.AllDungeons;
            //TODO: filter unlocked dungeons
            UnlockedDungeons.ItemsSource = unlockedDungeons;
        }
    }
}