using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public Button ButtonDone
        {
            get
            {
                return button1;
            }
        }

        public string Player1Name
        {
            get
            {
                return textBox1.Text;
            }

            set
            {
                textBox1.Text = value;
            }
        }

        public string Player2Name
        {
            get
            {
                return textBox2.Text;
            }

            set
            {
                textBox2.Text = value;
            }
        }

        public bool IsPlayer2PC
        {
            get
            {
                return !checkBox1.Checked;
            }
        }

        public int BoardSize
        {
            get
            {
                int boardSize = 0;

                if (radioButton1.Checked)
                {
                    boardSize = 6;
                }
                else if (radioButton2.Checked)
                {
                    boardSize = 8;
                }
                else
                {
                    boardSize = 10;
                }

                return boardSize;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("You must fill the players names");
            }
            else
            {
                this.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            textBox2.Enabled = textBox2.Enabled == true ? false : true;

            if (!textBox2.Enabled)
            {
                this.textBox2.BackColor = Color.White;
                this.textBox2.Text = string.Empty;
            }
            else
            {
                this.textBox2.BackColor = System.Drawing.SystemColors.MenuBar;
                this.textBox2.Text = "[Computer]";
            }
        }
    }
}
