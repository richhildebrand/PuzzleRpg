using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PuzzleRpg.Utils;

namespace PuzzleRpg.Heroes
{
    public class Hero
    {
        public Image Element { get; set; }

        public Hero(string heroImagePath)
        {
            Element = new Image();
            Element = ImageUtils.GetImageFromPath(heroImagePath);
            Element.Stretch = System.Windows.Media.Stretch.Fill;
        }
    }
}
