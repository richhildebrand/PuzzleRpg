using System;
using System.Linq;

namespace PuzzleRpg.Models
{
    public class DungeonFloor
    {
        public Monster Monsters;

        private DungeonFloor() { } //save in local storage

        public DungeonFloor(Monster monster)
        {
            Monsters = monster;
        }
    }
}