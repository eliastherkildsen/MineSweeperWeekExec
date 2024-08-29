using System.Windows.Controls;
using System.Windows.Media;

namespace MineSweeper.Model
{
    class Tile : Button
    {
        private bool IsClicked = true;
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
            
            this.TileColor = isBomb ? Colors.BombColor : Colors.FlipedTileColor;
            Background = Colors.TileColor;
        }

   

        
        
        
        
        

    }
}
