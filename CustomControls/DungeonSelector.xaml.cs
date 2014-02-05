using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
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
            MessageBus.Default.Notify("EnterDungeon", new Object(), new NotificationEventArgs(dungeonId.ToString()));

        }
    }
}
