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
        
        /// <summary>
        /// Method for revaling tile, and its contets to the UI. 
        /// </summary>
        public void Reveal()
        {
            Revealed = true;
            IsEnabled = false;
            
            // setting text in cell
            if (IsBomb)
            {
                Content = "B";
                Background = Colors.BombColor; 
            } 
            
            else if (BombsAround > 0)
            {
                Content = BombsAround;
                Background = Colors.FlipedTileColor; 
            }

            else
            {
                Background = Colors.EmptyTileColor; 
            }
            
            
            
            
            
        }

    }
}
