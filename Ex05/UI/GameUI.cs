using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic;
using Logic.Enums;

namespace UI
{
    public class GameUI
    {
        private Form2 m_FormGame = new Form2();
        private Game m_CheckersGame = new Game();

        public void Run()
        {
            registerToLogicEvents();
            registerToFormEvents();
            m_FormGame.ShowDialog();
        }

        private void registerToLogicEvents()
        {
            m_CheckersGame.NewGameStarted += m_CheckersGame_NewGameStarted;
            m_CheckersGame.BoardUpdated += m_CheckersGame_BoardUpdated;
            m_CheckersGame.GameEnded += m_CheckersGame_GameEnded;
        }

        private void registerToFormEvents()
        {
            m_FormGame.GameDetailsFilled += m_FormGame_GameDetailsFilled;
            m_FormGame.MoveEnterd += m_FormGame_MoveEnterd;
            m_FormGame.MessageBoxAnswered += m_FormGame_MessageBoxAnswered; 
        }

        private void m_CheckersGame_NewGameStarted(object sender, EventArgs e)
        {
            List<Point> player1GameToolsPoints = new List<Point>();
            List<Point> player2GameToolsPoints = new List<Point>();
            int player1Score, player2Score;
            Image player1GameToolImage = global::UI.Properties.Resources.Player1Tool;
            Image player2GameToolImage = global::UI.Properties.Resources.Player2Tool;

            m_FormGame.ResetGameBoard();
            m_CheckersGame.InitializeNewRound(m_CheckersGame.m_CurrentPlayer, m_CheckersGame.m_OtherPlayer, m_CheckersGame.m_OtherPlayer.m_PlayerType, m_CheckersGame.m_Board.m_Size);
            player1GameToolsPoints = createPointsListFromPlayerGameTools(m_CheckersGame.m_CurrentPlayer.m_Checkers);
            player2GameToolsPoints = createPointsListFromPlayerGameTools(m_CheckersGame.m_OtherPlayer.m_Checkers);
            m_FormGame.AddGameToolsToGameBoard(player1GameToolsPoints, player1GameToolImage);
            m_FormGame.AddGameToolsToGameBoard(player2GameToolsPoints, player2GameToolImage);
            m_FormGame.DisableGamePictureBoxes(player2GameToolsPoints);
            enabledBufferZone();
            player1Score = m_CheckersGame.m_CurrentPlayer.m_Score;
            player2Score = m_CheckersGame.m_OtherPlayer.m_Score;
            m_FormGame.UpdateScoreBoard(player1Score, player2Score);
            markCurrentPlayer();
        }

        private List<Point> createPointsListFromPlayerGameTools(List<Checker> i_PlayerTools)
        {
            List<Point> result = new List<Point>();

            foreach (Checker current in i_PlayerTools)
            {
                result.Add(current.Location);
            }

            return result;
        }

        private void enabledBufferZone()
        {
            List<Point> enabledBufferZonePoints = new List<Point>();
            int startLoopIndex = (m_CheckersGame.m_Board.m_Size / 2) - 1;
            int endLoopIndex = (m_CheckersGame.m_Board.m_Size / 2) + 1;

            for (int i = startLoopIndex; i < endLoopIndex; i++)
            {
                for (int j = 0; j < m_CheckersGame.m_Board.m_Size; j++)
                {
                    if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        enabledBufferZonePoints.Add(new Point(i, j));
                    }
                }
            }

            m_FormGame.EnableGamePictureBoxes(enabledBufferZonePoints);
        }

        private void m_FormGame_GameDetailsFilled(object sender, EventArgs e)
        {
            DetailsFilledEventArgs df = e as DetailsFilledEventArgs;

            m_CheckersGame.InitializeGame(df.Player1Name, df.Player2Name, df.IsPlayer2PC, df.GameBoardSize);
        }

        private void markCurrentPlayer()
        {
            m_FormGame.MarkCurrentPlayerLabel(m_CheckersGame.m_CurrentPlayer.m_Name);
        }

