using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core
{
    public interface IMoveStrategy
    {
        void UpdateMove(int move);
        int CalculateNextMove(TicTacToeGame game, int previousMove);
    }
}
