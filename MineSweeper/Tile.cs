using System.Windows.Controls;

namespace MineSweeper
{

    public delegate void ButtonClicked(int i);

    class Tile : Button
    {

        private bool IsClicked = true;
        public bool IsBomb { get; set; }
        public int BombsAround { get; set; }
        public int x { get; }
        public int y { get; }

        public Tile( int x = 0, int y = 0, bool isBomb = false)
        {
            this.IsBomb = isBomb;
            this.x = x;
            this.y = y;
        }

    }
}
