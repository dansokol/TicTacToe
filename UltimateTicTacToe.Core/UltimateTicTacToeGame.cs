using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Core;

namespace UltimateTicTacToe.Core
{
    public class UltimateTicTacToeGame
    {
        public UltimateTicTacToeGame()
        {
            for(int i = 0; i < GameState.Length; i++)
            {
                GameState[i] = new TicTacToeGame();
            }
            CurrentPlayer = 1;
            CurrentBoardIndex = -1;
        }

        /// <summary>
        /// Ultimate tic tac toe is really 9 individual tic tac toe games
        /// plus a 10th one to represent the overall global game
        /// </summary>
        public TicTacToeGame[] GameState {get; } = new TicTacToeGame[10];

        // game state could be a 256 bit number
        // would make it easier to copy/duplicate game

        public TicTacToeGame GlobalGame { get { return GameState[9]; } }

        public int CurrentPlayer
        {
            get
            {
                return GameState[9].CurrentPlayer;
            }
            set
            {
                GameState[9].CurrentPlayer = value;
            }
        }
        public int CurrentBoardIndex { get; set; }

        /// <summary>
        /// Calculates the entire board state and returns whether game is over
        /// </summary>
        /// <param name="player"></param>
        /// <returns>true if game is over</returns>
        public bool CalculateBoardState(int player)
        {
            return GlobalGame.CalculateBoardState(player);
        }

        public bool IsValidMove(int boardIndex, int index)
        {
            if (CurrentBoardIndex != -1 && CurrentBoardIndex != boardIndex) return false;
            return GameState[boardIndex].IsValidMove(index);
        }

        public bool PerformMove(int boardIndex, int index)
        {
            var miniGame = GameState[boardIndex];
            miniGame.CurrentPlayer = CurrentPlayer;
            if (!miniGame.PerformMove(index))
            {
                // if it's a tie then mark it as -1
                if(miniGame.IsTie)
                {
                    GlobalGame.GameState[boardIndex] = -1;
                }
                else if (miniGame.IsWin)
                {
                    GlobalGame.GameState[boardIndex] = CurrentPlayer;
                }
            }

            // check for game over
            if (!CalculateBoardState(CurrentPlayer))
            {
                GlobalGame.CurrentPlayer = CurrentPlayer  == 1 ? 2 : 1;
            }
            else
            {
                return false;
            }

            // calculate the board index
            if (GlobalGame.GameState[index] != 0)
            {
                CurrentBoardIndex = -1;
            }
            else
            {
                CurrentBoardIndex = index;
            }

            return true;
        }

        public UltimateTicTacToeGame Copy()
        {
            UltimateTicTacToeGame t = new UltimateTicTacToeGame();
            for (int i = 0; i < GameState.Length; i++) t.GameState[i] = GameState[i].Copy();
            t.CurrentBoardIndex = CurrentBoardIndex;
            return t;
        }
    }
}
