using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PuzzleRpg.Utils
{
    public static class ViewCalculations
    {
        private static double GetScreenWidth() {
            return Application.Current.Host.Content.ActualWidth;
        }

        public static Size GetHeroProfileSizeGiveNColumns(int numberOfColumns) {
            var screenWidth = GetScreenWidth();
            double heroProfileWidth = screenWidth / numberOfColumns;
            double heroProfileHeight = heroProfileWidth;

            return new Size( heroProfileWidth, heroProfileHeight);
        }
    }
}
