using System;
using System.Linq;
using PuzzleRpg.Monsters;

namespace PuzzleRpg.Models
{
    public class DungeonFloor
    {
        public Monster Monsters;

        public DungeonFloor(Monster monster)
        {
            Monsters = monster;
        }
    }
}