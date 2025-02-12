using System;
using System.Collections.Generic;
using System.Drawing;
using static Logic.MoveRules;
using Logic.Enums;
using System.Windows.Forms;

namespace Logic
{
    public class Game
    {
        private Board m_Board;
        private Player m_CurrentPlayer;
        private Player m_OtherPlayer;
        private Checker m_LatestCheckerPlaced;
        public EventHandler GameInitialized;
        public EventHandler GameFinished;
        public EventHandler BoardStateChanged;
        private static readonly Random sr_RandomMove = new Random();

        public void OnGameInitialized()
        {
            EventArgs e = new EventArgs();

            if(GameInitialized != null)
            {
                GameInitialized(this, e);
            }
        }

        public void OnBoardStateChanged(BoardStateChangedEventArgs bu)
        {
            if(BoardStateChanged != null)
            {
                BoardStateChanged(this, bu);
            }
        }

        public void OnGameFinished(GameFinishedEventArgs ge)
        {
            if(GameFinished != null)
            {
                GameFinished(this, ge);
            }
        }

        public void InitializeGame(
            string i_Player1Name,
            string i_Player2Name,
            ePlayerType i_Player2Type,
            eGameBoardSize i_BoardSize)
        {
            m_CurrentPlayer = new Player(i_Player1Name, ePlayerType.Human, eCheckerSymbol.WhiteChecker);
            m_OtherPlayer = new Player(i_Player2Name, i_Player2Type, eCheckerSymbol.BlackChecker);
            m_Board = new Board(i_BoardSize, m_CurrentPlayer, m_OtherPlayer);
            OnGameInitialized();
        }

        private bool isPlayersChecker(Player i_Player, int i_Row, int i_Col)
        {
            Checker checker = m_Board.GetCheckerAt(i_Row, i_Col);

            return checker != null && i_Player.Checkers.Contains(checker);
        }

