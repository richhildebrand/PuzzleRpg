using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class Dungeon
    {
        public List<DungeonFloor> Floors { get; set; }
        public String DisplayName { get; set; }

        public Dungeon(string displayName)
        {
            DisplayName = displayName;
            Floors = new List<DungeonFloor>();
        }
    }
}
