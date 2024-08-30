using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MineSweeper.Model
{
    
    internal class Board : UniformGrid
    {
        
        private readonly int _size;
        private readonly int _noOfBombs;
        private readonly Tile[,] _tiles;

        public Board(int size, int noOfBombs = 5) {

            _size = size;
            _noOfBombs = noOfBombs;
            // creating an array of tiles.
            _tiles = new Tile[size, size];

            // determining how many rows and columns to add to the grid.
            Columns = _size;
            Rows = _size;

            GenerateTiles();
            CalculateBombsAround();
            
        }


        /// <summary>
        /// Method for generating tiles for all indexes in the tiles array.
        /// </summary>
        private void GenerateTiles()
        {
            // looping thrugh all rows.
            for (int i = 0; i < _size; i++)
            {
                // looping thrugh all colums
                for (int j = 0; j < _size; j++)
                {
                    // creating a new tile.
                    Tile tile = new Tile(i, j);
                    
                    tile.Click += TileOnClick; 
                    
                    // adding tile to the board.
                    Children.Add(tile);
                    
                    // adding tile to the list of tiles.
                    _tiles[i, j] = tile;
                }

            }
            // assigning isBomb to some of the bombs
            AssignBomb(_noOfBombs);
        }

        private void TileOnClick(object sender, RoutedEventArgs e)
        {
            
            Tile tile = sender as Tile;
            tile.Reveal();

            if (tile.BombsAround == 0 ) {
            
                foreach (var i in GetBorderingTiles(tile, _tiles))
                {
                    if (!i.Revealed && !i.IsBomb)
                    {
                        i.Reveal();
                        TileOnClick(i, e);
                    }
                }
            }
            

        }

        /// <summary>
        /// Method for assigning bomb status to a number og bombs.
        /// </summary>
        private void AssignBomb(int noBombs)
        {
            int actualBombs = 0;
            Random rng = new Random();

            while (actualBombs != noBombs)
            {

                int col = rng.Next(0, _size);
                int row = rng.Next(0, _size);
                Tile tile = _tiles[col, row];


                if (!tile.IsBomb)
                {
                    tile.IsBomb = true;
                    actualBombs++;
                    

                }

            }

        }

        /// <summary>
        /// Method for getting the number of bombs souranding a tile.
        /// </summary>
        /// <param name="tile"></param>
        private void CalculateBombsAround()
        {
            // looping thrugh all tiles in the 2D array
            for (int i = 0; i < _size; ++i)
            {
                for (int j = 0; j < _size; ++j)
                {

                    // Storing current working tile.
                    Tile tile = _tiles[i, j];

                    // we dont want to calculate if tile is a bomb
                    if (!tile.IsBomb)
                    {
                        int bombsAround = 0;

                        // getching all tiles sourounding the selected tile.
                        foreach (Tile item in GetBorderingTiles(tile, _tiles))
                        {
                            if (item.IsBomb) bombsAround++;
                        }

                        tile.BombsAround = bombsAround;
                       
                    }
                    
                }
            }


        }

        /// <summary>
        /// Method for fetching the sourounding tiles of a given tile
        /// </summary>
        /// <param name="tile"></param>
        /// <param name="tiles"></param>
        /// <returns></returns>
        private List<Tile> GetBorderingTiles(Tile tile, Tile[,] tiles)
        {
            List<Tile> borderingTiles = new List<Tile>();

            // Check all eight directions around the current tile
            for (int height = -1; height <= 1; height++)
            {
                for (int width = -1; width <= 1; width++)
                {
                    int relativeI = tile.X + height;
                    int relativeJ = tile.Y + width;

                    // Ensure we are not out of bounds or checking the current tile
                    if (relativeI >= 0 && relativeI < _size && relativeJ >= 0 && relativeJ < _size && !(height == 0 && width == 0))
                    {
                        borderingTiles.Add(tiles[relativeI, relativeJ]);
                    }
                }
            }

            return borderingTiles; 

        }
        
        

        




    }

}

