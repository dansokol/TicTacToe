using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TicTacToe.Core;

namespace TicTacToe
{
    public partial class frmTicTacToe : Form
    {
        TicTacToeGame _game = new TicTacToeGame();
        private IMoveStrategy _moveStrategy = null;
        private Dictionary<int, Button> _buttons = new Dictionary<int, Button>();

        public frmTicTacToe()
        {
            InitializeComponent();
            ResetGame();
        }

        private void ResetGame()
        {
            _moveStrategy = new MonteCarloMoveStrategy();
            this.panelBoard.Controls.Clear();
            CreateTicTacToeBoard();
            _game = new TicTacToeGame();

            if (chkComputerGoFirst.Checked) DoComputerMove(-1);
        }

        private void LogMessage(string msg)
        {
            this.richTextBox1.Text += msg + Environment.NewLine;
        }

        private void CreateTicTacToeBoard()
        {
            _buttons.Clear();
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    int index = y * 3 + x;
                    Button b = new Button();
                    b.Left = 10 + x * 100;
                    b.Text = "";
                    b.Top = 10 + y * 100;
                    b.Width = 100;
                    b.Height = 100;
                    b.Tag = index;
                    b.Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);
                    b.Click += B_Click;
                    _buttons.Add(index, b);
                    this.panelBoard.Controls.Add(b);
                }
            }
        }

        private void DoComputerMove(int previousMove)
        {
            int nextMove = _moveStrategy.CalculateNextMove(_game, previousMove);
            _moveStrategy.UpdateMove(nextMove);
            _buttons[nextMove].Text = _game.CurrentPlayer == 1 ? "X" : "O";
            _game.PerformMove(nextMove);
            CheckGameState();
        }

        private void CheckGameState()
        {
            if (_game.BoardState.IsGameOver)
            {
                LogMessage("GAME OVER");
                LogMessage(_game.BoardState.ToString());
            }
            else
            {
                lblPlayerTurn.Text = $"Player {_game.CurrentPlayer}'s trun";
            }
        }

        private void B_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            int index = (int)button.Tag;

            if (_game.IsValidMove(index))
            {
                button.Text = _game.CurrentPlayer == 1 ? "X" : "O";
                if (_game.PerformMove(index))
                {
                    DoComputerMove(index);
                }

                CheckGameState();
            }
        }

        private void btnRestart_Click(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void chkComputerGoFirst_CheckedChanged(object sender, EventArgs e)
        {            
        }
    }
}
