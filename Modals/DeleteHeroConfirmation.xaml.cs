﻿using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using PuzzleRpg.Database;
using PuzzleRpg.Interface;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Modals
{
    public partial class DeleteHeroConfirmation : UserControl, IModalControl
    {
        private Hero _heroToDelete;

        public event EventHandler CloseModal;

        public DeleteHeroConfirmation(Hero heroToDelete)
        {
            InitializeComponent();
            _heroToDelete = heroToDelete;

            HeroImage.Source = ImageUtils.GetImageSourceFromPath("/" + _heroToDelete.FullImagePath);
            HeroName.Text = _heroToDelete.Name;
            HeroLevel.Text = "Current Level " + heroToDelete.Level;
        }

        public void Cancel(object sender, GestureEventArgs e)
        {
            CloseModal(this, new EventArgs());
        }

        public void DeleteHero(object sender, GestureEventArgs e)
        {
            var heroRepository = new HeroRepository();
            heroRepository.RemoveHeroFromPlayerCollection(_heroToDelete);
            CloseModal(this, new EventArgs());
        }
    }
}
