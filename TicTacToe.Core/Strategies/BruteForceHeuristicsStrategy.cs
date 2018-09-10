using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core
{
    public class BruteForceHeuristicsStrategy : IMoveStrategy
    {
        // TODO: update/check decision tree on every CalculateNextMove call
        // maintain the decision tree for future processing
        // save the tree for processing later
        TreeNode<TicTacToeGame> root = null;
        TreeNode<TicTacToeGame> currentRoot = null;
        int _depth = 2;

        public BruteForceHeuristicsStrategy(int depth)
        {
            _depth = depth;
        }

        private int GetRank(TicTacToeGame game, int player)
        {
            if (game.BoardState.IsGameOver)
            {
                if (game.BoardState.IsWin)
                {
                    return player == game.BoardState.PlayerWon ? Int32.MaxValue : Int32.MinValue;
                }
                return 0;
            }

            // do some heuristics
            int value = 0;
            for (int i = 0; i < 9; i++)
            {
                int playerControls = game.GameState[i];
                if (playerControls > 0)
                {
                    // center piece
                    if (i == 4)
                    {
                        value += playerControls == player ? 10 : -10;
                    }

                    // diagonals
                    if (i == 0 || i == 2 || i == 6 || i == 8)
                    {
                        value += playerControls == player ? 5 : -5;
                    }
                }
            }

            return value;
        }

        private void BuildDecisionTree(TicTacToeGame game, int player, TreeNode<TicTacToeGame> node, int depth)
        {
            node.Rank = GetRank(game, player);

            if (depth == 0) return;

            var moves = game.GetPossibleMoves();
            foreach (var move in moves)
            {
                var copy = game.Copy();
                var child = node.AddChild(copy);
                child.Move = move;
                if (copy.PerformMove(move))
                {
                    BuildDecisionTree(copy, player, child, depth - 1);
                }
                else
                {
                    child.Rank = GetRank(copy, player);
                }
            }
        }

        private decimal minimax(TreeNode<TicTacToeGame> node, int depth, int player)
        {
            if (node.Children.Count == 0 || depth == 0)
            {
                return node.Rank;
            }

            // maximizing player
            if (node.Value.CurrentPlayer == player)
            {
                var bestValue = decimal.MinValue;
                foreach (var child in node.Children)
                {
                    var v = minimax(child, depth - 1, player);
                    bestValue = Math.Max(v, bestValue);
                }
                return bestValue;
            }
            else
            {
                var bestValue = decimal.MaxValue;
                foreach (var child in node.Children)
                {
                    var v = minimax(child, depth - 1, player);
                    bestValue = Math.Min(v, bestValue);
                }
                return bestValue;
            }
        }

        public int CalculateNextMove(TicTacToeGame game, int previousMove)
        {
            // check decision tree at each step
            if (root == null)
            {
                root = new TreeNode<TicTacToeGame>(game);
                BuildDecisionTree(game, game.CurrentPlayer, root, _depth);
                currentRoot = root;
            }
            else if (previousMove != -1)
            {
                // we might have a child from root already
                UpdateMove(previousMove);
            }

            // now we have the decision tree, now what's the best move?
            TreeNode<TicTacToeGame> best = currentRoot.Children[0];
            var bestValue = decimal.MinValue;
            foreach (var child in currentRoot.Children)
            {
                var v = minimax(child, _depth, game.CurrentPlayer);
                if (v > bestValue)
                {
                    bestValue = v;
                    best = child;
                }
            }
            return best.Move;
        }

        /// <summary>
        /// Either gets existing game from children or adds new child with move
        /// </summary>
        /// <param name="node"></param>
        /// <param name="move"></param>
        private TreeNode<TicTacToeGame> UpdateMove(TreeNode<TicTacToeGame> node, int move)
        {
            var child = node.GetChildByMove(move);
            if (child == null)
            {
                var game = node.Value.Copy();
                game.PerformMove(move);
                var newChild = node.AddChild(game);
                newChild.Move = move;
                return newChild;
            }
            else
            {
                return child;
            }
        }

        public void UpdateMove(int move)
        {
            // no need to recalculate the entire tree, just update the pointer and expand
            var newChild = UpdateMove(currentRoot, move);
            currentRoot = newChild;
        }
    }
}
