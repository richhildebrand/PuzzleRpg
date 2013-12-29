using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.Phone.Controls;

namespace PuzzleRpg
{
    public partial class MainPage : PhoneApplicationPage
    {

        public MainPage()
        {
            InitializeComponent();
            InitPuzzleGrid(ContentPanel, 5, 6);
        }

        private void InitPuzzleGrid(Grid puzzleGrid, int rows, int columns)
        {
            AddRowsToGrid(puzzleGrid, rows);
            AddColumnsToGrid(puzzleGrid, columns);

            Brush defaultBrush = new SolidColorBrush(Colors.Yellow);
            Brush alternateBrush = new SolidColorBrush(Colors.Black);

            for (int row = 0; row < rows; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    var image = GetImageForGridLocation(row, column);
                    puzzleGrid.Children.Add(image);
                    image.SetValue(Grid.ColumnProperty, column);
                    image.SetValue(Grid.RowProperty, row);

                    Grid.SetColumn(image, column);
                    Grid.SetRow(image, row);
                    //Grid.SetColumn(
                    //cell.Background = (y + x) % 2 == 0 ? defaultBrush : alternateBrush;
                    //puzzleGrid.Children.Add(cell);
                }
            }

            puzzleGrid.ShowGridLines = true;
        }

        public Image GetImageForGridLocation(int row, int column) 
        {
            return (row + column) % 2 == 0 ? GetImageFromPath("Assests/Backgrounds/PrimaryTile.png")
                                           : GetImageFromPath("Assets/Backgrounds/SecondaryTile.png");
        }

        public Image GetImageFromPath(String path) {
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.UriSource = new Uri(path, UriKind.Relative);

            Image image = new Image();
            image.Source = bitmapImage;
            return image;
        }

        public void AddRowsToGrid(Grid grid, int rowsToAdd)
        {
            for (int i = 0; i < rowsToAdd; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
        }

        public void AddColumnsToGrid(Grid grid, int columnsToAdd)
        {
            for (int i = 0; i < columnsToAdd; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
        }

        private void MouseMoving(object sender, MouseEventArgs e)
        {
            var image = (System.Windows.Controls.Image)sender;
            Canvas.SetZIndex(image, 1);
            var x = 7;
        }
        
        public void OnDragEnd(object sender, ManipulationCompletedEventArgs e)
        {
            var image = (System.Windows.Controls.Image)sender;
            Canvas.SetZIndex(image, 0);
            var x = 8;
        }
    }
}