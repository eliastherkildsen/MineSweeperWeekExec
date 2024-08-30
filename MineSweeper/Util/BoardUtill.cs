using MineSweeper.Model;

namespace MineSweeper.Util;

public class BoardUtil
{
    /// <summary>
    /// Method for fetching the sourounding tiles of a given tile
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="tiles"></param>
    /// <returns></returns>
    public static List<Tile> GetBorderingTiles(Tile tile, Tile[,] tiles, int size)
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
                if (relativeI >= 0 && relativeI < size && relativeJ >= 0 && relativeJ < size && !(height == 0 && width == 0))
                {
                    borderingTiles.Add(tiles[relativeI, relativeJ]);
                }
            }
        }

        return borderingTiles; 

    }
    
    /// <summary>
    /// Method for assigning bomb status to a number og bombs.
    /// </summary>
    public static Tile[,] AssignBomb(int noBombs, Tile[,] tilesList, int size)
    {
        int actualBombs = 0;
        Random rng = new Random();

        while (actualBombs != noBombs)
        {

            int col = rng.Next(0, size);
            int row = rng.Next(0, size);
            Tile tile = tilesList[col, row];


            if (!tile.IsBomb)
            {
                tile.IsBomb = true;
                actualBombs++;
            }

        }

        return tilesList; 

    }
    
    /// <summary>
    /// Method for getting the number of bombs souranding a tile.
    /// </summary>
    /// <param name="tile"></param>
    public static void CalculateBombsAround(int _size, ref Tile[,] _tiles)
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
                    foreach (Tile item in BoardUtil.GetBorderingTiles(tile, _tiles, _size))
                    {
                        if (item.IsBomb) bombsAround++;
                    }

                    tile.BombsAround = bombsAround;
                       
                }
                    
            }
        }


    }
    
}