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

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        private int count=1;
        private List<Button> buttons = new List<Button>();//массив нажатых кнопок
        public MainWindow()
        {
            InitializeComponent();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((string)(sender as Button).Content=="")
            {
                if (++count % 2 == 0 ) { Write_0_X(sender, "X"); }
                else { Write_0_X(sender, "0"); }
            }                                 
        }

        private void Write_0_X(object sender, string symbol)
        {
            (sender as Button).Content = symbol;            
            buttons.Add(sender as Button);

            if (symbol == "X")
            {
                (sender as Button).Foreground = Brushes.Red;
            }
            if (symbol == "0")
            {
                (sender as Button).Foreground = Brushes.Green;
            }

            string [,] aray_0_X = {
                { (string)Coord_0_0.Content, (string)Coord_0_1.Content, (string)Coord_0_2.Content },
                { (string)Coord_1_0.Content, (string)Coord_1_1.Content, (string)Coord_1_2.Content },
                { (string)Coord_2_0.Content, (string)Coord_2_1.Content, (string)Coord_2_2.Content }
                               };
            Find_win(aray_0_X);
        }

        private void Find_win(string[,] array_0_X)
        {
            if (count <= 5)
            {
                return;
            }
            Check_row_column_diag(array_0_X,"0");
            Check_row_column_diag(array_0_X, "X");
            if (count - 1 == array_0_X.Length)
            {
                MessageBox.Show("Ничья");
                Cleare_Win(array_0_X);
                return;
            }
        }

        private void Check_row_column_diag(string[,] array_0_X, string symbol)
        {
            for (int j = 0; j < array_0_X.GetLength(0); j++)
            {
                int number_overlap_gorizontal = 0;
                int number_overlap_vertical = 0;
                int number_overlap_diag_main = 0;
                int number_overlap_diag_not_main = 0;
                for (int i = 0; i < array_0_X.GetLength(0); i++)
                {
                    if (array_0_X[i, j] == symbol) { number_overlap_vertical++; }
                    if (array_0_X[j, i] == symbol) { number_overlap_gorizontal++; }
                    if (array_0_X[i, i] == symbol) { number_overlap_diag_main++; }
                    if (array_0_X[i, array_0_X.GetLength(0) - i-1] == symbol) { number_overlap_diag_not_main++; }
                                       

                    if (number_overlap_gorizontal == array_0_X.GetLength(0)
                        || number_overlap_vertical == array_0_X.GetLength(0)
                        || number_overlap_diag_main == array_0_X.GetLength(0)
                        || number_overlap_diag_not_main== array_0_X.GetLength(0))
                    {

                        if (symbol == "0")
                        {
                            MessageBox.Show($"{Player1.Text},победил(а)");
                        }
                        else
                        {
                            MessageBox.Show($"{Player2.Text},победил(а)");                            
                        }
                        Cleare_Win(array_0_X);
                        return;
                    }                    
                }
            }
        }

        private void Cleare_Win(string[,] array_0_X)
        {
            count = 1;
            for (int i = 0; i < array_0_X.GetLength(0); i++)
            {
                for (int j = 0; j < array_0_X.GetLength(0); j++)
                {
                    array_0_X[i, j] = "";
                }                
            }
            foreach (Button button in buttons)
            {
                button.Content = "";
            }
        }
    }
}
