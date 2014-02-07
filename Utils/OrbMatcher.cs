using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PuzzleRpg.Models;

namespace PuzzleRpg.Utils
{
    public static class OrbMatcher
    {
        public static List<OrbMatch> OrbMatches { get; set; }

        public static Task MatchAllOrbs(List<PuzzlePiece> puzzlePieces, List<OrbMatch>  orbMatches)
        {
            OrbMatches = orbMatches; //set by side effect
            Task matchHorizontal = MatchHorizontalOrbsAndTheirVerticalConnections(puzzlePieces);
            Task matchVertial = MatchUnconnectedVerticalOrbs(puzzlePieces);

            return Task.WhenAll(matchVertial, matchHorizontal);
        }
  
        private static async Task MatchUnconnectedVerticalOrbs(List<PuzzlePiece> puzzlePieces)
        {
            puzzlePieces = puzzlePieces.OrderBy(pp => pp.Location.Row).ToList();

            for (int pieceIndex = 0; pieceIndex < puzzlePieces.Count; pieceIndex++)
            {
                var matchingNeighbors = GetMatchingColumnNeighbors(puzzlePieces[pieceIndex], puzzlePieces);
                if (matchingNeighbors.Count >= 3)
                {
                    MarkAllOrbs(matchingNeighbors);
                    OrbMatches.Add(new OrbMatch(matchingNeighbors[0].Type, matchingNeighbors.Count, false));
                    await new AnimateMatch().FadeOrbs(matchingNeighbors);
                }
            }
        }

        private static async Task MatchHorizontalOrbsAndTheirVerticalConnections(List<PuzzlePiece> puzzlePieces)
        {
            puzzlePieces = puzzlePieces.OrderBy(pp => pp.Location.Column).ToList();
            for (int pieceIndex = 0; pieceIndex < puzzlePieces.Count; pieceIndex++)
            {
                var matchingNeighbors = GetMatchingRowNeighbors(puzzlePieces[pieceIndex], puzzlePieces);
                if (matchingNeighbors.Count >= 3)
                {
                    matchingNeighbors = CheckNorthAndSouth(matchingNeighbors, puzzlePieces);
                    matchingNeighbors = matchingNeighbors.Distinct().ToList();
                    MarkAllOrbs(matchingNeighbors);
                    OrbMatches.Add(new OrbMatch(matchingNeighbors[0].Type, matchingNeighbors.Count, true));
                    await new AnimateMatch().FadeOrbs(matchingNeighbors);
                }
            }
        }

        private static List<PuzzlePiece> CheckNorthAndSouth(List<PuzzlePiece> currentlyMatchingPieces, List<PuzzlePiece> allPuzzlePieces)
        {
            var allVerticallyMatchedPieces = new List<PuzzlePiece>();
            foreach (var puzzlePiece in currentlyMatchingPieces)
            {
                var northMostConnectedMatchingPiece = GetNorthMostConnectedMatchingPiece(puzzlePiece, allPuzzlePieces);
                var oneLineOfVerticallyMatchedPieces = new List<PuzzlePiece>();
                oneLineOfVerticallyMatchedPieces = WalkSouthMatchingOrbs(northMostConnectedMatchingPiece, oneLineOfVerticallyMatchedPieces, allPuzzlePieces);

                if (oneLineOfVerticallyMatchedPieces.Count >= 3)
                {
                    allVerticallyMatchedPieces = allVerticallyMatchedPieces.Concat(oneLineOfVerticallyMatchedPieces).ToList();
                }
            }

            currentlyMatchingPieces = currentlyMatchingPieces.Concat(allVerticallyMatchedPieces).ToList();
            return currentlyMatchingPieces;
        }
  
        private static List<PuzzlePiece> WalkSouthMatchingOrbs(PuzzlePiece northMostConnectedMatchingPiece, List<PuzzlePiece> matchingPieces, List<PuzzlePiece> allPuzzlePieces)
        {
            matchingPieces.Add(northMostConnectedMatchingPiece);

            PuzzlePiece nextPiece = northMostConnectedMatchingPiece;
            do
            {
                nextPiece = allPuzzlePieces.SingleOrDefault(pp => pp.Location.Column == nextPiece.Location.Column
                                                               && pp.Location.Row == nextPiece.Location.Row + 1
                                                               && pp.Type == nextPiece.Type);

                nextPiece = AddPieceIfTypeMatches(northMostConnectedMatchingPiece, nextPiece, matchingPieces);
            } while (nextPiece != null);

            return matchingPieces;
        }
  
        private static PuzzlePiece GetNorthMostConnectedMatchingPiece(PuzzlePiece puzzlePiece, List<PuzzlePiece> allPuzzlePieces)
        {
            bool foundNorthMostConnectedMatchingPiece;
            PuzzlePiece currentPiece = puzzlePiece;
            do
            {
                foundNorthMostConnectedMatchingPiece = true;
                var nextPiece = allPuzzlePieces.SingleOrDefault(pp => pp.Location.Column == currentPiece.Location.Column
                                                             && pp.Location.Row == currentPiece.Location.Row - 1
                                                             && pp.Type == currentPiece.Type);

                if (nextPiece != null)
                {
                    currentPiece = nextPiece;
                    foundNorthMostConnectedMatchingPiece = false;
                }
            } while (!foundNorthMostConnectedMatchingPiece);

            return currentPiece;
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

            return matchingPieces;
        }

        private static List<PuzzlePiece> GetMatchingColumnNeighbors(PuzzlePiece piece, List<PuzzlePiece> puzzlePieces)
        {
            var matchingPieces = new List<PuzzlePiece>();
            matchingPieces.Add(piece);

            PuzzlePiece nextPiece = piece;
            do
            {
                nextPiece = puzzlePieces.SingleOrDefault(pp => pp.Location.Column == nextPiece.Location.Column
                                                            && pp.Location.Row == nextPiece.Location.Row + 1);

                nextPiece = AddPieceIfTypeMatches(piece, nextPiece, matchingPieces);
            } while (nextPiece != null);

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
