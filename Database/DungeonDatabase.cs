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

            AllDungeons.Add(GetRexsHouse(1));
            AllDungeons.Add(GetOrcVilla(2));
            AllDungeons.Add(GetTurtleShore(3));
            AllDungeons.Add(GetGreenDragonsDen(4));
            AllDungeons.Add(GetBlueDragonsDen(5));
            AllDungeons.Add(GetDragonsDen(6));
        }

        private Dungeon GetTurtleShore(int id)
        {
            var dungeon = new Dungeon(id, "Turtle Shore");
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("Turtle")));
            return dungeon;
        }

        private Dungeon GetRexsHouse(int id)
        {
            var dungeon = new Dungeon(id, "Rex's House");
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("Rex"))); 
            return dungeon;
        }

        private Dungeon GetDragonsDen(int id)
        {
            var dungeon = new Dungeon(id, "Dragons Den");
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("GreenDragon")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("BlueDragon")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("RedDragon")));
            return dungeon;
        }

        private Dungeon GetOrcVilla(int id)
        {
            var dungeon = new Dungeon(id, "Orc Villa");
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("WoodOrc")));
            dungeon.Floors.Add(new DungeonFloor(MonsterDatabase.GetMonster("StoneOrc")));
            return dungeon;
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
