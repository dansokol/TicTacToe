using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core
{
    public class LookAheadStrategy : IMoveStrategy
    {
        int _searchDepth = 5;

        public LookAheadStrategy() { }

        public LookAheadStrategy(int searchDepth)
        {
            _searchDepth = searchDepth;
        }

        private int GetRank(TicTacToeGame game, int player)
        {
            if (game.BoardState.IsGameOver)
            {
                if (game.BoardState.IsWin)
                {
                    return player == game.BoardState.PlayerWon ? 1 : -1;
                }
            }

            return 0;
        }

        private void BuildDecisionTree(TicTacToeGame game, int player, TreeNode<TicTacToeGame> node)
        {
            var moves = game.GetPossibleMoves();
            foreach (var move in moves)
            {
                var copy = game.Copy();
                var child = node.AddChild(copy);
                child.Move = move;
                if (copy.PerformMove(move))
                {
                    BuildDecisionTree(copy, player, child);
                }
                else
                {
                    child.PlayerRanks[1] = GetRank(copy, 1);
                    child.PlayerRanks[2] = GetRank(copy, 2);
                }
            }
        }

        private int minimax(TreeNode<TicTacToeGame> node, int depth, int player)
        {
            if (node.Children.Count == 0 || depth == 0)
            {
                return node.PlayerRanks[player];
            }

            // maximizing player
            if (node.Value.CurrentPlayer == player)
            {
                int bestValue = -1;
                foreach (var child in node.Children)
                {
                    int v = minimax(child, depth - 1, player);
                    bestValue = Math.Max(v, bestValue);
                }
                return bestValue;
            }
            else
            {
                int bestValue = 1;
                foreach (var child in node.Children)
                {
                    int v = minimax(child, depth - 1, player);
                    bestValue = Math.Min(v, bestValue);
                }
                return bestValue;
            }
        }

        public int CalculateNextMove(TicTacToeGame game, int previousMove)
        {
            TreeNode<TicTacToeGame> root = new TreeNode<TicTacToeGame>(game);
            BuildDecisionTree(game, game.CurrentPlayer, root);

            // now we have the decision tree, now what's the best move?
            TreeNode<TicTacToeGame> best = root.Children[0];
            int bestValue = -1;
            foreach (var child in root.Children)
            {
                int v = minimax(child, _searchDepth, game.CurrentPlayer);
                if (v > bestValue)
                {
                    bestValue = v;
                    best = child;
                }
            }
            return best.Move;
        }

        public void UpdateMove(int move)
        {
        }
    }
}
