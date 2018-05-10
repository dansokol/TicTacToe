using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TicTacToe.Core;
using System.Diagnostics;

namespace TicTacToe.Core.UnitTests
{
    [TestClass]
    public class TicTacGameTests
    {
        [TestMethod]
        public void TestPossibleMoves()
        {
            TicTacToeGame game = new TicTacToeGame();

            var moves = game.GetPossibleMoves();
            Assert.AreEqual(9, moves.Count);

            game.PerformMove(1, 4);
            moves = game.GetPossibleMoves();
            Assert.AreEqual(8, moves.Count);

            game.PerformMove(2, 3);
            moves = game.GetPossibleMoves();
            Assert.AreEqual(7, moves.Count);

            for (int i = 0; i < moves.Count; i++)
            {
                Assert.AreNotEqual(4, moves[i]);
                Assert.AreNotEqual(3, moves[i]);
            }

            Debug.WriteLine(game.ToString());
        }

        [TestMethod]
        public void TestGameState()
        {
            TicTacToeGame game = new TicTacToeGame();
            game.PerformMove(1, 0);
            game.PerformMove(2, 1);
            game.PerformMove(1, 3);
            Debug.WriteLine(game.ToString());
            Debug.WriteLine(game.BoardState.ToString());
            game.PerformMove(2, 2);
            game.PerformMove(1, 6);

            Debug.Write(game.ToString());
            Debug.WriteLine(game.BoardState.ToString());
        }

        [TestMethod]
        public void TestCompVsComp()
        {
            // test/train strategies against each other

        }
    }
}
