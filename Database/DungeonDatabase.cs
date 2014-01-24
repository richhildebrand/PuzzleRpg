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

            AllDungeons.Add(GetGreenDragonsDen());
            AllDungeons.Add(GetBlueDragonsDen());
        }

        private Dungeon GetGreenDragonsDen()
        {
            var dungeon = new Dungeon();
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("WoodOrc")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("WoodOrc")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("Turtle")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("GreenDragon")));
            return dungeon;
        }

        private Dungeon GetBlueDragonsDen()
        {
            var dungeon = new Dungeon();
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("StoneOrc")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("StoneOrc")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("BigBlue")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("BlueDragon")));
            return dungeon;
        }
    }
}
