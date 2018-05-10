using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UltimateTicTacToe.Core;

namespace UltimateTicTacToe
{
    public partial class frmUltimateTicTacToe : Form
    {
        UltimateTicTacToeGame _game = new UltimateTicTacToeGame();
        private Dictionary<int, Panel> _panels = new Dictionary<int, Panel>();
        private Dictionary<int, Button> _buttons = new Dictionary<int, Button>();

        public frmUltimateTicTacToe()
        {
            InitializeComponent();
            CreateBoard();
            UpdateUI();
        }

        private void CreateTicTacToeBoard(int gy, int gx)
        {
            int globalIndex = gy * 3 + gx;
            int left = gx * (80 * 3) + 9;
            int top = gy * (80 * 3) + 9;
            _buttons.Clear();

            Panel p = new Panel();
            p.Left = left;
            p.Top = top;
            p.Width = 75 * 3 + 10;
            p.Height = 75 * 3 + 10;
            p.BorderStyle = BorderStyle.FixedSingle;
            this.panelBoard.Controls.Add(p);
            _panels.Add(globalIndex, p);

            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    int index = y * 3 + x;
                    Button b = new Button();
                    b.Left = 3 + x * 75;
                    b.Text = "";
                    b.Top = 3 + y * 75;
                    b.Width = 75;
                    b.Height = 75;
                    b.Tag = new Tuple<int, int>(globalIndex, index);
                    b.Font = new Font(FontFamily.GenericSansSerif, 14, FontStyle.Bold);
                    b.Click += B_Click;

                    //_buttons.Add(index, b);

                    //this.panelBoard.Controls.Add(b);
                    p.Controls.Add(b);
                }
            }
        }

        private void CreateBoard()
        {
            _buttons.Clear();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    CreateTicTacToeBoard(i, j);
                }
            }
        }

        private string GetPlayerChar()
        {
            return _game.CurrentPlayer == 1 ? "X" : "O";
        }

        private void UpdateUI()
        {
            lblPlayerTurn.Text = $"Player {GetPlayerChar()}'s turn.";
            lblBoardIndex.Text = $"Board {_game.CurrentBoardIndex}";

            for (int i = 0; i < 9; i++)
            {
                if (_game.GlobalGame.GameState[i] == -1)
                {
                    _panels[i].BackColor = Color.LightBlue;
                }
                else if (_game.GlobalGame.GameState[i] == 1)
                {
                    _panels[i].BackColor = Color.LightGreen;
                }
                else if (_game.GlobalGame.GameState[i] == 2)
                {
                    _panels[i].BackColor = Color.LightPink;
                }
                else
                {
                    _panels[i].BackColor = Color.White;
                }
            }
            
            if(!_game.GlobalGame.IsGameOver)
            {
                if (_game.CurrentBoardIndex > -1)
                {
                    panelBoard.BackColor = Color.WhiteSmoke;
                    _panels[_game.CurrentBoardIndex].BackColor = Color.LightGoldenrodYellow;
                }
                else
                {
                    panelBoard.BackColor = Color.LightGoldenrodYellow;
                }
            }   
        }

        private void B_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            var indexes = (Tuple<int, int>)button.Tag;
            int boardIndex = indexes.Item1;
            int index = indexes.Item2;

            if (_game.IsValidMove(boardIndex, index))
            {
                button.Text = GetPlayerChar();
                _game.PerformMove(boardIndex, index);
                UpdateUI();
            }

            //if (_game.IsValidMove(index))
            //{
            //    button.Text = _game.CurrentPlayer == 1 ? "X" : "O";
            //    if (_game.PerformMove(index))
            //    {
            //        DoComputerMove();
            //    }

            //    CheckGameState();
            //}
        }
    }
}
