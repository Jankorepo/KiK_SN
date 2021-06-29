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
using Newtonsoft.Json;
using System.IO;

namespace KiK_SN
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        Web neural_web = new Web();
        Web neural_web_enemy = new Web();
        TicTacToe board_to_play = new TicTacToe();
        int web_character = 2;
        public MainWindow()
        {
            InitializeComponent();
            neural_web.web_structure = new List<int>() { 27,9,9 };
            neural_web.Fill(neural_web);
        }

        private void Button01_Click(object sender, RoutedEventArgs e)
        {
            int epochs = 100000;
            List<Double> tmp = new List<double>() { 0, 0, 0 };
            for (int i = 0; i < epochs; i++)
            {
                Tuple<List<List<Double>>, List<List<int>>, string> saved_moves=board_to_play.PlayASingleGame(neural_web, false);
                string winner = saved_moves.Item3;
                for (int j = 0; j < saved_moves.Item1.Count; j++)
                {
                    CalculateWebData.Output(neural_web, saved_moves.Item2[j]);
                    CalculateWebData.BackwardPropagation(neural_web, saved_moves.Item1[j], 0.01);
                    neural_web.Clean();
                }
                if(i>90000)
                {
                    if (winner == "Web won")
                        tmp[0]++;
                    else if (winner == "Random player won")
                        tmp[1]++;
                    else if (winner == "Draw")
                        tmp[2]++;
                }
            }
            MessageBox.Show("Sieć wygrała " + Math.Round((tmp[0] / 10000), 7) * 100 + "% gier \n" +
                "Losowość wygrała " + Math.Round((tmp[1] / 10000), 7) * 100 + "% gier \n" +
                "Remis wystąpił w " + Math.Round((tmp[2] / 10000), 7) * 100 + "% gier \n");



            CalculateWebData.Output(neural_web, board_to_play.board);
            double best_value = board_to_play.ChooseBestValue(neural_web.layers[neural_web.layers.Count - 1]);
            int field = board_to_play.FindBestNeuronIndex(best_value, neural_web.layers[neural_web.layers.Count - 1]);
            board_to_play.board[field] = 1 ;
            SetButtonContent(field, web_character);
        }
        /*private void PlayVsHuman(int entered_character)
        {
            SetButtonContent(entered_character, human_character);
            board_to_play.board[entered_character] = human_character;
            if (board_to_play.GameResult(2) == 1)
            {
                MessageBox.Show("Gracz wygrywa!");
                return;
            }
            else if (board_to_play.GameResult(2) == -1)
            {
                MessageBox.Show("Remis!");
                return;
            }
                
            CalculateWebData.Output(neural_web, board_to_play.board);
            double best_value = board_to_play.ChooseBestValue(neural_web.layers[neural_web.layers.Count - 1]);
            int field = board_to_play.FindListIndex(best_value, neural_web.layers[neural_web.layers.Count - 1]);
            board_to_play.Move(2, field);
            SetButtonContent(field, web_character);
            board_to_play.GameResult(2);
            if (board_to_play.GameResult(2) == 2)
            {
                MessageBox.Show("Sieć wygrywa!");
                return;
            }
            else if (board_to_play.GameResult(2) == -1)
            {
                MessageBox.Show("Remis!");
                return;
            }
        }*/

        

        private void SetButtonContent(int field, int character)
        {
            string output_character = "";
            if (character==1) output_character = "0";
            else if (character == 2) output_character = "X";
            if (field == 0) Field11.Content = output_character;
            else if (field == 1) Field12.Content = output_character;
            else if (field == 2) Field13.Content = output_character;
            else if (field == 3) Field21.Content = output_character;
            else if (field == 4) Field22.Content = output_character;
            else if (field == 5) Field23.Content = output_character;
            else if (field == 6) Field31.Content = output_character;
            else if (field == 7) Field32.Content = output_character;
            else if (field == 8) Field33.Content = output_character;
        }

        private void Field11_Click(object sender, RoutedEventArgs e)
        {
            //if(game_started)
                //PlayVsHuman(0);
        }

        private void Field12_Click(object sender, RoutedEventArgs e)
        {
            //if (game_started)
                //PlayVsHuman(1);
        }

        private void Field13_Click(object sender, RoutedEventArgs e)
        {
            //if (game_started)
                //PlayVsHuman(2);
        }

        private void Field21_Click(object sender, RoutedEventArgs e)
        {
            //if (game_started)
                //PlayVsHuman(3);
        }

        private void Field22_Click(object sender, RoutedEventArgs e)
        {
            //if (game_started)
                //PlayVsHuman(4);
        }

        private void Field23_Click(object sender, RoutedEventArgs e)
        {
            //if (game_started)
                //PlayVsHuman(5);
        }

        private void Field31_Click(object sender, RoutedEventArgs e)
        {
            //if (game_started)
                //PlayVsHuman(6);
        }

        private void Field32_Click(object sender, RoutedEventArgs e)
        {
            //if (game_started)
                //PlayVsHuman(7);
        }

        private void Field33_Click(object sender, RoutedEventArgs e)
        {
            //if (game_started)
                //PlayVsHuman(8);
        }

        private void Clear_button_Click(object sender, RoutedEventArgs e)
        {
            board_to_play.board=board_to_play.BuildBoard();
            Field11.Content = "";
            Field12.Content = "";
            Field13.Content = "";
            Field21.Content = "";
            Field22.Content = "";
            Field23.Content = "";
            Field31.Content = "";
            Field32.Content = "";
            Field33.Content = "";
            CalculateWebData.Output(neural_web, board_to_play.board);
            double best_value = board_to_play.ChooseBestValue(neural_web.layers[neural_web.layers.Count - 1]);
            int field = board_to_play.FindBestNeuronIndex(best_value, neural_web.layers[neural_web.layers.Count - 1]);
            board_to_play.board[field] = 1;
            SetButtonContent(field, web_character);
        }
    }
}
