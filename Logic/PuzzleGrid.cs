﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Controls;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;
using SimpleMvvmToolkit;

namespace PuzzleRpg.Logic
{
    public class PuzzleGrid
    {
        private readonly int _rows;
        private readonly int _columns;
        private readonly Grid _grid;
        private List<PuzzlePiece> _puzzlePieces;

        public List<OrbMatch> MatchedOrbs { get; set; }

        public PuzzleGrid(Grid puzzleGrid, int rows, int columns)
        {
            _rows = rows;
            _columns = columns;
            _grid = puzzleGrid;
            _puzzlePieces = new List<PuzzlePiece>();
            MessageBus.Default.Register("SwapOrbs", SwapPuzzlePieces);

            _grid = GridUtils.AddRowsToGrid(puzzleGrid, rows);
            _grid = GridUtils.AddColumnsToGrid(puzzleGrid, columns);
            _grid = CreateCheckerBoardOnGrid(puzzleGrid, rows, columns);
            AppGlobals.PuzzleGridActualHeight = _grid.ActualHeight;
        }

        public void Unregister()
        {
            MessageBus.Default.Unregister("SwapOrbs", SwapPuzzlePieces);
        }

        public void SwapPuzzlePieces(object sender, NotificationEventArgs e)
        {
            var orbMove = sender as OrbMove;
            var movingPiece = _puzzlePieces.SingleOrDefault(pp => pp.Location.Row == orbMove.Origin.Row
                                                               && pp.Location.Column == orbMove.Origin.Column);

            var pieceToSwap = _puzzlePieces.SingleOrDefault(pp => pp.Location.Row == orbMove.Destination.Row
                                                               && pp.Location.Column == orbMove.Destination.Column);

            pieceToSwap.SetPosition(orbMove.Origin.Row, orbMove.Origin.Column);
            movingPiece.Location.Row = orbMove.Destination.Row;
            movingPiece.Location.Column = orbMove.Destination.Column;
        }

        public Task MatchAndReplacePuzzlePieces()
        {
            MatchedOrbs = new List<OrbMatch>();
            var taskSource = new TaskCompletionSource<bool>();
            MatchingAndReplacingPuzzlePieces(taskSource);
            return taskSource.Task;
        }

        private async void MatchingAndReplacingPuzzlePieces(TaskCompletionSource<bool> taskSource) {
            await OrbMatcher.MatchAllOrbs(_puzzlePieces, MatchedOrbs);
            MatchedOrbs = OrbMatcher.OrbMatches;
            _puzzlePieces = RemoveMatchedOrbs(_puzzlePieces, _grid);
            if (NeedToAddOrbs(_puzzlePieces))
            {
                _puzzlePieces = OrbDropper.DropExistingOrbs(_puzzlePieces);
                await AnimatedMoves.DropOrbs(_puzzlePieces);
                await AddOrbs();
                MatchingAndReplacingPuzzlePieces(taskSource);
            }
            else
            {
                taskSource.SetResult(true);
            }
        }

        private bool NeedToAddOrbs(List<PuzzlePiece> puzzlePieces) 
        {
            return puzzlePieces.Count < (AppGlobals.PuzzleGridRowCount * AppGlobals.PuzzleGridColumnCount);
        }

        private List<PuzzlePiece> RemoveMatchedOrbs(List<PuzzlePiece> puzzlePieces, Grid grid)
        {
            var matchedPieces = puzzlePieces.Where(pp => pp.Matched == true);

            foreach (var piece in matchedPieces)
            {
                grid.Children.Remove(piece.Element);
            }

            puzzlePieces = puzzlePieces.Except(matchedPieces).ToList();
            return puzzlePieces;
        }

        private Task AddOrbs()
        {
            for (int row = 0; row < _rows; ++row)
            {
                for (int column = 0; column < _columns; ++column)
                {
                    var orbAtLocation = _puzzlePieces.SingleOrDefault(pp => pp.Location.Row == row
                                                                         && pp.Location.Column == column);
                    if (orbAtLocation == null)
                    {
                        AddOrb(row, column, _puzzlePieces);
                    }
                }
            }
            return Task.WhenAll(AnimatedMoves.DropOrbs(_puzzlePieces));
        }

        private void AddOrb(int row, int column, List<PuzzlePiece> puzzlePieces)
        {
            var orb = new PuzzlePiece(row, column);
            puzzlePieces.Add(orb);
            _grid.Children.Add(orb.Element);
        }

        private Grid CreateCheckerBoardOnGrid(Grid grid, int rows, int columns)
        {
            for (int row = 0; row < rows; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    var image = GetImageForGridLocation(row, column);
                    grid.Children.Add(image);
                    image.SetValue(Grid.ColumnProperty, column);
                    image.SetValue(Grid.RowProperty, row);
                    image.Stretch = System.Windows.Media.Stretch.UniformToFill;
                }
            }
            return grid;
        }

        private Image GetImageForGridLocation(int row, int column)
        {
            return (row + column) % 2 == 0 ? ImageUtils.GetImageFromPath("Assests/Backgrounds/PrimaryTile.png")
                                           : ImageUtils.GetImageFromPath("Assets/Backgrounds/SecondaryTile.png");
        }
    }
}