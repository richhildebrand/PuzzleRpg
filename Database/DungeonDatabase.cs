using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Database
{
    public class DungeonDatabase
    {
        public List<Dungeon> AllDungeons { get; set; }

        public DungeonDatabase()
        {
            AllDungeons = new List<Dungeon>();

            AllDungeons.Add(GetGreenDragonsDen(1));
            AllDungeons.Add(GetBlueDragonsDen(2));
        }

        private Dungeon GetGreenDragonsDen(int id)
        {
            var dungeon = new Dungeon(id, "Green Dragons Den");
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("WoodOrc")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("WoodOrc")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("Turtle")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("GreenDragon")));
            return dungeon;
        }

        private Dungeon GetBlueDragonsDen(int id)
        {
            var dungeon = new Dungeon(id, "Blue Dragons Den");
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("StoneOrc")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("StoneOrc")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("BigBlue")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("BlueDragon")));
            return dungeon;
        }
    }
}
