using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core
{
    public class MonteCarloMoveStrategy : IMoveStrategy
    {
        Random _random = new Random();

        // maintain the decision tree for future processing
        // save the tree for processing later
        TreeNode<TicTacToeGame> root = null;
        TreeNode<TicTacToeGame> currentRoot = null;

        public MonteCarloMoveStrategy()
        {
        }

        public void RunSimulation(int player, bool ucb1 = false)
        {
            var simRoot = currentRoot;

            // traverse down tree until game over
            while (!simRoot.Value.IsGameOver)
            {
                var game = simRoot.Value;
                var moves = game.GetPossibleMoves();
                int nextMove = -1;
                if (ucb1)
                {
                    // look at all children and select max ucb1
                    nextMove = simRoot.GetNextUCB1Move(root.GamesPlayed);
                }
                else
                {
                    nextMove = moves[_random.Next(0, moves.Count)];
                }
                simRoot = UpdateMove(simRoot, nextMove);
            }

            simRoot.IncrementGamesPlayedWon(simRoot.Value.IsTie || simRoot.Value.PlayerWon == player);
        }

        public int CalculateNextMove(TicTacToeGame game, int previousMove)
        {
            // set currentRoot
            if (root == null)
            {
                root = new TreeNode<TicTacToeGame>(game);
                currentRoot = root;
            }
            else if(previousMove != -1)
            {
                // we might have a child from root already
                UpdateMove(previousMove);
            }

            for (int i = 0; i < 1000; i++)
            {
                RunSimulation(game.CurrentPlayer);
            }

            for (int i = 0; i < 1000; i++)
            {
                RunSimulation(game.CurrentPlayer, true);
            }

            return currentRoot.GetBestMove();
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
            var newChild = UpdateMove(currentRoot, move);
            currentRoot = newChild;
        }
    }
}
