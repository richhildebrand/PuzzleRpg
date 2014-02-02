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

        private HeroRepository _heroRepository;
        private TeamRepository _teamRepository;

        private TeamMember GetRandomTeamMember(int slot)
        {
            var hero = HeroDatabase.GetHero(MathUtils.GetRandomInteger(0, HeroDatabase.HeroCount()));
            return new TeamMember(slot, hero);
        }
        
        public Team()
        {
            _heroRepository = new HeroRepository();
            _teamRepository = new TeamRepository();

            TeamMembers = new List<TeamMember>();
            TeamMembers.Add(GetRandomTeamMember(0));
            TeamMembers.Add(GetRandomTeamMember(2));
            TeamMembers.Add(GetRandomTeamMember(3));
            TeamMembers.Add(GetRandomTeamMember(4));

            var heroes = TeamMembers.Select(h => h.ThisHero);
            CurrentHealth = heroes.Sum(h => h.HitPoints);
            TotalHealth = heroes.Sum(h => h.HitPoints);
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

        public void RemoveHeroFromTeam(string idToRemove)
        {
            if (idToRemove != null)
            {
                var id = new Guid(idToRemove);
                var memberToRemove = TeamMembers.Single(tm => tm.ThisHero.Id == id);
                TeamMembers.Remove(memberToRemove);
                _teamRepository.SaveTeam(TeamMembers);
            }
        }

        public void AddTeamMember(string idOfHeroToAdd)
        {
            var heroGuid = new Guid(idOfHeroToAdd);
            var heroToAdd = _heroRepository.GetHeroesOwnedByPlayer().Single(h => h.Id == heroGuid);

            var firstOpenSlot = GetFirstOpenSlot();
            var teamMemberToAdd = new TeamMember(firstOpenSlot, heroToAdd);
            TeamMembers.Add(teamMemberToAdd);
            _teamRepository.SaveTeam(TeamMembers);
        }
  
        private int GetFirstOpenSlot()
        {
            for (int i = 0; i < AppGlobals.MaxHeroesOnATeam; i++)
            {
                var heroInSlot = TeamMembers.SingleOrDefault(tm => tm.Slot == i);
                if (heroInSlot == null)
                {
                    return i;
                }
            }

            throw new Exception("No open slots");
        }
    }
}
