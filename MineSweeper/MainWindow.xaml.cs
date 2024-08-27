using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MineSweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _gameBoardSize;
        private UniformGrid _grid;
        private List<List<Button>> _buttons = new List<List<Button>>();
        public MainWindow()
        {
            InitializeComponent();
            InitializeGameBoard(10);
        }


        public void InitializeGameBoard(int size) {

            Random rng = new Random();

            _gameBoardSize = size;
            
            // assigning grid to gameboard ref from xaml.
            _grid = gameBoard;
            _grid.Columns = _gameBoardSize; 
            _grid.Rows = _gameBoardSize;

            for (int i =  0; i < _gameBoardSize; i++)
            {
                _buttons.Add(new List<Button>());

                for (int j = 0; j < _gameBoardSize; j++)
                {
                    gameBoard.Children.Add(new Tile(rng.Next(0, 10) == 3));
                }

               

            }

        } 


       
    }
}