        private bool isValidDirection(Checker i_Checker, int i_TargetRow, int i_TargetCol)
        {
            bool isValid = false;
            int rowDelta = i_TargetRow - i_Checker.LocationOnBoardX;
            int colDelta = i_TargetCol - i_Checker.LocationOnBoardY;
            int normalizedRowDelta = rowDelta > 0 ? 1 : -1;
            int normalizedColDelta = colDelta > 0 ? 1 : -1;
            Direction[] allowedDirections = GetAllowedDirections(i_Checker);

            foreach(Direction direction in allowedDirections)
            {
                if(normalizedRowDelta == direction.RowDelta
                   && Math.Abs(normalizedColDelta) == Math.Abs(direction.ColDelta))
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        public bool IsValidMove(Player i_Player, Move i_Move)
        {
            bool isValid = true;

            if(!isPlayersChecker(i_Player, i_Move.StartRow, i_Move.StartCol))
            {
                isValid = false;
            }

            if(!m_Board.IsEmpty(i_Move.EndRow, i_Move.EndCol))
            {
                isValid = false;
            }

            Checker checker = m_Board.GetCheckerAt(i_Move.StartRow, i_Move.StartCol);
            int rowDistance = Math.Abs(i_Move.EndRow - i_Move.StartRow);
            int colDistance = Math.Abs(i_Move.EndCol - i_Move.StartCol);
            bool isNormalMove = rowDistance == 1 && colDistance == 1;
            bool isCaptureMove = rowDistance == 2 && colDistance == 2;

            if(!isNormalMove && !isCaptureMove)
            {
                isValid = false;
            }

            if(isCaptureMove)
            {
                int capturedRow = (i_Move.StartRow + i_Move.EndRow) / 2;
                int capturedCol = (i_Move.StartCol + i_Move.EndCol) / 2;

                Checker capturedChecker = m_Board.GetCheckerAt(capturedRow, capturedCol);

                if(capturedChecker == null || !isOpponentChecker(i_Player.Checkers[0], capturedChecker))
                {
                    isValid = false;
                }
            }

            if(!isValidDirection(checker, i_Move.EndRow, i_Move.EndCol))
            {
                isValid = false;
            }

            return isValid;
        }

        private bool isOpponentChecker(Checker i_PlayersChecker, Checker i_Checker)
        {
            bool isOpponents = true;
            eCheckerSymbol playerSymbol = i_PlayersChecker.CheckerSymbol;
            eCheckerSymbol checkerSymbol = i_Checker.CheckerSymbol;

            bool isBothBlack = (playerSymbol == eCheckerSymbol.BlackChecker || playerSymbol == eCheckerSymbol.BlackKing)
                               && (checkerSymbol == eCheckerSymbol.BlackChecker
                                   || checkerSymbol == eCheckerSymbol.BlackKing);

            bool isBothWhite = (playerSymbol == eCheckerSymbol.WhiteChecker || playerSymbol == eCheckerSymbol.WhiteKing)
                               && (checkerSymbol == eCheckerSymbol.WhiteChecker
                                   || checkerSymbol == eCheckerSymbol.WhiteKing);

            if(isBothBlack || isBothWhite)
            {
                isOpponents = false;
            }

            return isOpponents;
        }

        private List<Move> getPossibleMovesForChecker(Checker i_Checker, Player i_CurrentPlayer)
        {
            var possibleMoves = new List<Move>();
            Direction[] directions = GetAllowedDirections(i_Checker);

            foreach(var direction in directions)
            {
                int normalRow = i_Checker.LocationOnBoardX + direction.RowDelta;
                int normalCol = i_Checker.LocationOnBoardY + direction.ColDelta;

                bool allowCapture;
                if(isValidPosition(normalRow, normalCol) && m_Board.IsEmpty(normalRow, normalCol))
                {
                    var normalMove = new Move(
                        i_Checker.LocationOnBoardX,
                        i_Checker.LocationOnBoardY,
                        normalRow,
                        normalCol);
                    allowCapture = false;
                    if(IsValidMoveDistance(normalMove, allowCapture))
                    {
                        possibleMoves.Add(normalMove);
                    }
                }

                int captureRow = i_Checker.LocationOnBoardX + 2 * direction.RowDelta;
                int captureCol = i_Checker.LocationOnBoardY + 2 * direction.ColDelta;

                if(isValidPosition(captureRow, captureCol) && m_Board.IsEmpty(captureRow, captureCol))
                {
                    int jumpedRow = i_Checker.LocationOnBoardX + direction.RowDelta;
                    int jumpedCol = i_Checker.LocationOnBoardY + direction.ColDelta;
                    Checker jumpedChecker = m_Board.GetCheckerAt(jumpedRow, jumpedCol);

                    if(jumpedChecker != null && isOpponentChecker(i_CurrentPlayer.Checkers[0], jumpedChecker))
                    {
                        var captureMove = new Move(
                            i_Checker.LocationOnBoardX,
                            i_Checker.LocationOnBoardY,
                            captureRow,
                            captureCol);
                        allowCapture = true;

                        if(IsValidMoveDistance(captureMove, allowCapture))
                        {
                            possibleMoves.Add(captureMove);
                        }
                    }
                }
            }

            return possibleMoves;
        }

        public void ExecuteMove(Move i_Move, Player i_CurrentPlayer)
        {
            Checker movingChecker = m_Board.GetCheckerAt(i_Move.StartRow, i_Move.StartCol);
            m_LatestCheckerPlaced = movingChecker;

            bool isValidMove = handleCaptureMove(i_Move, i_CurrentPlayer);
            if (isValidMove)
            {
                m_Board.MoveChecker(i_Move);

                if (shouldPromoteToKing(movingChecker, i_Move.EndRow))
                {
                    promoteToKing(movingChecker);
                }

                OnBoardStateChanged(new BoardStateChangedEventArgs(i_Move));

                if (CheckGameOver())
                {
                    endGame(i_CurrentPlayer);
                }
                else
                {
                    bool hasAnotherCapture = i_Move.IsACaptureMove && hasAdditionalCapture(i_Move, i_CurrentPlayer);

                    if (!(i_CurrentPlayer.PlayerType == ePlayerType.Human && hasAnotherCapture))
                    {
                        SwitchPlayer();
                        OnBoardStateChanged(new BoardStateChangedEventArgs(i_Move));

                        if (m_CurrentPlayer.PlayerType == ePlayerType.Computer)
                        {
                            Move computerMove = ProcessComputerTurn(m_CurrentPlayer);
                            ExecuteMove(computerMove, m_CurrentPlayer);
                        }
                    }
                }
            }
        }

        private bool handleCaptureMove(Move i_Move, Player i_CurrentPlayer)
        {
            List<Move> captureMoves = GetPossibleCapturesForPlayer(i_CurrentPlayer);
            bool isValidMove = true;

            if(captureMoves.Count > 0)
            {
                bool isCaptureMove = Math.Abs(i_Move.EndRow - i_Move.StartRow) == 2;
                if(isCaptureMove)
                {
                    int capturedRow = (i_Move.StartRow + i_Move.EndRow) / 2;
                    int capturedCol = (i_Move.StartCol + i_Move.EndCol) / 2;
                    Checker capturedChecker = m_Board.GetCheckerAt(capturedRow, capturedCol);

                    if(capturedChecker != null)
                    {
                        m_OtherPlayer.RemoveChecker(capturedChecker);
                        m_Board.RemoveChecker(capturedRow, capturedCol);
                    }

                    i_Move.IsACaptureMove = true;
                    i_Move.CapturePosition = new Point(capturedRow, capturedCol);
                }
                else
                {
                    MessageBox.Show(
                        $"Capture Move available!{Environment.NewLine}You must capture first!",
                        "Invalid Move",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    isValidMove = false;
                }
            }

            return isValidMove;
        }

        private bool hasAdditionalCapture(Move i_Move, Player i_CurrentPlayer)
        {
            bool hasCapture = false;
            List<Move> captureMoves = GetPossibleCapturesForPlayer(i_CurrentPlayer);

            foreach(Move move in captureMoves)
            {
                if(move.StartRow == i_Move.EndRow && move.StartCol == i_Move.EndCol)
                {
                    hasCapture = true;
                    break;
                }
            }

            return hasCapture;
        }

        private void endGame(Player i_CurrentPlayer)
        {
            CalculateScore(i_CurrentPlayer);
            string gameOverMessage = $"{i_CurrentPlayer.PlayerName} Won!{Environment.NewLine}Another Round?";
            OnGameFinished(new GameFinishedEventArgs(gameOverMessage));
        }

        private bool shouldPromoteToKing(Checker i_Checker, int i_EndRow)
        {
            bool promotion = false;

            if(i_Checker.CheckerSymbol == eCheckerSymbol.WhiteChecker && i_EndRow == m_Board.Size - 1)
            {
                promotion = true;
            }

            if(i_Checker.CheckerSymbol == eCheckerSymbol.BlackChecker && i_EndRow == 0)
            {
                promotion = true;
            }

            return promotion;
        }

        private void promoteToKing(Checker i_Checker)
        {
            i_Checker.CheckerType = eCheckerType.King;

            if(i_Checker.CheckerSymbol == eCheckerSymbol.WhiteChecker)
            {
                i_Checker.CheckerSymbol = eCheckerSymbol.WhiteKing;
            }
            else if(i_Checker.CheckerSymbol == eCheckerSymbol.BlackChecker)
            {
                i_Checker.CheckerSymbol = eCheckerSymbol.BlackKing;
            }
        }

        public Move ProcessComputerTurn(Player i_ComputerPlayer)
        {
            List<Move> validMoves = new List<Move>();
            Move selectedMove = null;

            foreach(Checker checker in i_ComputerPlayer.Checkers)
            {
                validMoves.AddRange(getPossibleMovesForChecker(checker, i_ComputerPlayer));
            }

            List<Move> captureMoves = GetPossibleCapturesForPlayer(i_ComputerPlayer);

            if(captureMoves.Count > 0)
            {
                selectedMove = captureMoves[sr_RandomMove.Next(captureMoves.Count)];
            }
            else
            {
                List<Move> normalMoves = new List<Move>();

                foreach(Move move in validMoves)
                {
                    if(Math.Abs(move.EndRow - move.StartRow) == 1)
                    {
                        normalMoves.Add(move);
                    }
                }

                if(normalMoves.Count > 0)
                {
                    selectedMove = normalMoves[sr_RandomMove.Next(normalMoves.Count)];
                }
            }

            return selectedMove;
        }

        private bool isValidPosition(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < m_Board.Size && i_Col >= 0 && i_Col < m_Board.Size;
        }

        public bool CheckGameOver()
        {
            bool noCheckersLeft = m_CurrentPlayer.Checkers.Count == 0 || m_OtherPlayer.Checkers.Count == 0;
            bool noPossibleMoves =
                !GetPossibleMovesForPlayer(m_CurrentPlayer) || !GetPossibleMovesForPlayer(m_OtherPlayer);

            return noCheckersLeft || noPossibleMoves;
        }

        public bool GetPossibleMovesForPlayer(Player i_player)
        {
            bool possibleMoves = false;
            foreach(Checker checker in i_player.Checkers)
            {
                if(getPossibleMovesForChecker(checker, i_player).Count > 0)
                {
                    possibleMoves = true;
                }
            }

            return possibleMoves;
        }

        public void SwitchPlayer()
        {
            Player.Swap(ref m_CurrentPlayer, ref m_OtherPlayer);
        }

        public List<Move> GetPossibleCapturesForPlayer(Player i_CurrentPlayer)
        {
            List<Move> captures = new List<Move>();
            foreach(Checker checker in i_CurrentPlayer.Checkers)
            {
                List<Move> moves = getPossibleMovesForChecker(checker, i_CurrentPlayer);
                foreach(Move move in moves)
                {
                    if(Math.Abs(move.EndRow - move.StartRow) == 2)
                    {
                        captures.Add(move);
                    }
                }
            }

            return captures;
        }

        public void CalculateScore(Player i_WinnerPlayer)
        {
            int roundScore = 0;

            foreach (var checker in i_WinnerPlayer.Checkers)
            {
                if (checker.CheckerType == eCheckerType.King)
                {
                    roundScore += 4;
                }
                else
                {
                    roundScore += 1;
                }
            }

            i_WinnerPlayer.PlayerScore += roundScore;
        }

        public void InitializeNewRound(
            Player i_Player1,
            Player i_Player2,
            ePlayerType i_Player2Type,
            eGameBoardSize i_BoardSize)
        {
            if(m_CurrentPlayer.CheckerSymbol == eCheckerSymbol.WhiteChecker
               || m_CurrentPlayer.CheckerSymbol == eCheckerSymbol.WhiteKing)
            {
                m_CurrentPlayer = i_Player1;
                m_OtherPlayer = i_Player2;
            }
            else
            {
                m_CurrentPlayer = i_Player2;
                m_OtherPlayer = i_Player1;
            }

            m_Board = new Board(i_BoardSize, m_CurrentPlayer, m_OtherPlayer);
            m_CurrentPlayer.Checkers.Clear();
            m_OtherPlayer.Checkers.Clear();
            m_Board.InitializeBoard(m_CurrentPlayer, m_OtherPlayer);
        }

        public Checker LatestCheckerPlaced
        {
            get
            {
                return m_LatestCheckerPlaced;
            }
        }

        public Board Board
        {
            get
            {
                return m_Board;
            }
        }

        public Player CurrentPlayer
        {
            get
            {
                return m_CurrentPlayer;
            }
        }

        public Player OtherPlayer
        {
            get
            {
                return m_OtherPlayer;
            }
        }
    }
}