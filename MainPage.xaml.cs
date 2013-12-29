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
            puzzleGrid = AddRowsToGrid(puzzleGrid, rows);
            puzzleGrid = AddColumnsToGrid(puzzleGrid, columns);
            puzzleGrid = CreateCheckerBoardOnGrid(puzzleGrid, rows, columns);
        }

        public Grid CreateCheckerBoardOnGrid(Grid grid, int rows, int columns) 
        {
            for (int row = 0; row < rows; ++row)
            {
                for (int column = 0; column < columns; ++column)
                {
                    var image = GetImageForGridLocation(row, column);
                    grid.Children.Add(image);
                    image.SetValue(Grid.ColumnProperty, column);
                    image.SetValue(Grid.RowProperty, row);

                    Grid.SetColumn(image, column);
                    Grid.SetRow(image, row);
                }
            }
            return grid;
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

        public Grid AddRowsToGrid(Grid grid, int rowsToAdd)
        {
            for (int i = 0; i < rowsToAdd; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
            }
            return grid;
        }

        public Grid AddColumnsToGrid(Grid grid, int columnsToAdd)
        {
            for (int i = 0; i < columnsToAdd; i++)
            {
                grid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            return grid;
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