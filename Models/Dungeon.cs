using System;
using System.Collections.Generic;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class Dungeon
    {
        public List<DungeonFloor> Floors { get; set; }
        public String DisplayName { get; set; }
        public int Id { get; set; }
        public int StaminaCost { get; set; }

        public Dungeon(int id, string displayName, int stam)
        {
            Id = id;
            DisplayName = displayName;
            StaminaCost = stam;
            Floors = new List<DungeonFloor>();
        }
    }
}
