using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Database;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Models
{
    public class Team
    {
        public List<TeamMember> TeamMembers { get; set; }
        public int TotalHealth { get; set; }
        public int CurrentHealth { get; set; }

        private TeamMember GetRandomTeamMember(int slot) {
            var hero = HeroDatabase.GetHero(MathUtils.GetRandomInteger(0, HeroDatabase.HeroCount()));
            return new TeamMember(slot, hero);
        }
        
        public Team()
        {
            TeamMembers = new List<TeamMember>();

            TeamMembers.Add(GetRandomTeamMember(0));
            TeamMembers.Add(GetRandomTeamMember(2));
            TeamMembers.Add(GetRandomTeamMember(3));
            TeamMembers.Add(GetRandomTeamMember(4));

            CurrentHealth = GetTotalHealth();
            TotalHealth = GetTotalHealth();
        }

        public void Heal(List<OrbMatch> matches)
        {
            int healFor = TeamUtils.CalculateHealing(TeamMembers, matches);

            CurrentHealth += healFor;
            CurrentHealth = (CurrentHealth > TotalHealth) ? TotalHealth : CurrentHealth;
        }

        public int CalculateDamage(List<OrbMatch> matches)
        {
            return TeamUtils.CalculateDamage(TeamMembers, matches);
        }

        public void TakeDamage(int monsterAttackDamage)
        {
            CurrentHealth -= monsterAttackDamage;
        }

        private int GetTotalHealth()
        {
            var hitPoints = 0;
            foreach (var teamMember in TeamMembers)
            {
                hitPoints += teamMember.ThisHero.HitPoints;
            }
            return hitPoints;
        }
    }
}
