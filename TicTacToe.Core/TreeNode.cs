using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace TicTacToe.Core
{
    public class TreeNode<T>
    {
        private readonly T _value;
        private readonly List<TreeNode<T>> _children = new List<TreeNode<T>>();

        public TreeNode(T value)
        {
            _value = value;
        }

        public decimal GamesPlayed { get; private set; }
        public decimal GamesWon { get; private set; }

        public decimal UCB1(decimal totalN)
        {
            if (GamesPlayed == 0) return decimal.MaxValue;

            return (GamesWon / GamesPlayed) + (decimal)Math.Sqrt(2.0 * Math.Log((double)totalN) / (double)GamesPlayed);
        }

        public void IncrementGamesPlayedWon(bool won)
        {
            GamesPlayed++;
            if (won) GamesWon++;
            if (Parent != null) Parent.IncrementGamesPlayedWon(won);
        }

        public int GetNextUCB1Move(decimal totalN)
        {
            decimal ucb1 = 0.0M;
            int bestMove = Children[0].Move;
            foreach (var child in Children)
            {
                var w = child.UCB1(totalN);
                if (w > ucb1)
                {
                    ucb1 = w;
                    bestMove = child.Move;
                }
            }
            return bestMove;
        }

        public int GetBestMove()
        {
            decimal winPerc = 0.0M;
            int bestMove = Children[0].Move;
            foreach(var child in Children)
            {
                var w = child.GamesWon / child.GamesPlayed;
                if (w > winPerc)
                {
                    winPerc = w;
                    bestMove = child.Move;
                }
            }
            return bestMove;
        }

        public int Depth { get; set; }
        /// <summary>
        /// -1 is can't win, 0 is tie, 1 is win
        /// </summary>
        public int[] PlayerRanks { get; } = new int[3];

        public int Move { get; set; }
        
        public TreeNode<T> GetChildByMove(int move)
        {
            foreach (var child in Children) if (child.Move == move) return child;
            return null;
        }

        public TreeNode<T> this[int i]
        {
            get { return _children[i]; }
        }

        public TreeNode<T> Parent { get; private set; }

        public T Value { get { return _value; } }

        public ReadOnlyCollection<TreeNode<T>> Children
        {
            get { return _children.AsReadOnly(); }
        }

        public TreeNode<T> AddChild(T value)
        {
            var node = new TreeNode<T>(value) { Parent = this, Depth = Depth + 1 };
            _children.Add(node);
            return node;
        }

        public TreeNode<T>[] AddChildren(params T[] values)
        {
            return values.Select(AddChild).ToArray();
        }

        public bool RemoveChild(TreeNode<T> node)
        {
            return _children.Remove(node);
        }

        public void Traverse(Action<T> action)
        {
            action(Value);
            foreach (var child in _children)
                child.Traverse(action);
        }

        public IEnumerable<T> Flatten()
        {
            return new[] { Value }.Concat(_children.SelectMany(x => x.Flatten()));
        }
    }
}
