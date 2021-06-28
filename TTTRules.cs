using System;
using System.Collections.Generic;
using System.Text;

namespace KiK_SN
{
    static class TTTRules
    {
        public static int GameResult(List<double> game_board)
        {
            int X_win = 2;
            int Y_win = 1;
            if (CheckResultAtThisMoment(game_board, X_win) ==true)
                return 2;
            else if (CheckResultAtThisMoment(game_board, Y_win) == true)
                return 1;
            else if (!game_board.Contains(0))
                return 0;
            return -1;
        }

        private static bool CheckResultAtThisMoment(List<double> game_board, int value)
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
    }
}
