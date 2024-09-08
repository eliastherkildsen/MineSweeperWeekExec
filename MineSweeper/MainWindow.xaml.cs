using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;
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

        // the board is a square, therefore the with and height are both the same. this is called board size.
        private System.Timers.Timer _timer; 
        private const int BoardSize = 10;

        private GameState _gameState; 
        public GameState GameState
        {
            set { _gameState = value; 
                UpdateGameStateImage(); }
            
            get { return _gameState; }
        }

        private int _flippedTiles; 
        private int _noBombs = 5;
        public int NoBombs
        {
            get { return _noBombs;  }
            set { _noBombs = value;
                OnPropertyChanged(); }
        }
        private Board _board; 
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
        private int _elapsedTime;
        public int ElapsedTime
        {
            get { return _elapsedTime; }
            set
            {
                _elapsedTime = value;
                OnPropertyChanged();
            }
        }

        
        public MainWindow()
        {
            InitializeComponent();
            StartGame();
            DataContext = this;
            
        }

        private void btnRestart_Click(object sender, RoutedEventArgs e)
        {
            GameLost();
            StartGame();
        }
        
        
        /// <summary>
        /// Method for setting up game stat and attributes. 
        /// </summary>
        private void StartGame()
        {
            // clears the grid if it is already initialized
            if (uGridBoard.Children.Count != 0) { 
                uGridBoard.Children.Clear();
            }
            
            
            NoClicks = 0;
            ElapsedTime = 0;
            _flippedTiles = 0; 
            GameState = GameState.GameInProgress;  
            
            // setting up game board and tiles. 
            _board = CreateGameBoard();
            _board.tiles = GenerateTiles(_board.tiles); 
            _board.tiles = BoardUtil.AssignBomb(_noBombs, _board.tiles);
            _board.tiles = BoardUtil.CalculateBombsAround(_board.tiles);
            uGridBoard.Children.Add(_board);
            
            CreateTimer();
            
        }
        
        /// <summary>
        /// Method called when game is lost
        /// </summary>
        private void GameLost()
        {
            GameState = GameState.GameLost; 
            _timer.Stop();
            _timer.Dispose();
        }
        
        /// <summary>
        /// Method called when game is won
        /// </summary>
        private void GameWon()
        {
            GameState = GameState.GameWon;
            _timer.Stop();
            _timer.Dispose();
        }

        /// <summary>
        /// Method for checking if the game is won.
        /// </summary>
        /// <returns></returns>
        private bool IsGameWon()
        {
            int totalTiles = _board.tiles.Length; 
            // checks if all tiles have been filped. '
            return ((_flippedTiles + _noBombs) == totalTiles);
        }

        /// <summary>
        /// Method for creating the game timer, and setting the attributes.
        /// </summary>
        private void CreateTimer()
        {
            int updateRate = 1000; // calculated in ms
            
            using (_timer = new System.Timers.Timer(updateRate)) {
                _timer = new System.Timers.Timer(updateRate);
                _timer.Elapsed += (sender, args) => ElapsedTime++;
                _timer.Stop(); // makes sure that the timer is not started on time of creation. 
            
            }
        }
        
        /// <summary>
        /// Method for creating a new game board
        /// </summary>
        /// <returns></returns>
        private Board CreateGameBoard()
        {
            return new Board(BoardSize);
        }
        
        /// <summary>
        /// Method for generating tiles for all indexes in the tiles array.
        /// </summary>
        private Tile[,] GenerateTiles(Tile[,] tiles)
        {
            Tile[,] newTiles = new Tile[tiles.GetLength(0), tiles.GetLength(1)];
            
            // looping through all rows.
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                // looping through all columns
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    // creating a new tile.
                    Tile tile = new Tile(i, j);
                    
                    tile.Click += TileOnClick; 
                    
                    // adding tile to the board.
                    _board.Children.Add(tile);
                    
                    // adding tile to the list of tiles.
                    newTiles[i, j] = tile;
                }
            }
            return newTiles; 
        }
        
        /// <summary>
        /// Event handler for button click 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TileOnClick(object sender, RoutedEventArgs e)
        {
            // validating that the sender is of type tile. 
            if (sender.GetType() != typeof(Tile)) return;
            
            // checks if game is over, in witch case you should not be able to click on a tile. 
            if (GameState == GameState.GameLost || GameState == GameState.GameWon) return; 
            
            Tile tile = (Tile)sender;
            _flippedTiles++; 
            tile.Reveal();
            NoClicks++;  
            
            
            // checks if the click is the first click, then starts the timer.  
            if (NoClicks == 1)
            {
                _timer.Start();
            }

            
            // logic if tile is a bomb
            if (tile.IsBomb)
            {
                GameLost();
                return; 
            }
            
            // logic if tile is not a bomb
            if (tile.BombsAround == 0) {
                RecursiveSearch(tile, e);
            }
            
            // check if game is won 
            if (IsGameWon())
            {
                GameWon();
            }
        }
        
        private void RecursiveSearch(Tile tile, RoutedEventArgs e)
        {
            foreach (var i in BoardUtil.GetBorderingTiles(tile, _board.tiles))
            {
                if (!i.Revealed && !i.IsBomb && tile.BombsAround == 0)
                {
                    i.Reveal();
                    _flippedTiles++; 
                    RecursiveSearch(i, e);
                }
            }
        }

        /// <summary>
        /// Method for updating the image reference for game state icon
        /// </summary>
        private void UpdateGameStateImage()
        {
            BitmapImage bitmap; 
            switch (GameState)
            { 
                
                case GameState.GameWon:
                    bitmap = new BitmapImage(new Uri("Resources/GameStateWonIcon.jpg", UriKind.Relative));
                    ImageGameState.Source = bitmap;
                    break;

                case GameState.GameLost:
                    bitmap = new BitmapImage(new Uri("Resources/GameStateLostIcon.jpg", UriKind.Relative));
                    ImageGameState.Source = bitmap;
                    break;

                case GameState.GameInProgress:
                    bitmap = new BitmapImage(new Uri("Resources/GameStateInProgressIcon.jpg", UriKind.Relative));
                    ImageGameState.Source = bitmap;
                    break;

                default:
                    bitmap = new BitmapImage(new Uri("Resources/GameStateIdleIcon.jpg", UriKind.Relative));
                    ImageGameState.Source = bitmap;
                    break;
            }

            
        }

        
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
        
        
    }
    
    
}