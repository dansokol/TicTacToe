using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core
{
    public class TicTacToeGame
    {

        public TicTacToeGame()
        {
            // initialize game state (which should be all 0's)
            // set current player's turn
            GameState[9] = 1;
            BoardState = new BoardState();
        }

        public TicTacToeGame(int[] gameState, BoardState boardState = null)
        {
            GameState = gameState;
            BoardState = boardState ?? new BoardState();
        }

        /// <summary>
        /// 9 integer board starting at top left
        /// e.g. 
        /// X | O | 
        ///   |   | 
        ///   | O | X
        ///   
        /// would be 
        /// 10th integer is the current player's turn
        /// </summary>
        public int[] GameState { get; } = new int[10];

        public int CurrentPlayer
        {
            get
            {
                return GameState[9];
            }
            set
            {
                GameState[9] = value;
            }
        }

        public BoardState BoardState { get; private set; }

        public bool IsGameOver { get { return BoardState.IsGameOver;  } }
        public bool IsTie { get { return BoardState.IsTie; } }
        public bool IsWin { get { return BoardState.IsWin; } }
        public int PlayerWon { get { return BoardState.PlayerWon; } }

        /// <summary>
        /// Calculates board state and returns whether game is over 
        /// </summary>
        /// <param name="player"></param>
        /// <returns>true if game is over</returns>
        public bool CalculateBoardState(int player)
        {
            // check for horizontal/vertical wins
            for (int i = 0; i < 3; i++)
            {
                // check for vertical
                if (GameState[i] == player && GameState[i + 3] == player && GameState[i + 6] == player)
                {
                    BoardState.SetWinner(player, i, i + 3, i + 6);
                    return true;
                }
                // check horizontal
                if (GameState[i * 3] == player && GameState[i * 3 + 1] == player && GameState[i*3 + 2] == player)
                {
                    BoardState.SetWinner(player, i * 3, i * 3 + 1, i * 3 + 2);
                    return true;
                }
            }

            // check for crosses
            if (GameState[4] == player)
            {
                if (GameState[0] == player && GameState[8] == player)
                {
                    BoardState.SetWinner(player, 0, 4, 8);
                    return true;
                        
                }
                else if (GameState[2] == player && GameState[6] == player)
                {
                    BoardState.SetWinner(player, 2, 4, 6);
                    return true;
                }
            }

            // check for ties
            bool isTie = true;
            for(int i = 0; i < 9; i++)
            {
                if(GameState[i] == 0)
                {
                    isTie = false;
                    break;
                }
            }

            if (isTie)
            {
                BoardState.IsTie = true;
                return true;
            }

            return false;
        }

        public List<int> GetPossibleMoves()
        {
            List<int> moves = new List<int>();
            for (int i = 0; i < 9; i++)
            {
                if (GameState[i] == 0) moves.Add(i);
            }
            return moves;
        }
        public bool IsValidMove(int index)
        {
            if (GameState[index] != 0) return false;
            if (BoardState.IsGameOver) return false;

            return true;
        }

        /// <summary>
        /// performs the requested move and updates the BoardState
        /// </summary>
        /// <param name="player"></param>
        /// <param name="index"></param>
        /// <returns>false if invalid move</returns>
        public bool PerformMove(int index)
        {
            // simple validation
            if (!IsValidMove(index)) return false;

            int player = CurrentPlayer;

            GameState[index] = player;
            
            // check for game over
            if (!CalculateBoardState(player))
            {
                GameState[9] = player == 1 ? 2 : 1;
            }
            else
            {
                return false;
            }
            
            return true;
        }

        public bool PerformMove(int y, int x)
        {
            return PerformMove(y * 3 + x);
        }
        
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if(j > 0) sb.Append("\t");
                    int index = i * 3 + j;
                    int player = GameState[index];
                    if (player > 0)
                    {
                        sb.Append(player == 1 ? "X\t" : "O\t");
                    } else
                    {
                        sb.Append("-\t");
                    }
                    if (j < 2) sb.Append("|");
                }
                sb.AppendLine();
                if(i < 2) sb.AppendLine("----------------------------------------------");
            }
            return sb.ToString();
        }

        public TicTacToeGame Copy()
        {
            TicTacToeGame t = new TicTacToeGame();
            for (int i = 0; i < GameState.Length; i++) t.GameState[i] = GameState[i];
            return t;
        }
    }
}
