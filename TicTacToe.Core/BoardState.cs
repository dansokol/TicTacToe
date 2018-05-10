using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Core
{
    public class BoardState
    {
        public bool IsGameOver
        {
            get
            {
                return IsTie || IsWin;
            }
        }
        public bool IsTie { get; set; }
        public bool IsWin { get; set; }
        public int PlayerWon { get; set; }
        public int[] WinningCells { get;} = new int[3];

        public void SetWinner(int player, int cell1, int cell2, int cell3)
        {
            IsWin = true;
            PlayerWon = player;
            WinningCells[0] = cell1;
            WinningCells[1] = cell2;
            WinningCells[2] = cell3;
        }

        public override string ToString()
        {
            return $"GameOver:{IsGameOver}, IsTie:{IsTie}, IsWin:{IsWin}, PlayerWon:{PlayerWon}" + (IsWin ? $"-{WinningCells[0]}{WinningCells[1]}{WinningCells[2]}" : "");
        }
    }
}