        /////// update ///////

        private void m_CheckersGame_BoardUpdated(object sender, EventArgs e)
        {
            BoardUpdatedEventArgs bu = e as BoardUpdatedEventArgs;
            List<Point> pointsToAddOnBoard = new List<Point>();
            List<Point> pointsToEraseOnBoard = new List<Point>();
            List<Point> pointsToEnableOnBoard;
            List<Point> pointsToDisableOnBoard;
            Image playerToolImage;

            pointsToAddOnBoard.Add(bu.LastMove.To);
            pointsToEraseOnBoard.Add(bu.LastMove.From);
            pointsToEnableOnBoard = createPointsListFromPlayerGameTools(m_CheckersGame.m_CurrentPlayer.m_Checkers);
            pointsToDisableOnBoard = createPointsListFromPlayerGameTools(m_CheckersGame.m_OtherPlayer.m_Checkers);
            if (bu.LastMove.IsSkipMove)
            {
                pointsToEraseOnBoard.Add(bu.LastMove.Eaten);
                pointsToEnableOnBoard.Add(bu.LastMove.Eaten);
            }

            getImagePlayerTool(out playerToolImage);
            markCurrentPlayer();
            m_FormGame.AddGameToolsToGameBoard(pointsToAddOnBoard, playerToolImage);
            m_FormGame.EraseGameToolsFromGameBoard(pointsToEraseOnBoard);
            m_FormGame.EnableGamePictureBoxes(pointsToEnableOnBoard);
            m_FormGame.DisableGamePictureBoxes(pointsToDisableOnBoard);
        }

        private void getImagePlayerTool(out Image o_ImagePlayerGameTool)
        {
            eCheckerSymbol playerToolSign = m_CheckersGame.LastCheckerPlaced.m_CheckerSymbol;

            if (playerToolSign == eCheckerSymbol.WhiteChecker)
            {
                o_ImagePlayerGameTool = global::UI.Properties.Resources.Player1Tool;
            }
            else if (playerToolSign == eCheckerSymbol.WhiteKing)
            {
                o_ImagePlayerGameTool = global::UI.Properties.Resources.Player1Crown;
            }
            else if (playerToolSign == eCheckerSymbol.BlackChecker)
            {
                o_ImagePlayerGameTool = global::UI.Properties.Resources.Player2Tool;
            }
            else
            {
                o_ImagePlayerGameTool = global::UI.Properties.Resources.Player2Crown;
            }
        }

        private void m_FormGame_MoveEnterd(object sender, EventArgs e)
        {
            MoveEnterdEventArgs me = e as MoveEnterdEventArgs;
            Move newMove = new Move(me.From.X,me.From.Y, me.To.X, me.To.Y);
            string errorMessage;
            bool isLegalMove;

            isLegalMove = m_CheckersGame.IsValidMove(m_CheckersGame.m_CurrentPlayer, newMove);
            //isLegalMove = m_CheckersGame.ExecuteMove(newMove, m_CheckersGame.m_CurrentPlayer);
            if (!isLegalMove)
            {
                m_FormGame.ErrorMessageBox("Invalid move position");
            }
            else
            {
                m_CheckersGame.ExecuteMove(newMove, m_CheckersGame.m_CurrentPlayer);
                //m_CheckersGame.SwitchPlayer();
            }

        }

        ////// ended //////


        private void m_CheckersGame_GameEnded(object sender, EventArgs e)
        {
            GameEndedEventArgs ge = e as GameEndedEventArgs;

            m_FormGame.CreateYesNoMessageBox(ge.Message, "Damka");
        }

        private void m_FormGame_MessageBoxAnswered(object sender, EventArgs e)
        {
            MessageBoxAnsweredEventArgs mba = e as MessageBoxAnsweredEventArgs;

            if (mba.IsAnsweredYes)
            {
                m_CheckersGame_NewGameStarted(sender, e);
            }
            else
            {
                m_FormGame.Close();
            }
        }

    }
}
