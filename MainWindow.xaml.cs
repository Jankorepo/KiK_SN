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
            neural_web.web_structure = new List<int>() { 27,18,9 };
            neural_web.Fill(neural_web);
            neural_web_enemy.web_structure = new List<int>() { 27, 18, 9 };
            neural_web_enemy.Fill(neural_web_enemy);
        }

        private void Button01_Click(object sender, RoutedEventArgs e)
        {
            int epochs = 100000;
            for (int i = 0; i < epochs; i++)
            {
                Tuple<List<List<Double>>, List<List<int>>, string> saved_moves=board_to_play.PlayASingleGame(neural_web, true);
                string winner = saved_moves.Item3;
                for (int j = 0; j < saved_moves.Item1.Count; j++)
                {
                    CalculateWebData.Output(neural_web, saved_moves.Item2[j]);
                    CalculateWebData.BackwardPropagation(neural_web, saved_moves.Item1[j], 0.01);
                    neural_web.Clean();
                }
            }
            for (int i = 0; i < epochs; i++)
            {
                Tuple<List<List<Double>>, List<List<int>>, string> saved_moves = board_to_play.PlayASingleGame(neural_web_enemy, false);
                for (int j = 0; j < saved_moves.Item1.Count; j++)
                {
                    CalculateWebData.Output(neural_web_enemy, saved_moves.Item2[j]);
                    CalculateWebData.BackwardPropagation(neural_web_enemy, saved_moves.Item1[j], 0.01);
                    neural_web_enemy.Clean();
                }
            }
            int x = 0;
            int o = 0;
            int draw = 0;
            for (int i = 0; i < epochs; i++)
            {
                Tuple<List<List<Double>>, List<List<Double>>, List<List<int>>, string> saved_moves = board_to_play.TrainAIvsAI(neural_web, neural_web_enemy);
                
                for (int j = 0; j < saved_moves.Item2.Count; j++)
                {
                    CalculateWebData.Output(neural_web_enemy, saved_moves.Item3[j]);
                    CalculateWebData.BackwardPropagation(neural_web_enemy, saved_moves.Item2[j], 0.01);
                    neural_web_enemy.Clean();
                }
                for (int j = 0; j < saved_moves.Item1.Count; j++)
                {
                    CalculateWebData.Output(neural_web, saved_moves.Item3[j]);
                    CalculateWebData.BackwardPropagation(neural_web, saved_moves.Item1[j], 0.01);
                    neural_web.Clean();
                }
                if(i>90000)
                {
                    if (saved_moves.Item4 == "X won")
                        x++;
                    else if (saved_moves.Item4 == "0 won")
                        o++;
                    else
                        draw++;
                }
                
            }
            MessageBox.Show("Sieć pierwsza wygrała " + x + " gier, sieć druga wygrała " + o + " gier, a remisów było " + draw);
        }
        
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
