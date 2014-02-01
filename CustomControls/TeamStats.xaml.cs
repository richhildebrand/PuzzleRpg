using System;
using System.Linq;
using System.Windows.Controls;
using System.Collections.Generic;
using PuzzleRpg.Models;
using SimpleMvvmToolkit;

namespace PuzzleRpg.CustomControls
{
    public partial class TeamStats : UserControl
    {
        public TeamStats()
        {
            InitializeComponent();
        }

        public void Draw(Hero[] team) {
            var filteredTeam = team.ToList().Where(h => h != null).ToList();

            TotalHealth.Text = "Total health is " + filteredTeam.Sum(h => h.HitPoints);
            HealsFor.Text = "Heals for " + filteredTeam.Sum(h => h.HealsFor);

            EarthAttacksFor.Text = "Attacks For " + " ???";
            WaterAttacksFor.Text = GetDamageMessage(filteredTeam, AppGlobals.Types.Water);
            WoodAttacksFor.Text = GetDamageMessage(filteredTeam, AppGlobals.Types.Wood);
            FireAttacksFor.Text = GetDamageMessage(filteredTeam, AppGlobals.Types.Fire);

        }

        private string GetDamageMessage(List<Hero> team, AppGlobals.Types type) 
        {
            return "Attacks for " + GetDamage(team, type);
        }

        private int GetDamage(List<Hero> team, AppGlobals.Types type) 
        {
            var heroesOfType = team.Where(h => h.Type == type);
            return heroesOfType.Sum(h => h.AttackDamage);
        }
    }
}
