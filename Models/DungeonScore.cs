using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class DungeonScore
    {
        public bool DungeonInProgress { get; set; }
        public List<Monster> MonstersSlain { get; set; }
        public Dungeon ActiveDungeon { get; set; }
        public bool PlayerWins { get; set; }

        public DungeonScore(Dungeon activeDungeon)
        {
            ActiveDungeon = activeDungeon;
            MonstersSlain = new List<Monster>();
        }
    }
}
