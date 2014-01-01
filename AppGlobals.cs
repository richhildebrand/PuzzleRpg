﻿using System;
using System.Linq;
using System.Windows.Media.Animation;

namespace PuzzleRpg
{
    public static class AppGlobals
    {
        public static readonly int PuzzleGridRowCount = 5;
        public static readonly int PuzzleGridColumnCount = 6;
        public static double PuzzleGridActualHeight { get; set; }
        public static Storyboard PuzzleStoryBoard = new Storyboard();
    }
}