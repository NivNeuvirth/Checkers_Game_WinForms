using System;
using System.Drawing;
using System.Windows.Forms;

namespace UI
{
    public partial class FormGameSetup : Form
    {
        public bool m_IsDoneButtonClicked = false;

        public FormGameSetup()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            int centerX = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            this.Location = new Point(centerX, 0);
        }

        private void checkBoxPlayer2_CheckedChanged(object sender, EventArgs e)
        {
            bool isChecked = checkBoxPlayer2.Checked;
            textBoxPlayer2Name.Enabled = isChecked;
            textBoxPlayer2Name.Text = isChecked ? "" : "[Computer]";
            textBoxPlayer2Name.BackColor = isChecked ? Color.White : SystemColors.Control;
        }

        private void buttonDone_Click(object sender, EventArgs e)
        {
            string message = "Both player names are required to continue. Please fill them in.";

            if (string.IsNullOrWhiteSpace(Player1Name) || string.IsNullOrWhiteSpace(Player2Name))
            {
                MessageBox.Show(message);
            }
            else
            {
                m_IsDoneButtonClicked = true;
                this.Close();
            }
        }

        public int BoardSize
        {
            get
            {
                int boardSize = 0;

                if (radioButtonSmallBoardSize.Checked)
                {
                    boardSize = (int)eOptionsForGameBoardSize.Mini;
                }
                else if (radioButtonMediumBoardSize.Checked)
                {
                    boardSize = (int)eOptionsForGameBoardSize.Classic;
                }
                else
                {
                    boardSize = (int)eOptionsForGameBoardSize.Grand;
                }

                return boardSize;
            }
        }

        public string Player1Name
        {
            get
            {
                return textBoxPlayer1Name.Text;
            }

            set
            {
                textBoxPlayer1Name.Text = value;
            }
        }

        public string Player2Name
        {
            get
            {
                return textBoxPlayer2Name.Text;
            }

            set
            {
                textBoxPlayer2Name.Text = value;
            }
        }

        public bool IsPlayer2Computer
        {
            get
            {
                return !checkBoxPlayer2.Checked;
            }
        }

        private void FormGameSetup_Load(object sender, EventArgs e)
        {

        }
    }
}