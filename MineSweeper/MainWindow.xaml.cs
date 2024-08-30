using System.Windows;
using MineSweeper.Model;

namespace MineSweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // the board is a squre, therefore the with and height are both the same. this is called boardsize.
        private int _boardSize = 10;
        
        public MainWindow()
        {
            InitializeComponent();
            StartGame();
            
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

            Board board = new Board(_boardSize, 10);
            uGridBoard.Children.Add(board);
        }
    }
}