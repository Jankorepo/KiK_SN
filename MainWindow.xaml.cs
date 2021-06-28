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
        TicTacToe board = new TicTacToe();
        public MainWindow()
        {
            InitializeComponent();
            neural_web.web_structure = new List<int>() { 9,5,9 };
            neural_web.Fill(neural_web);
        }

        private void Button01_Click(object sender, RoutedEventArgs e)
        {
            int epochs = 1000000;
            int games_to_leave = 0;
            List<double> licz = new List<double>() { 0,0,0 };
            for (int i = 0; i < epochs; i++)
            {
                List<List<List<Double>>> saved_moves = board.PlayASingleGame(neural_web);
                double winner = saved_moves[2][0][0];
                PrepareSavedMovesToLearn(saved_moves, winner);
                for (int j= 0; j < saved_moves[0].Count; j++)
                {
                    CalculateWebData.Output(neural_web, saved_moves[1][j]);
                    CalculateWebData.BackwardPropagation(neural_web, saved_moves[0][j], 0.1);
                    neural_web.Clean();
                }
                if (winner == 2)
                    licz[2]++;
                else if (winner == 1)
                    licz[1]++;
                else if (winner == 0)
                    licz[0]++;
                else if (winner == 5)
                    games_to_leave++;
            }
            MessageBox.Show("Sieć wygrała "+ Math.Round((licz[2] / (epochs - games_to_leave)), 4)*100 + "% gier"+"\n"+
            "Losowość wygrała " + Math.Round((licz[1] /( epochs - games_to_leave)), 4) * 100 + "% gier" + "\n" +
            "Równość wygrała " + Math.Round((licz[0] / (epochs - games_to_leave)), 4) * 100 + "% gier" + "\n");
        }

        private void PrepareSavedMovesToLearn(List<List<List<double>>> saved_moves, double winner)
        {
            List<List<Double>> web_moves = saved_moves[0];
            if(winner==2)
                ChooseMoveToChange(web_moves, 1);
            else if(winner == 1 || winner ==5)
                ChooseMoveToChange(web_moves, 0);
            else if (winner == 0)
                ChooseMoveToChange(web_moves, 0.95);
        }

        private void ChooseMoveToChange(List<List<double>> web_moves, double value)
        {
            foreach (var single_move in web_moves)
                for (int i = 0; i < single_move.Count; i++)
                    if (single_move.Max() == single_move[i])
                        single_move[i] = value;
        }
    }
}
