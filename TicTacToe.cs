using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KiK_SN
{
    class TicTacToe
    {
        public List<int> board=new List<int>();
        private Random rnd=new Random();
        public List<int> BuildBoard()
        {
            List<int> new_board = new List<int>();
            for (int i = 0; i < 18; i++)
                new_board.Add(0);
            for (int i = 18; i < 27; i++)
                new_board.Add(1);
            return new_board;
        }
        private List<int> CloneBoard(List<int> old_board)
        {
            List<int> new_board = new List<int>();
            foreach (var board_value in old_board)
                new_board.Add(board_value);
            return new_board;
        }
        private string CheckGameResult()
        {
            for (int i = 0; i < 9; i = i + 3)
            {
                int j = i + 9;
                if ((board[i] == 1 && board[i + 1] == 1 && board[i + 2] == 1) || (board[i] == 1 && board[i + 3] == 1 && board[i + 6] == 1)
                    || (board[0] == 1 && board[4] == 1 && board[8] == 1) || (board[2] == 1 && board[4] == 1 && board[6] == 1))
                    return "X won";
                else if ((board[j] == 1 && board[j + 1] == 1 && board[j + 2] == 1) || (board[j] == 1 && board[j + 3] == 1 && board[j + 6] == 1)
                    || (board[9] == 1 && board[13] == 1 && board[17] == 1) || (board[11] == 1 && board[13] == 1 && board[15] == 1))
                    return "0 won";
            }
            if (board[18] == 0 && board[19] == 0 && board[20] == 0 && board[21] == 0 &&
                    board[22] == 0 && board[23] == 0 && board[24] == 0 && board[25] == 0 && board[26] == 0)
                return "Draw";
            return "Game is not over";
        }
        public void RandomMove()
        {
            List<int> unoccupied_fields=new List<int>();
            for (int i = 18; i < board.Count; i++)
                if(board[i]==1)
                    unoccupied_fields.Add(i);
            int random_field = unoccupied_fields[rnd.Next(unoccupied_fields.Count - 1)];
            board[random_field - 9] = 1;
            board[random_field] = 0;
        }
        public Tuple<List<List<Double>>,List<List<int>>, string> PlayASingleGame(Web neural_web, bool web_move)
        {
            string result = "Game is not over";
            board = BuildBoard();
            List<List<double>> saved_web_outputs = new List<List<double>>();
            List<List<int>> saved_board_moves = new List<List<int>>();
            while (result == "Game is not over")
            {
                if (web_move)
                {
                    web_move = false;
                    saved_board_moves.Add(CloneBoard(board));
                    CalculateWebData.Output(neural_web, board);
                    double best_value=ChooseBestValue(neural_web.layers[neural_web.layers.Count - 1]);
                    int field = FindBestNeuronIndex(best_value, neural_web.layers[neural_web.layers.Count - 1]);
                    saved_web_outputs.Add(GetWebOutputValues(neural_web.layers[neural_web.layers.Count - 1]));
                    board[field] = 1;
                    board[18 + field] = 0;
                }
                else if(!web_move)
                {
                    web_move = true;
                    RandomMove();
                }
                result = CheckGameResult();
            }
            if (result == "Web won")
                FixBoardBestValue(saved_board_moves, saved_web_outputs, 1);
            else if (result == "Random player won")
                FixBoardBestValue(saved_board_moves, saved_web_outputs, 0);
            else if (result == "Draw")
                FixBoardBestValue(saved_board_moves, saved_web_outputs, 0.5);
            return new Tuple<List<List<double>>, List<List<int>>, string>(saved_web_outputs, saved_board_moves, result);
        }
        public Tuple<List<List<Double>>, List<List<Double>>, List<List<int>>, string> TrainAIvsAI(Web neural_web, Web neural_web_enemy)
        {
            string result = "Game is not over";
            board = BuildBoard();
            bool web1_move = true;
            List<List<double>> saved_web_outputs = new List<List<double>>();
            List<List<double>> saved_web_enemy_outputs = new List<List<double>>();
            List<List<int>> saved_board_moves = new List<List<int>>();
            while (result == "Game is not over")
            {
                if (web1_move)
                {
                    web1_move = false;
                    saved_board_moves.Add(CloneBoard(board));
                    CalculateWebData.Output(neural_web, board);
                    double best_value = ChooseBestValue(neural_web.layers[neural_web.layers.Count - 1]);
                    int field = FindBestNeuronIndex(best_value, neural_web.layers[neural_web.layers.Count - 1]);
                    saved_web_outputs.Add(GetWebOutputValues(neural_web.layers[neural_web.layers.Count - 1]));
                    board[field] = 1;
                    board[18 + field] = 0;
                }
                else if (!web1_move)
                {
                    web1_move = true;
                    CalculateWebData.Output(neural_web_enemy, board);
                    double best_value = ChooseBestValue(neural_web_enemy.layers[neural_web_enemy.layers.Count - 1]);
                    int field = FindBestNeuronIndex(best_value, neural_web_enemy.layers[neural_web_enemy.layers.Count - 1]);
                    saved_web_enemy_outputs.Add(GetWebOutputValues(neural_web_enemy.layers[neural_web_enemy.layers.Count - 1]));
                    board[9+field] = 1;
                    board[18 + field] = 0;
                }
                result = CheckGameResult();
            }
            if (result == "X won")
            {
                FixBoardBestValue(saved_board_moves, saved_web_outputs, 1);
                FixBoardBestValue(saved_board_moves, saved_web_enemy_outputs, 0);
            }
                
            else if (result == "0 won")
            {
                FixBoardBestValue(saved_board_moves, saved_web_outputs, 0);
                FixBoardBestValue(saved_board_moves, saved_web_enemy_outputs, 1);
            }
            else if (result == "Draw")
            {
                FixBoardBestValue(saved_board_moves, saved_web_outputs, 0.95);
                FixBoardBestValue(saved_board_moves, saved_web_enemy_outputs, 0.95);
            }
            return new Tuple<List<List<double>>, List<List<double>>, List<List<int>>, string>(saved_web_outputs,
                saved_web_enemy_outputs, saved_board_moves, result);
        }
        private void FixBoardBestValue(List<List<int>> saved_board_fields, List<List<double>> web_moves, double value)
        {
            for (int i = 0; i < web_moves.Count; i++)
                for (int j = 0; j < 9; j++)
                    if (saved_board_fields[i][j+18] == 0)
                        web_moves[i][j] = 0;
            for (int i = 0; i < web_moves.Count; i++)
            {
                int index_of_best_move= web_moves[i].IndexOf(web_moves[i].Max());
                web_moves[i][index_of_best_move] = value;
            }
        }

        public double ChooseBestValue(List<Neuron> list_of_neurons)
        {
            double best_value = 0;
            List<Neuron> tmp_list = new List<Neuron>();
            for (int i = 0; i < list_of_neurons.Count; i++)
                if (board[i+18] == 1)
                    tmp_list.Add(list_of_neurons[i]);
            best_value = tmp_list.Max(val => val.output);
            return best_value;
        }
        public int FindBestNeuronIndex(double best_value, List<Neuron> list_of_outputs)
        {
            for (int i = 0; i < list_of_outputs.Count; i++)
                if (best_value == list_of_outputs[i].output)
                    return i;
            return 0;
        }
        private List<double> GetWebOutputValues(List<Neuron> neuron_list)
        {
            List<double> values = new List<double>();
            foreach (var neuron in neuron_list)
                values.Add(neuron.output);
            return values;
        }
    }
}
