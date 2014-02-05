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
        public int TeamId { get; set; }

        private readonly HeroRepository _heroRepository;

        public Team(TeamToSaveToDatabase teamFromDatabase)
        {
            _heroRepository = new HeroRepository();

            TeamId = teamFromDatabase.TeamId;
            TeamMembers = TeamMemberMapper.Map(teamFromDatabase.TeamMembers);

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

        public void RemoveHeroFromTeam(int slot)
        {
            var teamMemberToRemove = TeamMembers.SingleOrDefault(tm => tm.Slot == slot);
            TeamMembers.Remove(teamMemberToRemove);
        }

        public void AddTeamMember(int slotToAddHero, string idOfHeroToAdd)
        {
            var heroGuid = new Guid(idOfHeroToAdd);
            var heroToAdd = _heroRepository.GetHeroesOwnedByPlayer().Single(h => h.Id == heroGuid);

            var teamMemberToAdd = new TeamMember(slotToAddHero, heroToAdd);
            TeamMembers.Add(teamMemberToAdd);
        }
    }
}
