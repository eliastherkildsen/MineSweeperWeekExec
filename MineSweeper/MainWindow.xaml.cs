using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using MineSweeper.Model;
using MineSweeper.Util;

namespace MineSweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        // the board is a squre, therefore the with and height are both the same. this is called boardsize.
        private int _boardSize = 10;
        private bool _gameOver = false;
        private int _noClicks = 0;

        public int NoClicks
        {
            get { return _noClicks; }
            set
            {
                _noClicks = value;
                OnPropertyChanged();
            }
        }
        private int _NoBombs = 5; 
        private Tile[,] _tiles;
        private Board board; 
        
        public MainWindow()
        {
            InitializeComponent();
            StartGame();
            this.DataContext = this; 
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            StartGame();
        }

        private void StartGame()
        {
            // clears the grid if it is allready initialized
            if (uGridBoard.Children.Count != 0) { 
                uGridBoard.Children.Clear();
            }

            CreateGameBoard();
            PopulateBoardWithTiles();
            _tiles = BoardUtil.AssignBomb(_NoBombs, _tiles, _boardSize);
            BoardUtil.CalculateBombsAround(_boardSize, ref _tiles);
            uGridBoard.Children.Add(board);
        }

        private void CreateGameBoard()
        {
            board = new Board(_boardSize, _NoBombs);
            _tiles = new Tile[_boardSize, _boardSize];
            
        }
        
        /// <summary>
        /// Method for generating tiles for all indexes in the tiles array.
        /// </summary>
        private void PopulateBoardWithTiles()
        {
            // looping thrugh all rows.
            for (int i = 0; i < _boardSize; i++)
            {
                // looping thrugh all colums
                for (int j = 0; j < _boardSize; j++)
                {
                    // creating a new tile.
                    Tile tile = new Tile(i, j);
                    
                    tile.Click += TileOnClick; 
                    
                    // adding tile to the board.
                    board.Children.Add(tile);
                    
                    // adding tile to the list of tiles.
                    _tiles[i, j] = tile;
                }

            }
            // assigning isBomb to some of the bombs
            _tiles = BoardUtil.AssignBomb(_NoBombs, _tiles, _boardSize);
        }
        
           

        private void TileOnClick(object sender, RoutedEventArgs e)
        {
            
            Tile tile = sender as Tile;
            tile.Reveal();
            _noClicks++; 
            
            if (tile.IsBomb)
            {
                _gameOver = true; 
            }

            if (tile.BombsAround == 0 && !tile.IsBomb) {
                RecursiveSearch(tile, e);
            }

            


        }

        private void RecursiveSearch(Tile tile, RoutedEventArgs e)
        {
            foreach (var i in BoardUtil.GetBorderingTiles(tile, _tiles, _boardSize))
            {
                if (!i.Revealed && !i.IsBomb && tile.BombsAround == 0)
                {
                    i.Reveal();
                    RecursiveSearch(i, e);
                }
            }
        }


        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
    
    
}