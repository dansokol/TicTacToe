using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core
{
    public class RandomMoveStrategy : IMoveStrategy
    {
        Random _random;

        public RandomMoveStrategy()
        {
            _random = new Random();
        }

        public int CalculateNextMove(TicTacToeGame game, int previousMove)
        {
            var moves = game.GetPossibleMoves();
            int i = _random.Next(0, moves.Count);
            return moves[i];
        }

        public void UpdateMove(int move)
        {
        }
    }
}
