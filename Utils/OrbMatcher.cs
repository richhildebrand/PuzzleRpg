using System;
using System.Collections.Generic;
using System.Diagnostics;
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
                    Debug.WriteLine(matchingNeighbors[0].Type.ToString() + matchingNeighbors.Count.ToString());
                }
            }
            return puzzlePieces;
        }

        public static List<PuzzlePiece> MatchVerticalOrbs(List<PuzzlePiece> puzzlePieces)
        {
            for (int pieceIndex = 0; pieceIndex < puzzlePieces.Count; pieceIndex++)
            {
                var matchingNeighbors = GetMatchingColumnNeighbors(puzzlePieces[pieceIndex], puzzlePieces);
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

            PuzzlePiece nextPiece = piece;
            do {
                nextPiece = puzzlePieces.SingleOrDefault(pp => pp.Location.Column == nextPiece.Location.Column + 1
                                                                    && pp.Location.Row == nextPiece.Location.Row);
                nextPiece = AddPieceIfTypeMatches(piece, nextPiece, matchingPieces);
            } while (nextPiece != null);

            //var leftNeighbor = puzzlePieces.SingleOrDefault(pp => pp.Location.Column == piece.Location.Column - 1
            //                                                    && pp.Location.Row == piece.Location.Row);
            //AddPieceIfTypeMatches(piece, leftNeighbor, matchingPieces);

            return matchingPieces;
        }

        private static List<PuzzlePiece> GetMatchingColumnNeighbors(PuzzlePiece piece, List<PuzzlePiece> puzzlePieces)
        {
            var matchingPieces = new List<PuzzlePiece>();
            matchingPieces.Add(piece);

            var upperNeighbor = puzzlePieces.SingleOrDefault(pp => pp.Location.Row == piece.Location.Row + 1
                                                                && pp.Location.Column == piece.Location.Column);
            AddPieceIfTypeMatches(piece, upperNeighbor, matchingPieces);

            var lowerNeighbor = puzzlePieces.SingleOrDefault(pp => pp.Location.Row == piece.Location.Row - 1
                                                                && pp.Location.Column == piece.Location.Column);
            AddPieceIfTypeMatches(piece, lowerNeighbor, matchingPieces);

            return matchingPieces;
        }

        private static PuzzlePiece AddPieceIfTypeMatches(PuzzlePiece origionalPiece, PuzzlePiece possibleMatch, List<PuzzlePiece> puzzlePieces)
        {
            if (possibleMatch == null)
            {
                return null; 
            }

            if (origionalPiece.Type == possibleMatch.Type && !possibleMatch.Matched)
            {
                puzzlePieces.Add(possibleMatch);
                return possibleMatch;
            }

            return null;
        }
    }
}
