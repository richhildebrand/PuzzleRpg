using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleRpg.Utils
{
    public static class OrbMatcher
    {
        public static List<PuzzlePiece> MatchHorizontalOrbrs(List<PuzzlePiece> puzzlePieces)
        {
            for (int pieceIndex = 0; pieceIndex < puzzlePieces.Count; pieceIndex++)
            {
                var matchingNeighbors = GetMatchingRowNeighbors(puzzlePieces[pieceIndex], puzzlePieces);
                if (matchingNeighbors.Count >= 3)
                {
                    MarkAllOrbs(matchingNeighbors);
                }
            }
            return puzzlePieces;
        }

        private static void MarkAllOrbs(List<PuzzlePiece> puzzlePiecesToMark)
        {
            foreach (var piece in puzzlePiecesToMark)
            {
                piece.Matched = true;
            }
        }

        private static List<PuzzlePiece> GetMatchingRowNeighbors(PuzzlePiece piece, List<PuzzlePiece> puzzlePieces)
        {
            var matchingPieces = new List<PuzzlePiece>();
            matchingPieces.Add(piece);

            var rightNeighbor = puzzlePieces.SingleOrDefault(pp => pp.Location.Column == piece.Location.Column + 1
                                                                && pp.Location.Row == piece.Location.Row);
            AddPieceIfTypeMatches(piece, rightNeighbor, matchingPieces);


            var leftNeightbor = puzzlePieces.SingleOrDefault(pp => pp.Location.Column == piece.Location.Column - 1
                                                                && pp.Location.Row == piece.Location.Row);
            AddPieceIfTypeMatches(piece, leftNeightbor, matchingPieces);

            return matchingPieces;
        }

        private static void AddPieceIfTypeMatches(PuzzlePiece origionalPiece, PuzzlePiece possibleMatch, List<PuzzlePiece> puzzlePieces)
        {
            if (possibleMatch == null)
            {
                return; 
            }

            if (origionalPiece.Type == possibleMatch.Type)
            {
                puzzlePieces.Add(possibleMatch);
            }
        }
    }
}
