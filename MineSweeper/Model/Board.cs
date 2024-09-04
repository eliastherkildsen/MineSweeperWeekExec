using System.Windows.Controls.Primitives;

namespace MineSweeper.Model
{
    
    internal class Board : UniformGrid
    {

        private readonly int _size;
        private Tile[,] _tiles;
        public Tile[,] tiles
        {
            get { return _tiles; }
            set { _tiles = value; }
        }
        

        public Board(int size, int noBombs) {
                
            _size = size;
            _tiles = new Tile[size, size];
            
            // determining how many rows and columns to add to the grid.
            Columns = _size;
            Rows = _size;

            
        }
    }

}

