using System;
using System.Linq;
using System.Windows.Controls;
using PuzzleRpg.Models;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Modals
{
    public partial class DeleteHeroConfirmation : UserControl
    {
        private Hero _heroToDelete;

        public DeleteHeroConfirmation(Hero heroToDelete)
        {
            InitializeComponent();
            _heroToDelete = heroToDelete;

            HeroImage.Source = ImageUtils.GetImageSourceFromPath("/" + _heroToDelete.FullImagePath);
            HeroName.Text = _heroToDelete.Name;
            HeroLevel.Text = "Current Level " + "999";
        }
    }
}
