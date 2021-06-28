using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KiK_SN
{
    class TicTacToe
    {
        public List<double> game_board = new List<double>() { 0,0,0,0,0,0,0,0,0 };
        private List<double> CloneBoard(List<double> old_board)
        {
            List<Double> new_board = new List<double>();
            foreach (var board_value in old_board)
                new_board.Add(board_value);
            return new_board;
        }
        public int GameResult()
        {
            int X_win = 2;
            int Y_win = 1;
            if (CheckResultAtThisMoment(X_win) ==true)
                return 2;
            else if (CheckResultAtThisMoment(Y_win) == true)
                return 1;
            else if (game_board.Contains(0))
                return 0;
            return -1;
        }
        private bool CheckResultAtThisMoment(int value)
        {
            for (int i = 0; i < 3; i++)
                if (value == game_board[i] && value == game_board[i+1] && value == game_board[i+2])
                    return true;
            for (int i = 0; i < 3; i++)
                if (value == game_board[i] && value == game_board[i+3] && value == game_board[i+6])
                    return true;
            if (value == game_board[0] && value == game_board[4] && value == game_board[8] || 
                value == game_board[2] && value == game_board[4] && value == game_board[6])
                return true;
            return false;
        }
        public void RandomMove(int which_player)
        {
            List<double> unoccupied_fields = game_board.Where(field => field==0).ToList();
            Random rnd = new Random();
            double choosen_field = rnd.Next(unoccupied_fields.Count);
            for (int i = 0; i < game_board.Count; i++)
            {
                if (choosen_field == i && game_board[i] == 0)
                {
                    game_board[i] = which_player;
                    break;
                }  
                else if (game_board[i] > 0)
                    choosen_field++;
            }
        }
        public void Move(int which_player, int field)
        {
            game_board[field] = which_player;
        }
        public List<List<List<double>>> PlayASingleGame(Web neural_web)
        {
            int result = 0;
            int current_player = 2;
            List<List<double>> saved_web_moves = new List<List<double>>();
            List<List<double>> saved_board_moves = new List<List<double>>();
            List<List<double>> winner = new List<List<double>>();
            while (result == 0)
            {
                if (current_player == 2)
                {
                    saved_board_moves.Add(CloneBoard(game_board));
                    current_player = 1;
                    CalculateWebData.Output(neural_web, game_board);
                    double best_value=neural_web.layers[neural_web.layers.Count - 1].Max(neuron => neuron.output);
                    int field = FindListIndex(best_value, neural_web.layers[neural_web.layers.Count - 1]);
                    saved_web_moves.Add(GetWebOutputValues(neural_web.layers[neural_web.layers.Count - 1]));
                    if (game_board[field] != 0)
                        result = 5;
                    else
                        Move(2, field);
                }
                else if (current_player == 1)
                {
                    RandomMove(current_player);
                    current_player = 2;
                }
                if(result!=5)
                    result = GameResult();
            }
            if (result == 5)
                winner.Add(new List<double>() { 5 });
            else if (result == 2)
                winner.Add(new List<double>() { 2 });
            else if (result == 1)
                winner.Add(new List<double>() { 1 });
            else if (result == -1)
                winner.Add(new List<double>() { 0 });
            
            game_board = new List<double>() { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            return new List<List<List<double>>>() { saved_web_moves, saved_board_moves, winner };
        }
        private int FindListIndex(double best_value, List<Neuron> list_of_outputs)
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
