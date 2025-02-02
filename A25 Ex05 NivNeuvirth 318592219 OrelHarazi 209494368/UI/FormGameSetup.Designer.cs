namespace UI
{
    partial class FormGameSetup
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
            this.labelBoardSize = new System.Windows.Forms.Label();
            this.buttonDone = new System.Windows.Forms.Button();
            this.radioButtonSmallBoardSize = new System.Windows.Forms.RadioButton();
            this.radioButtonMediumBoardSize = new System.Windows.Forms.RadioButton();
            this.radioButtonLargeBoardSize = new System.Windows.Forms.RadioButton();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.labelPlayer1 = new System.Windows.Forms.Label();
            this.checkBoxPlayer2 = new System.Windows.Forms.CheckBox();
            this.textBoxPlayer1Name = new System.Windows.Forms.TextBox();
            this.textBoxPlayer2Name = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // labelBoardSize
            // 
            this.labelBoardSize.AutoSize = true;
            this.labelBoardSize.Location = new System.Drawing.Point(22, 20);
            this.labelBoardSize.Name = "labelBoardSize";
            this.labelBoardSize.Size = new System.Drawing.Size(91, 20);
            this.labelBoardSize.TabIndex = 0;
            this.labelBoardSize.Text = "Board Size:";
            // 
            // buttonDone
            // 
            this.buttonDone.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.buttonDone.Location = new System.Drawing.Point(248, 239);
            this.buttonDone.Name = "buttonDone";
            this.buttonDone.Size = new System.Drawing.Size(96, 33);
            this.buttonDone.TabIndex = 4;
            this.buttonDone.Text = "Done";
            this.buttonDone.UseVisualStyleBackColor = false;
            this.buttonDone.Click += new System.EventHandler(this.buttonDone_Click);
            // 
            // radioButtonSmallBoardSize
            // 
            this.radioButtonSmallBoardSize.AutoSize = true;
            this.radioButtonSmallBoardSize.Checked = true;
            this.radioButtonSmallBoardSize.Location = new System.Drawing.Point(46, 53);
            this.radioButtonSmallBoardSize.Name = "radioButtonSmallBoardSize";
            this.radioButtonSmallBoardSize.Size = new System.Drawing.Size(67, 24);
            this.radioButtonSmallBoardSize.TabIndex = 5;
            this.radioButtonSmallBoardSize.TabStop = true;
            this.radioButtonSmallBoardSize.Text = "6 x 6";
            this.radioButtonSmallBoardSize.UseVisualStyleBackColor = true;
            // 
            // radioButtonMediumBoardSize
            // 
            this.radioButtonMediumBoardSize.AutoSize = true;
            this.radioButtonMediumBoardSize.Location = new System.Drawing.Point(155, 53);
            this.radioButtonMediumBoardSize.Name = "radioButtonMediumBoardSize";
            this.radioButtonMediumBoardSize.Size = new System.Drawing.Size(67, 24);
            this.radioButtonMediumBoardSize.TabIndex = 6;
            this.radioButtonMediumBoardSize.Text = "8 x 8";
            this.radioButtonMediumBoardSize.UseVisualStyleBackColor = true;
            // 
            // radioButtonLargeBoardSize
            // 
            this.radioButtonLargeBoardSize.AutoSize = true;
            this.radioButtonLargeBoardSize.Location = new System.Drawing.Point(259, 53);
            this.radioButtonLargeBoardSize.Name = "radioButtonLargeBoardSize";
            this.radioButtonLargeBoardSize.Size = new System.Drawing.Size(85, 24);
            this.radioButtonLargeBoardSize.TabIndex = 7;
            this.radioButtonLargeBoardSize.Text = "10 x 10";
            this.radioButtonLargeBoardSize.UseVisualStyleBackColor = true;
            // 
            // labelPlayers
            // 
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Location = new System.Drawing.Point(22, 103);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(64, 20);
            this.labelPlayers.TabIndex = 8;
            this.labelPlayers.Text = "Players:";
            // 
            // labelPlayer1
            // 
            this.labelPlayer1.AutoSize = true;
            this.labelPlayer1.Location = new System.Drawing.Point(42, 147);
            this.labelPlayer1.Name = "labelPlayer1";
            this.labelPlayer1.Size = new System.Drawing.Size(69, 20);
            this.labelPlayer1.TabIndex = 9;
            this.labelPlayer1.Text = "Player 1:";
            // 
            // checkBoxPlayer2
            // 
            this.checkBoxPlayer2.AutoSize = true;
            this.checkBoxPlayer2.Location = new System.Drawing.Point(45, 193);
            this.checkBoxPlayer2.Name = "checkBoxPlayer2";
            this.checkBoxPlayer2.Size = new System.Drawing.Size(95, 24);
            this.checkBoxPlayer2.TabIndex = 11;
            this.checkBoxPlayer2.Text = "Player 2:";
            this.checkBoxPlayer2.UseVisualStyleBackColor = true;
            this.checkBoxPlayer2.CheckedChanged += new System.EventHandler(this.checkBoxPlayer2_CheckedChanged);
            // 
            // textBoxPlayer1Name
            // 
            this.textBoxPlayer1Name.Location = new System.Drawing.Point(183, 144);
            this.textBoxPlayer1Name.Name = "textBoxPlayer1Name";
            this.textBoxPlayer1Name.Size = new System.Drawing.Size(161, 26);
            this.textBoxPlayer1Name.TabIndex = 12;
            // 
            // textBoxPlayer2Name
            // 
            this.textBoxPlayer2Name.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxPlayer2Name.Enabled = false;
            this.textBoxPlayer2Name.Location = new System.Drawing.Point(183, 188);
            this.textBoxPlayer2Name.Name = "textBoxPlayer2Name";
            this.textBoxPlayer2Name.Size = new System.Drawing.Size(161, 26);
            this.textBoxPlayer2Name.TabIndex = 13;
            this.textBoxPlayer2Name.Text = "[Computer]";
            // 
            // FormGameSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 305);
            this.Controls.Add(this.textBoxPlayer2Name);
            this.Controls.Add(this.textBoxPlayer1Name);
            this.Controls.Add(this.checkBoxPlayer2);
            this.Controls.Add(this.labelPlayer1);
            this.Controls.Add(this.labelPlayers);
            this.Controls.Add(this.radioButtonLargeBoardSize);
            this.Controls.Add(this.radioButtonMediumBoardSize);
            this.Controls.Add(this.radioButtonSmallBoardSize);
            this.Controls.Add(this.buttonDone);
            this.Controls.Add(this.labelBoardSize);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormGameSetup";
            this.Text = "Game Settings";
            this.Load += new System.EventHandler(this.FormGameSetup_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelBoardSize;
        private System.Windows.Forms.Button buttonDone;
        private System.Windows.Forms.RadioButton radioButtonSmallBoardSize;
        private System.Windows.Forms.RadioButton radioButtonMediumBoardSize;
        private System.Windows.Forms.RadioButton radioButtonLargeBoardSize;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Label labelPlayer1;
        private System.Windows.Forms.CheckBox checkBoxPlayer2;
        private System.Windows.Forms.TextBox textBoxPlayer1Name;
        private System.Windows.Forms.TextBox textBoxPlayer2Name;
    }
}