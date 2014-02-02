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

        public void Draw(Team team) {
            var heroes = team.TeamMembers.Select(tm => tm.ThisHero).ToList();

            TotalHealth.Text = "Total health is " + heroes.Sum(h => h.HitPoints);
            HealsFor.Text = "Heals for " + heroes.Sum(h => h.HealsFor);

            EarthAttacksFor.Text = "Attacks for " + " ???";
            WaterAttacksFor.Text = GetDamageMessage(heroes, AppGlobals.Types.Water);
            WoodAttacksFor.Text = GetDamageMessage(heroes, AppGlobals.Types.Wood);
            FireAttacksFor.Text = GetDamageMessage(heroes, AppGlobals.Types.Fire);

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
