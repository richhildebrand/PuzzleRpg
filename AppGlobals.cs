using System;
using System.Linq;
using System.Windows.Media.Animation;
using PuzzleRpg.Models;

namespace PuzzleRpg
{
    public static class AppGlobals
    {
        public static readonly int PuzzleGridRowCount = 5;
        public static readonly int PuzzleGridColumnCount = 6;
        public static double PuzzleGridActualHeight { get; set; }
        public static Storyboard PuzzleStoryBoard = new Storyboard();
        public static readonly int MaxHeroesOnATeam = 5;
        public static DungeonScore ActiveDungeonScore = new DungeonScore();

        public static readonly string HeroImagePathPrefix = "Assets/Monsters/";

        public enum Types 
        {
            Fire,
            Water,
            Wood,
            Earth,
            Heal
        }
    }
}
