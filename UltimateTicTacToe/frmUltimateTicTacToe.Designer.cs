namespace UltimateTicTacToe
{
    partial class frmUltimateTicTacToe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelBoard = new System.Windows.Forms.Panel();
            this.lblPlayerTurn = new System.Windows.Forms.Label();
            this.lblBoardIndex = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // panelBoard
            // 
            this.panelBoard.BackColor = System.Drawing.Color.White;
            this.panelBoard.Location = new System.Drawing.Point(12, 12);
            this.panelBoard.Name = "panelBoard";
            this.panelBoard.Size = new System.Drawing.Size(750, 750);
            this.panelBoard.TabIndex = 0;
            // 
            // lblPlayerTurn
            // 
            this.lblPlayerTurn.AutoSize = true;
            this.lblPlayerTurn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerTurn.Location = new System.Drawing.Point(768, 12);
            this.lblPlayerTurn.Name = "lblPlayerTurn";
            this.lblPlayerTurn.Size = new System.Drawing.Size(135, 24);
            this.lblPlayerTurn.TabIndex = 1;
            this.lblPlayerTurn.Text = "Player 1\'s Turn";
            // 
            // lblBoardIndex
            // 
            this.lblBoardIndex.AutoSize = true;
            this.lblBoardIndex.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBoardIndex.Location = new System.Drawing.Point(768, 47);
            this.lblBoardIndex.Name = "lblBoardIndex";
            this.lblBoardIndex.Size = new System.Drawing.Size(75, 24);
            this.lblBoardIndex.TabIndex = 2;
            this.lblBoardIndex.Text = "Board 1";
            // 
            // frmUltimateTicTacToe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(943, 809);
            this.Controls.Add(this.lblBoardIndex);
            this.Controls.Add(this.lblPlayerTurn);
            this.Controls.Add(this.panelBoard);
            this.Name = "frmUltimateTicTacToe";
            this.Text = "Ultimate Tic Tac Toe";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelBoard;
        private System.Windows.Forms.Label lblPlayerTurn;
        private System.Windows.Forms.Label lblBoardIndex;
    }
}

