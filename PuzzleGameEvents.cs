using System;
using System.Linq;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg
{
    public class PuzzleGameEvents
    {
        private readonly PuzzleGrid _puzzleGrid;

        public PuzzleGameEvents(PuzzleGrid puzzleGrid)
        {
            this._puzzleGrid = puzzleGrid;
            MessageBus.Default.Register("EndTurn", OnEndTurn);
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
            //MonsterAttacks
            StartTurn();
        }
    }
}
