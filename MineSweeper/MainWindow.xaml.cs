using System.Windows;
using MineSweeper.Model;

namespace MineSweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
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

            Board board = new Board(_boardSize);
            uGridBoard.Children.Add(board);
        }
    }
}