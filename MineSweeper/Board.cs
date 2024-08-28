using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace MineSweeper
{
    internal class Board : UniformGrid
    {
        private readonly bool IsDebug = true;
        private readonly int Size;
        private int NoOfBombs;
        private Tile[,] tiles;

        public Board(int size, int noOfBombs = 5) {

            Size = size;
            NoOfBombs = noOfBombs;
            // creating an array of tiles.
            tiles = new Tile[size, size];

            // determening how maney rows and colums to add to the grid.
            Columns = Size;
            Rows = Size;

            GenerateTiles();
            CalculateBombsAround();

        }


        /// <summary>
        /// Method for generating tiles for all indexes in the tiles array.
        /// </summary>
        private void GenerateTiles()
        {
            // looping thrugh all rows.
            for (int i = 0; i < Size; i++)
            {
                // looping thrugh all colums
                for (int j = 0; j < Size; j++)
                {
                    // creating a new tile.
                    Tile tile = new Tile(i, j);

                    // adding tile to the board.
                    this.Children.Add(tile);
                    
                    // adding tile to the list of tiles.
                    tiles[i, j] = tile;
                }

            }
            // assigning isBomb to some of the bombs
            AssignBomb(NoOfBombs);
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

                int col = rng.Next(0, Size);
                int row = rng.Next(0, Size);
                Tile tile = tiles[col, row];


                if (!tile.IsBomb)
                {
                    tile.IsBomb = true;
                    actualBombs++;
                    tile.Content = "B";

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
            for (int i = 0; i < Size; ++i)
            {
                for (int j = 0; j < Size; ++j)
                {

                    // Storing current working tile.
                    Tile tile = tiles[i, j];

                    // we dont want to calculate if tile is a bomb
                    if (!tile.IsBomb)
                    {
                        int bombsAround = 0;

                        // getching all tiles sourounding the selected tile.
                        foreach (Tile item in GetBorderingTiles(tile, tiles))
                        {
                            if (item.IsBomb) bombsAround++;
                        }

                        tile.BombsAround = bombsAround;
                        if (IsDebug && bombsAround > 0)
                        {
                            tile.Content = bombsAround.ToString();
                        }
                    }
                    else if (IsDebug)
                    {
                        tile.Background = Brushes.Red;
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
                    int relativeI = tile.x + height;
                    int relativeJ = tile.y + width;

                    // Ensure we are not out of bounds or checking the current tile
                    if (relativeI >= 0 && relativeI < Size && relativeJ >= 0 && relativeJ < Size && !(height == 0 && width == 0))
                    {
                        borderingTiles.Add(tiles[relativeI, relativeJ]);
                    }
                }
            }

            return borderingTiles; 

        }



    }

}

