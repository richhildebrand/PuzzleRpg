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
        public int Unlocks { get; set; }
        public int StaminaCost { get; set; }
        public bool IsAvailable { get; set; }
        public bool HasBeenDefeated { get; set; }

        private Dungeon() { } //so it can be saved in local storage

        public Dungeon(int id, string displayName, int stam)
        {
            Id = id;
            Unlocks = id + 1;

            DisplayName = displayName;
            StaminaCost = stam;
            Floors = new List<DungeonFloor>();
        }
    }
}
