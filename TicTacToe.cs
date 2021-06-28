using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace KiK_SN
{
    class TicTacToe
    {
        List<double> game_board = new List<double>();
        public int GameResult()
        {
            int X_win = 2;
            int Y_win = 1;
            if (CheckResultAtThisMoment(X_win) ==true)
                return 2;
            else if (CheckResultAtThisMoment(Y_win) == true)
                return 1;
            else if (!game_board.Contains(0))
                return 0;
            return -1;
        }
        private bool CheckResultAtThisMoment(int value)
        {
            for (int i = 0; i < 3; i++)
                if (value == game_board[i] && value == game_board[i+1] && value == game_board[i+2])
                    return true;
            for (int i = 0; i < 3; i = i++)
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
                else if (game_board[i] < 0)
                    choosen_field++;
            }
        }
        public void Move(int which_player, int field)
        {
            game_board[field] = which_player;
        }
    }
}
