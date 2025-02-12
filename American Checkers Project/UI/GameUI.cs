using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Logic;
using Logic.Enums;

namespace UI
{
    public class GameUI
    {
        private readonly FormGameWindow r_FormGameWindow = new FormGameWindow();
        private readonly Game r_CheckersGame = new Game();

        public void Play()
        {
            connectToLogicAndFormEvents();
            r_FormGameWindow.ShowDialog();
        }

        private void connectToLogicAndFormEvents()
        {
            r_CheckersGame.GameInitialized += r_CheckersGame_GameInitialized;
            r_CheckersGame.BoardStateChanged += r_CheckersGame_BoardStateChanged;
            r_CheckersGame.GameFinished += r_CheckersGame_GameFinished;
            r_FormGameWindow.GameSetupCompleted += r_FormGameWindow_GameSetupCompleted;
            r_FormGameWindow.MoveExecuted += r_FormGameWindow_MoveExecuted;
            r_FormGameWindow.MessageBoxResponse += r_FormGameWindow_MessageBoxResponse;
        }

        private void r_CheckersGame_GameInitialized(object sender, EventArgs e)
        {
            initializeGameVariables(out List<Point> player1GameToolsPoints, out List<Point> player2GameToolsPoints, out int player1Score, out int player2Score);
            resetAndInitializeBoard();
            placeCheckersOnBoard(player1GameToolsPoints, player2GameToolsPoints);
            deactivateOpponentCheckers(player2GameToolsPoints);
            updateScore(player1Score, player2Score);
            highlightPlayersTurn();
        }

        private void initializeGameVariables(out List<Point> o_Player1CheckersPoints, out List<Point> o_Player2CheckersPoints, out int o_Player1Score, out int o_Player2Score)
        {
            o_Player1CheckersPoints = new List<Point>();
            o_Player2CheckersPoints = new List<Point>();
            o_Player1Score = r_CheckersGame.CurrentPlayer.PlayerScore;
            o_Player2Score = r_CheckersGame.OtherPlayer.PlayerScore;
        }

        private void resetAndInitializeBoard()
        {
            r_FormGameWindow.ResetBoard();
            r_CheckersGame.InitializeNewRound(r_CheckersGame.CurrentPlayer, r_CheckersGame.OtherPlayer, r_CheckersGame.OtherPlayer.PlayerType, (eGameBoardSize)r_CheckersGame.Board.Size);
            activateCentralPlayableArea();
        }

        private void placeCheckersOnBoard(List<Point> i_Player1CheckersPoints, List<Point> i_Player2CheckersPoints)
        {
            Image player1CheckerImage = global::UI.Properties.Resources.blackChecker;
            Image player2CheckerImage = global::UI.Properties.Resources.redChecker;
            i_Player1CheckersPoints = getCheckerLocations(r_CheckersGame.CurrentPlayer.Checkers);
            i_Player2CheckersPoints = getCheckerLocations(r_CheckersGame.OtherPlayer.Checkers);
            r_FormGameWindow.PlaceCheckersOnBoard(i_Player1CheckersPoints, player1CheckerImage);
            r_FormGameWindow.PlaceCheckersOnBoard(i_Player2CheckersPoints, player2CheckerImage);
        }

        private List<Point> getCheckerLocations(List<Checker> i_PlayerCheckers)
        {
            List<Point> checkerLocations = new List<Point>();

            if (i_PlayerCheckers != null)
            {
                foreach (Checker checker in i_PlayerCheckers)
                {
                    checkerLocations.Add(checker.CheckerLocation);
                }
            }

            return checkerLocations;
        }

        private void deactivateOpponentCheckers(List<Point> i_Player2CheckersPoints)
        {
            r_FormGameWindow.DeactivatePictureBoxes(i_Player2CheckersPoints);
        }

        private void updateScore(int i_Player1Score, int i_Player2Score)
        {
            r_FormGameWindow.UpdateScoreOnBoard(i_Player1Score, i_Player2Score);
        }

        private void highlightPlayersTurn()
        {
            r_FormGameWindow.HighlightCurrentPlayer(r_CheckersGame.CurrentPlayer.PlayerName);
        }

        private void activateCentralPlayableArea()
        {
            List<Point> centralPlayableAreaPoints = new List<Point>();
            int startRowIndex = (r_CheckersGame.Board.Size / 2) - 1;
            int endRowIndex = (r_CheckersGame.Board.Size / 2) + 1;

            for (int row = startRowIndex; row < endRowIndex; row++)
            {
                for (int column = 0; column < r_CheckersGame.Board.Size; column++)
                {
                    bool isCentralPlayablePoint = (row % 2 == 0 && column % 2 != 0) || (row % 2 != 0 && column % 2 == 0);

                    if (isCentralPlayablePoint)
                    {
                        centralPlayableAreaPoints.Add(new Point(row, column));
                    }
                }
            }

            r_FormGameWindow.ActivatePictureBoxes(centralPlayableAreaPoints);
        }

