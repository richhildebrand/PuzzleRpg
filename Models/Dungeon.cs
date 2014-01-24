using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class Dungeon
    {
        public List<DungeonFloor> Floors { get; set; }

        public Dungeon()
        {
            Floors = new List<DungeonFloor>();
        }
    }
}
