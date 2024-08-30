using System.Windows;
using System.Windows.Controls.Primitives;
using MineSweeper.Util;

namespace MineSweeper.Model
{
    
    internal class Board : UniformGrid
    {
        private readonly int _size;

        public Board(int size, int noOfBombs = 5) {

            _size = size;
            // creating an array of tiles.

            // determining how many rows and columns to add to the grid.
            Columns = _size;
            Rows = _size;

            
        }
    }

}