        private void getCheckerImage(out Image o_CheckerImage)
        {
            eCheckerSymbol lastCheckerSymbol = r_CheckersGame.LatestCheckerPlaced.CheckerSymbol;

            switch (lastCheckerSymbol)
            {
                case eCheckerSymbol.WhiteChecker:
                    o_CheckerImage = global::UI.Properties.Resources.blackChecker;
                    break;
                case eCheckerSymbol.WhiteKing:
                    o_CheckerImage = global::UI.Properties.Resources.blackKing;
                    break;
                case eCheckerSymbol.BlackChecker:
                    o_CheckerImage = global::UI.Properties.Resources.redChecker;
                    break;
                case eCheckerSymbol.BlackKing:
                    o_CheckerImage = global::UI.Properties.Resources.redKing;
                    break;
                default:
                    o_CheckerImage = null;
                    break;
            }
        }

        private void updateBoardState(List<Point> i_AddPoints, List<Point> i_RemovePoints, List<Point> i_EnablePoints, List<Point> i_DisabledPoints)
        {
            getCheckerImage(out Image playerCheckerImage);
            highlightPlayersTurn();
            r_FormGameWindow.PlaceCheckersOnBoard(i_AddPoints, playerCheckerImage);
            r_FormGameWindow.RemoveCheckersFromBoard(i_RemovePoints);
            r_FormGameWindow.ActivatePictureBoxes(i_EnablePoints);
            r_FormGameWindow.DeactivatePictureBoxes(i_DisabledPoints);
        }

        private void r_FormGameWindow_GameSetupCompleted(object sender, EventArgs e)
        {
            SetupCompletedEventArgs sc = e as SetupCompletedEventArgs;

            r_CheckersGame.InitializeGame(sc.Player1Name, sc.Player2Name, sc.IsPlayer2AComputer, (eGameBoardSize)sc.CheckersBoardSize);
        }

        private void r_CheckersGame_BoardStateChanged(object sender, EventArgs e)
        {
            List<Point> addPoints = new List<Point>();
            List<Point> removePoints = new List<Point>();
            List<Point> disablePoints = new List<Point>();
            List<Point> enablePoints = new List<Point>();
            BoardStateChangedEventArgs bsc = e as BoardStateChangedEventArgs;

            addPoints.Add(bsc.ExecutedMove.ToPosition);
            removePoints.Add(bsc.ExecutedMove.FromPosition);
            disablePoints = getCheckerLocations(r_CheckersGame.OtherPlayer.Checkers);
            enablePoints = getCheckerLocations(r_CheckersGame.CurrentPlayer.Checkers);

            if (bsc.ExecutedMove.IsACaptureMove)
            {
                removePoints.Add(bsc.ExecutedMove.CapturePosition);
                enablePoints.Add(bsc.ExecutedMove.CapturePosition);
            }

            updateBoardState(addPoints, removePoints, enablePoints, disablePoints);
        }

        private void r_FormGameWindow_MoveExecuted(object sender, EventArgs e)
        {
            MoveExecutedEventArgs me = e as MoveExecutedEventArgs;
            Move moveToExecute = new Move(me.FromPosition.X, me.FromPosition.Y, me.ToPosition.X, me.ToPosition.Y);

            if (!r_CheckersGame.IsValidMove(r_CheckersGame.CurrentPlayer, moveToExecute))
            {
                MessageBox.Show("You are trying to move to an invalid Position", "Invalid Move", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            r_CheckersGame.ExecuteMove(moveToExecute, r_CheckersGame.CurrentPlayer);
        }

        private void r_CheckersGame_GameFinished(object sender, EventArgs e)
        {
            GameFinishedEventArgs gf = e as GameFinishedEventArgs;

            r_FormGameWindow.ShowYesOrNoMessageBox(gf.ResultMessage, "Another Round?");
        }

        private void r_FormGameWindow_MessageBoxResponse(object sender, EventArgs e)
        {
            MessageBoxResponseEventArgs mba = e as MessageBoxResponseEventArgs;

            if (mba.IsAPositiveResponse)
            {
                r_CheckersGame_GameInitialized(sender, e);
            }
            else
            {
                r_FormGameWindow.Close();
            }
        }

    }
}