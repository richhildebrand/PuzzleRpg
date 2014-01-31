using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Phone.Controls;
using PuzzleRpg.CustomControls;
using PuzzleRpg.Database;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;

namespace PuzzleRpg
{
    public partial class TeamSelection : PhoneApplicationPage
    {
        private HeroRepository _heroRepository;
        private string _teamMemberToSwap;
        private Hero[] _activeTeam;

        public TeamSelection()
        {
            _heroRepository = new HeroRepository();
            _activeTeam = new Hero[AppGlobals.MaxHeroesOnATeam];

            InitializeComponent();
            ShowTeam();
            LoadAvailableHeroes();
        }

        public void OnAddHeroToTeam(object sender, GestureEventArgs e)
        {
            var heroToAdd = GetHeroId(sender);
            RemoveHeroFromTeam(_teamMemberToSwap);
            AddHeroToTeam(heroToAdd);

            AvailableHeroes.Visibility = Visibility.Collapsed;
            TeamStats.Visibility = Visibility.Visible;
            LoadAvailableHeroes();
        }

        public void OnBeginSelectingDifferentHero(object sender, GestureEventArgs e)
        {
            AvailableHeroes.Visibility = Visibility.Visible;
            TeamStats.Visibility = Visibility.Collapsed;
            _teamMemberToSwap = GetHeroId(sender);
        }

        private void AddHeroToTeam(string heroId)
        {
            var heroGuid = new Guid(heroId);
            for (int i = 0; i < _activeTeam.Length; i++)
            {
                var hero = _activeTeam[i];
                if (hero == null)
                {
                    _activeTeam[i] = _heroRepository.GetHeroesOwnedByPlayer().Single(h => h.Id == heroGuid);
                    ShowTeam();
                    break;
                }
            }
        }

        private void RemoveHeroFromTeam(string idToRemove) {
            if (idToRemove != null)
            {
                var id = new Guid(idToRemove);
                var heroToRemove = _activeTeam.Single(h => h != null && h.Id == id);
                var indexToRemove = Array.IndexOf(_activeTeam, heroToRemove);
                _activeTeam[indexToRemove] = null;
            }
        }

        private string GetHeroId(object sender) {
            var selectedProfile = sender as HeroProfileInHeroBox;
            var selectedHero = selectedProfile.HeroId;

            if (selectedHero.Tag == null)
            {
                return null;
            }
            var heroId = selectedHero.Tag;
            return heroId.ToString();
        }

        private void ShowTeam()
        {
            var activeHeroProfiles = HeroToViewModelMapper.GetHeroViewModels(_activeTeam);

            for (int i = 0; i < activeHeroProfiles.Length; i++)
            {
                var heroProfile = Team.Children[i] as HeroProfileInHeroBox;
                heroProfile.Draw(activeHeroProfiles[i]);
                heroProfile.Tap -= OnBeginSelectingDifferentHero;
                heroProfile.Tap += OnBeginSelectingDifferentHero;
            }
        }

        private void LoadAvailableHeroes()
        {
            var heroProfileSize = ViewCalculations.GetHeroProfileSizeGiveNColumns(AppGlobals.MaxHeroesOnATeam);
            AvailableHeroes.GridCellSize = heroProfileSize;
            AvailableHeroes.Visibility = Visibility.Collapsed;

            var availableHeroes = _heroRepository.GetHeroesOwnedByPlayer();
            var heroesInUse =_activeTeam.ToList();

            availableHeroes = availableHeroes.Except(heroesInUse).ToList();
            AvailableHeroes.ItemsSource = HeroToViewModelMapper.GetHeroViewModels(availableHeroes);
        }
    }
}