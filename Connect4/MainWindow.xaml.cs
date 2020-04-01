using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Connect4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            BitBoard bitBoard = new BitBoard();
            bitBoard.Play("2252576253462244111563365343671351441".ToCharArray());
            string displayBitBoard = bitBoard.ToString();

            MatrixBoard matrixBoard = new MatrixBoard();
            matrixBoard.Play("2252576253462244111563365343671351441".ToCharArray());
            string displayMatrixBoard = matrixBoard.ToString();
        }
    }
}
