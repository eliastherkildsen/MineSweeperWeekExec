using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Controls;
using System.Windows.Media;

namespace MineSweeper.Model
{
    public class Tile : Button, INotifyPropertyChanged
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
            // setting text in cell
            if (IsBomb)
            {
                Content = "\ud83d\udca3"; // ASCII bomb char. 
            } 
            
            else if (BombsAround >= 1)
            {
                Content = BombsAround;
            }

            
            Revealed = true;
            IsEnabled = false; 



        }

        public event PropertyChangedEventHandler? PropertyChanged;
        

        
    }
    
    
}
