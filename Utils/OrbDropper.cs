using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuzzleRpg.Utils
{
    public static class OrbDropper
    {
        private static bool _orbMoved;

        public static List<PuzzlePiece> DropExistingOrbs(List<PuzzlePiece> puzzlePieces) 
        {
            do
            {
                _orbMoved = false;
                for (int row = 0; row < AppGlobals.PuzzleGridRowCount; row++)
                {
                    var piecesInRow = puzzlePieces.Where(pp => pp.Location.Row == row).ToList();
                    for (int i = 0; i < piecesInRow.Count(); i++)
                    {
                        DropOneSpotIfEmptyBelow(piecesInRow[i], puzzlePieces);
                    }
                }
            } while (_orbMoved == true);
            return puzzlePieces;
        }

        private static void DropOneSpotIfEmptyBelow(PuzzlePiece pieceToDrop, List<PuzzlePiece> puzzlePieces)
        {
            var orbBelow = puzzlePieces.SingleOrDefault(pp => pp.Location.Column == pieceToDrop.Location.Column
                                                           && pp.Location.Row == pieceToDrop.Location.Row + 1);

            if (orbBelow == null && pieceToDrop.Location.Row != AppGlobals.PuzzleGridRowCount -1)
            {
                _orbMoved = true;
                pieceToDrop.Location.Row += 1;
            }
        }
    }
}
