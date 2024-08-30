using System.Windows.Controls;

namespace MineSweeper.Model
{
    public class Tile : Button
    {
        public bool Revealed;
        public bool IsBomb { get; set; }
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
            IsEnabled = false;
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
