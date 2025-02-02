using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace UI
{
    public partial class FormGameWindow : Form
    {
        public event EventHandler GameSetupCompleted;
        public event EventHandler MoveExecuted;
        public event EventHandler MessageBoxResponse;
        private readonly FormGameSetup r_FormGameSetup = new FormGameSetup();
        private readonly Label r_LabelPlayer1 = new Label();
        private readonly Label r_LabelPlayer2 = new Label();
        private CheckerPictureBox[,] m_CheckersPictureBoxCells;
        private CheckerPictureBox m_CheckerPictureBoxSelected;
        private bool m_IsAdditionalPictureBoxSelected = false;

        public FormGameWindow()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.Manual;
            int centerX = (Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2;
            this.Location = new Point(centerX, 0);
        }

        protected virtual void OnGameSetupCompleted(SetupCompletedEventArgs gsc)
        {
            if (GameSetupCompleted != null)
            {
                GameSetupCompleted(this, gsc);
            }
        }

        protected virtual void OnMoveExecuted(MoveExecutedEventArgs me)
        {
            if (MoveExecuted != null)
            {
                MoveExecuted(this, me);
            }
        }

        private void OnMessageBoxResponse(MessageBoxResponseEventArgs mbr)
        {
            if (MessageBoxResponse != null)
            {
                MessageBoxResponse(this, mbr);
            }
        }

        public void ShowYesOrNoMessageBox(string i_Message, string i_Caption)
        {
            DialogResult result = MessageBox.Show(i_Message, i_Caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            bool userRespondedYes = result == DialogResult.Yes;

            MessageBoxResponseEventArgs responseEventArgs = new MessageBoxResponseEventArgs(userRespondedYes);
            OnMessageBoxResponse(responseEventArgs);
        }

        public void ResetBoard()
        {
            foreach (CheckerPictureBox checker in m_CheckersPictureBoxCells)
            {
                checker.BackColor = Color.White;
                checker.Image = null;
            }
        }

        public void PlaceCheckersOnBoard(List<Point> i_PointsToPlace, Image i_CheckerResourceImage)
        {
            foreach (Point point in i_PointsToPlace)
            {
                assignImageToPictureBox(point, i_CheckerResourceImage);
            }

            ActivatePictureBoxes(i_PointsToPlace);
        }

        public void ActivatePictureBoxes(List<Point> i_PictureBoxesLocationToActivate)
        {
            foreach (Point location in i_PictureBoxesLocationToActivate)
            {
                CheckerPictureBox pictureBox = m_CheckersPictureBoxCells[location.X, location.Y];
                pictureBox.Enabled = true;
                pictureBox.BackColor = Color.Gray;
                pictureBox.IsCheckerPointAvailable = true;
            }
        }

        private void assignImageToPictureBox(Point i_Point, Image i_CheckerResourceImage)
        {
            CheckerPictureBox pictureBox = m_CheckersPictureBoxCells[i_Point.X, i_Point.Y];
            pictureBox.Image = i_CheckerResourceImage;
            pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        public void DeactivatePictureBoxes(List<Point> i_PictureBoxes)
        {
            foreach (Point point in i_PictureBoxes)
            {
                CheckerPictureBox pictureBox = m_CheckersPictureBoxCells[point.X, point.Y];
                pictureBox.Enabled = false;
                pictureBox.IsCheckerPointAvailable = false;
            }
        }

        public void r_FormGameSetup_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!r_FormGameSetup.m_IsDoneButtonClicked)
            {
                MessageBox.Show("Goodbye, and thank you!", "Damka", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                SetupCompletedEventArgs df = new SetupCompletedEventArgs(
                    r_FormGameSetup.Player1Name,
                    r_FormGameSetup.Player2Name,
                    r_FormGameSetup.BoardSize,
                    r_FormGameSetup.IsPlayer2Computer);

                m_CheckersPictureBoxCells = new CheckerPictureBox[r_FormGameSetup.BoardSize, r_FormGameSetup.BoardSize];
                adjustFormSizeForBoard();
                initializeCheckersPictureBoxCells();
                setPlayersLabelsAndScore();
                OnGameSetupCompleted(df);
            }
        }

        private void adjustFormSizeForBoard()
        {
            this.Size = new Size(
                (r_FormGameSetup.BoardSize * GameUiFormBoardSettings.k_CellWidth) + GameUiFormBoardSettings.k_WindowWidthOffset,
                (r_FormGameSetup.BoardSize * GameUiFormBoardSettings.k_CellHeight) + GameUiFormBoardSettings.k_WindowHeightOffset
            );
        }

        private void initializeCheckersPictureBoxCells()
        {
            bool isANewLine = false;
            bool isTheFirstCheckersPictureBoxCell = true;
            PictureBox lastCheckerPictureBoxCell = new PictureBox();

            for (int i = 0; i < r_FormGameSetup.BoardSize; i++)
            {
                for (int j = 0; j < r_FormGameSetup.BoardSize; j++)
                {
                    CheckerPictureBox currentCheckerPictureBoxCell = new CheckerPictureBox(i, j);
                    placeCheckerPictureBox(currentCheckerPictureBoxCell, isANewLine, isTheFirstCheckersPictureBoxCell, lastCheckerPictureBoxCell);
                    configureCheckerPictureBox(currentCheckerPictureBoxCell);
                    m_CheckersPictureBoxCells[i, j] = currentCheckerPictureBoxCell;
                    this.Controls.Add(currentCheckerPictureBoxCell);
                    isANewLine = false;
                    isTheFirstCheckersPictureBoxCell = false;
                    lastCheckerPictureBoxCell = currentCheckerPictureBoxCell;
                }

                isANewLine = true;
            }
        }

        private void placeCheckerPictureBox(CheckerPictureBox i_Current, bool i_IsNewLine, bool i_IsFirstInRow, PictureBox i_Last)
        {
            Point pointToPlace;

            if (i_IsFirstInRow)
            {
                pointToPlace = new Point(GameUiFormBoardSettings.k_InitialCellX, GameUiFormBoardSettings.k_InitialCellY);
            }
            else
            {
                pointToPlace = i_Last.Location;

                if (i_IsNewLine)
                {
                    pointToPlace.X = GameUiFormBoardSettings.k_InitialCellX;
                    pointToPlace.Offset(0, i_Last.Height);
                }
                else
                {
                    pointToPlace.Offset(i_Last.Width, 0);
                }
            }
            i_Current.Location = pointToPlace;
        }

        private void configureCheckerPictureBox(CheckerPictureBox i_CurrentPictureBox)
        {
            setCheckerPictureBoxDimensions(i_CurrentPictureBox);
            i_CurrentPictureBox.Enabled = false;
            i_CurrentPictureBox.Click += pictureBox_Click;
        }

        private void setCheckerPictureBoxDimensions(CheckerPictureBox i_CurrentPictureBox)
        {
            i_CurrentPictureBox.Height = GameUiFormBoardSettings.k_CellHeight;
            i_CurrentPictureBox.Width = GameUiFormBoardSettings.k_CellWidth;
        }

        private void setPlayersLabelsAndScore()
        {
            int playerStartingScore = 0;

            setPlayersLabelLocationOnForm();
            setPlayerLabelsFontToBold();
            UpdateScoreOnBoard(playerStartingScore, playerStartingScore);
            this.Controls.Add(r_LabelPlayer1);
            this.Controls.Add(r_LabelPlayer2);
        }

        private void setPlayersLabelLocationOnForm()
        {
            CheckerPictureBox middlePictureBox = m_CheckersPictureBoxCells[0, (r_FormGameSetup.BoardSize / 2) - 1];
            int gamePictureBoxWidth = middlePictureBox.Width;
            int gamePictureBoxHeight = middlePictureBox.Height / 2;
            Point middle = middlePictureBox.Location;
            Point player1LabelLocation = middle, player2LabelLocation = middle;

            player1LabelLocation.Offset(-middlePictureBox.Width, -gamePictureBoxHeight);
            player2LabelLocation.Offset(middlePictureBox.Width, -gamePictureBoxHeight);
            r_LabelPlayer1.Location = player1LabelLocation;
            r_LabelPlayer1.AutoSize = true;
            r_LabelPlayer2.Location = player2LabelLocation;
            r_LabelPlayer2.AutoSize = true;
        }

        private void setPlayerLabelsFontToBold()
        {
            r_LabelPlayer1.Font = new Font(r_LabelPlayer1.Font, FontStyle.Bold);
            r_LabelPlayer2.Font = new Font(r_LabelPlayer2.Font, FontStyle.Bold);
        }

        public void UpdateScoreOnBoard(int i_Player1Score, int i_Player2Score)
        {
            r_LabelPlayer1.Text = $"{r_FormGameSetup.Player1Name}: {i_Player1Score}";
            r_LabelPlayer2.Text = $"{r_FormGameSetup.Player2Name}: {i_Player2Score}";
        }

        public void HighlightCurrentPlayer(string currentPlayerName)
        {
            r_LabelPlayer1.BackColor = (currentPlayerName == r_FormGameSetup.Player1Name) ? Color.LightSkyBlue : Color.Transparent;
            r_LabelPlayer2.BackColor = (currentPlayerName == r_FormGameSetup.Player2Name) ? Color.LightSkyBlue : Color.Transparent;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            CheckerPictureBox clickedPictureBox = sender as CheckerPictureBox;

            if (clickedPictureBox == null || !clickedPictureBox.IsCheckerPointAvailable)
            {
                resetSelection();
                return;
            }

            if (m_IsAdditionalPictureBoxSelected)
            {
                handleMove(clickedPictureBox);
            }
            else
            {
                selectCheckerPictureBox(clickedPictureBox);
            }
        }

        private void handleMove(CheckerPictureBox i_TargetPictureBox)
        {
            if (i_TargetPictureBox.Image == null)
            {
                OnMoveExecuted(new MoveExecutedEventArgs(m_CheckerPictureBoxSelected.CheckerPoint, i_TargetPictureBox.CheckerPoint));
                resetSelection();
            }
        }

        private void selectCheckerPictureBox(CheckerPictureBox i_SelectedPictureBox)
        {
            if (i_SelectedPictureBox.Image != null)
            {
                m_CheckerPictureBoxSelected = i_SelectedPictureBox;
                i_SelectedPictureBox.BorderStyle = BorderStyle.Fixed3D;
                i_SelectedPictureBox.IsCheckerPointAvailable = false;
                m_IsAdditionalPictureBoxSelected = true;
                i_SelectedPictureBox.BackColor = Color.DarkBlue;
            }
        }

        private void resetSelection()
        {
            m_IsAdditionalPictureBoxSelected = false;

            if (m_CheckerPictureBoxSelected != null)
            {
                m_CheckerPictureBoxSelected.BorderStyle = BorderStyle.FixedSingle;
                m_CheckerPictureBoxSelected.IsCheckerPointAvailable = true;
                m_CheckerPictureBoxSelected.BackColor = Color.Gray;
            }
        }

        public void RemoveCheckersFromBoard(List<Point> i_PointsToRemove)
        {
            foreach (Point point in i_PointsToRemove)
            {
                m_CheckersPictureBoxCells[point.X, point.Y].Image = null;
                m_CheckersPictureBoxCells[point.X, point.Y].BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FormGameWindow
            // 
            this.ClientSize = new System.Drawing.Size(278, 244);
            this.Name = "FormGameWindow";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            r_FormGameSetup.FormClosed += r_FormGameSetup_FormClosed;
            r_FormGameSetup.ShowDialog();
        }
    }
}