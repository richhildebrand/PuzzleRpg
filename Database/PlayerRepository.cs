﻿using System;
using System.IO.IsolatedStorage;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Database
{
    public class PlayerRepository : Repository
    {
        private readonly string PLAYER_KEY = "Stam";

        public PlayerRepository()
        {
            CreateKeyIfMissing(PLAYER_KEY);
        }

        protected override void CreateKey(string key)
        {
            var stam = new Stamina(max: 10);

            var exp = new Experience();
            exp.Max = 100;
            exp.Current = 0;

            var player = new Player();
            player.Exp = exp;
            player.Stam = stam;

            IsolatedStorageSettings.ApplicationSettings.Add(PLAYER_KEY, player);
        }

        public void SavePlayer(Player updatedPlayer) 
        {
            IsolatedStorageSettings.ApplicationSettings[PLAYER_KEY] = updatedPlayer;
            IsolatedStorageSettings.ApplicationSettings.Save();
        }

        public Player GetPlayer() 
        {
            return IsolatedStorageSettings.ApplicationSettings[PLAYER_KEY] as Player;
        }
    }
}
