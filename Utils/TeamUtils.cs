using System;
using System.Collections.Generic;
using System.Linq;
using PuzzleRpg.Models;

namespace PuzzleRpg.Utils
{
    public static class TeamUtils
    {
        private static readonly double MATCH_MULTIPLIER = 0.25;
        private static readonly double ORB_OVER_THREE_MULTIPLIER = 0.25;

        public static int CalculateHealing(List<TeamMember> teamMembers, List<OrbMatch> matches)
        {
            var healMatches = matches.Where(m => m.Type == AppGlobals.Types.Heal).ToList();
            var totalHealing = 0;

            foreach (var teamMember in teamMembers)
            {
                double heroHealing = 0;
                foreach (var match in healMatches)
                {
                    heroHealing = CalculateMatchDamage(teamMember.ThisHero.HealsFor, match);
                }

                var extraMatches = matches.Count - healMatches.Count;
                heroHealing *= (MATCH_MULTIPLIER * extraMatches) + 1;
                totalHealing += Convert.ToInt32(heroHealing);
            }

            return totalHealing;
        }

        public static int CalculateDamage(List<TeamMember> teamMembers, List<OrbMatch> matches)
        {
            var totalDamage = 0;

            if (matches == null)
            {
                return totalDamage;
            }

            foreach (var teamMember in teamMembers)
            {
                totalDamage += CalculateHeroDamage(teamMember.ThisHero, matches);
            }

            return totalDamage;
        }
  
        private static int CalculateHeroDamage(Hero hero, List<OrbMatch> matches)
        {
            double heroDamage = 0;
            
            var typeMatches = matches.Where(m => m.Type == hero.Type).ToList();
            foreach (var match in typeMatches)
            {
                heroDamage += CalculateMatchDamage(hero.AttackDamage, match);
            }

            var extraMatches = matches.Count - typeMatches.Count;
            heroDamage *= (MATCH_MULTIPLIER * extraMatches) + 1;

            return Convert.ToInt32(heroDamage);
        }
  
        private static double CalculateMatchDamage(int attackDamage, OrbMatch match)
        {
            var extraOrbs = match.Count - 3;
            double heroDamage = attackDamage;
            heroDamage *= (ORB_OVER_THREE_MULTIPLIER * extraOrbs) + 1;
            return heroDamage;
        }
    }
}
