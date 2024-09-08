using System.DirectoryServices.ActiveDirectory;
using MineSweeper.Model;

namespace MineSweeper.Util;

public class BoardUtil
{
    /// <summary>
    /// Method for fetching the surrounding tiles of a given tile
    /// </summary>
    /// <param name="tile"></param>
    /// <param name="tiles"></param>
    /// <returns></returns>
    public static List<Tile> GetBorderingTiles(Tile tile, Tile[,] tiles)
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
                if (relativeI >= 0 && relativeI < tiles.GetLength(0) && relativeJ >= 0 && relativeJ < tiles.GetLength(1) && !(height == 0 && width == 0))
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
    public static Tile[,] AssignBomb(int noBombs, Tile[,] tilesList)
    {
        int actualBombs = 0;
        Random rng = new Random();

        while (actualBombs != noBombs)
        {

            int col = rng.Next(0, tilesList.GetLength(0));
            int row = rng.Next(0, tilesList.GetLength(1));
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
    /// Method for getting the number of bombs sounding a tile.
    /// </summary>
    /// <param name="tile"></param>
    public static Tile[,] CalculateBombsAround(Tile[,] tiles)
    {

        Tile[,] tileArray = tiles; 
        
        // looping through all tiles in the 2D array
        for (int i = 0; i < tileArray.GetLength(0); ++i) // tiles.GetLength(0) dimension 0  
        {
            for (int j = 0; j < tileArray.GetLength(1); ++j) // tiles.GetLength(0) dimension 1 
            {

                // Storing current working tile.
                Tile tile = tileArray[i, j];

                // we don't want to calculate if tile is a bomb
                if (tile.IsBomb) continue;
                
                int bombsAround = 0;

                // fetching all tiles surrounding the selected tile.
                foreach (Tile item in BoardUtil.GetBorderingTiles(tile, tileArray))
                {
                    if (item.IsBomb) bombsAround++;
                }

                tile.BombsAround = bombsAround;

            }
        }

        return tileArray; 
        
    }
    
    
    
    

}