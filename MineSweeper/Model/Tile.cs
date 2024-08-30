using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace MineSweeper.Model
{
    public class Tile : Button
    {

        public bool Revealed = false;
        public bool IsBomb { get; set; }
        public SolidColorBrush TileColor; 
        public int BombsAround { get; set; }
        public int X { get; }
        public int Y { get; }

        public Tile( int x = 0, int y = 0, bool isBomb = false)
        {
            IsBomb = isBomb;
            X = x;
            Y = y;
        }
        
        public void Reveal()
        {
            Revealed = true; 
            // Setting color
            Background = IsBomb ? Colors.BombColor : Colors.FlipedTileColor;
            
            // setting text in cell

            if (IsBomb)
            {
                Content = "B";
            } else if (BombsAround > 0)
            {
                Content = BombsAround;
            }
            
        }

        

    }
}
