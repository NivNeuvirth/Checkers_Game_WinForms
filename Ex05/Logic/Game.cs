using System;
using static Logic.MoveRules;
using Logic.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Logic
{
    public class Game
    {
        public Board m_Board;
        public Player m_Player1;
        public Player m_Player2;

        public void InitializeGame(string i_Player1Name, string i_Player2Name, ePlayerType i_Player2Type, int i_BoardSize)
        {
            m_Player1 = new Player(i_Player1Name, ePlayerType.Human, eCheckerSymbol.WhiteChecker);
            m_Player2 = new Player(i_Player2Name, i_Player2Type, eCheckerSymbol.BlackChecker);
            m_Board = new Board(i_BoardSize, m_Player1, m_Player2);
        }

        private bool isPlayersChecker(Player i_Player, int i_Row, int i_Col)
        {
            Checker checker = m_Board.GetCheckerAt(i_Row, i_Col);

            return checker != null && i_Player.m_Checkers.Contains(checker);
        }

        private bool isValidDirection(Checker i_Checker, int i_TargetRow, int i_TargetCol)
        {
            bool isValid = false;
            int rowDelta = i_TargetRow - i_Checker.m_X;
            int colDelta = i_TargetCol - i_Checker.m_Y;
            int normalizedRowDelta = rowDelta > 0 ? 1 : -1;
            int normalizedColDelta = colDelta > 0 ? 1 : -1;
            Direction[] allowedDirections = GetAllowedDirections(i_Checker);

            foreach (var direction in allowedDirections)
            {
                if (normalizedRowDelta == direction.RowDelta &&
                    Math.Abs(normalizedColDelta) == Math.Abs(direction.ColDelta))
                {
                    isValid = true;
                }
            }

            return isValid;
        }

        public bool IsValidMove(Player i_Player, Move i_Move)
        {
            bool isValid = true;

            if (!isPlayersChecker(i_Player, i_Move.m_StartRow, i_Move.m_StartCol))
            {
                isValid = false;
            }

            if (!m_Board.IsEmpty(i_Move.m_EndRow, i_Move.m_EndCol))
            {
                isValid = false;
            }

            Checker checker = m_Board.GetCheckerAt(i_Move.m_StartRow, i_Move.m_StartCol);
            int rowDistance = Math.Abs(i_Move.m_EndRow - i_Move.m_StartRow);
            int colDistance = Math.Abs(i_Move.m_EndCol - i_Move.m_StartCol);
            bool isNormalMove = rowDistance == 1 && colDistance == 1;
            bool isCaptureMove = rowDistance == 2 && colDistance == 2;

            if (!isNormalMove && !isCaptureMove)
            {
                isValid = false;
            }

            if (isCaptureMove)
            {
                int capturedRow = (i_Move.m_StartRow + i_Move.m_EndRow) / 2;
                int capturedCol = (i_Move.m_StartCol + i_Move.m_EndCol) / 2;

                Checker capturedChecker = m_Board.GetCheckerAt(capturedRow, capturedCol);

                if (capturedChecker == null || !isOpponentChecker(i_Player.m_Checkers[0], capturedChecker))
                {
                    isValid = false;
                }
            }

            if (!isValidDirection(checker, i_Move.m_EndRow, i_Move.m_EndCol))
            {
                isValid = false;
            }

            return isValid;
        }

        private bool isOpponentChecker(Checker i_PlayersChecker, Checker i_Checker)
        {
            bool isOpponents = true;
            eCheckerSymbol playerSymbol = i_PlayersChecker.m_CheckerSymbol;
            eCheckerSymbol checkerSymbol = i_Checker.m_CheckerSymbol;

            bool isBothBlack = (playerSymbol == eCheckerSymbol.BlackChecker || playerSymbol == eCheckerSymbol.BlackKing) &&
                      (checkerSymbol == eCheckerSymbol.BlackChecker || checkerSymbol == eCheckerSymbol.BlackKing);

            bool isBothWhite = (playerSymbol == eCheckerSymbol.WhiteChecker || playerSymbol == eCheckerSymbol.WhiteKing) &&
                               (checkerSymbol == eCheckerSymbol.WhiteChecker || checkerSymbol == eCheckerSymbol.WhiteKing);

            if (isBothBlack || isBothWhite)
            {
                isOpponents = false;
            }
            return isOpponents;
        }

        private List<Move> getPossibleMovesForChecker(Checker i_checker, Player i_currentPlayer)
        {
            var possibleMoves = new List<Move>();
            Direction[] directions = GetAllowedDirections(i_checker);
            bool allowCapture;

            foreach (var direction in directions)
            {
                int normalRow = i_checker.m_X + direction.RowDelta;
                int normalCol = i_checker.m_Y + direction.ColDelta;

                if (isValidPosition(normalRow, normalCol) && m_Board.IsEmpty(normalRow, normalCol))
                {
                    var normalMove = new Move(i_checker.m_X, i_checker.m_Y, normalRow, normalCol);
                    allowCapture = false;
                    if (IsValidMoveDistance(normalMove, allowCapture))
                    {
                        possibleMoves.Add(normalMove);
                    }
                }

                int captureRow = i_checker.m_X + 2 * direction.RowDelta;
                int captureCol = i_checker.m_Y + 2 * direction.ColDelta;

                if (isValidPosition(captureRow, captureCol) && m_Board.IsEmpty(captureRow, captureCol))
                {
                    int jumpedRow = i_checker.m_X + direction.RowDelta;
                    int jumpedCol = i_checker.m_Y + direction.ColDelta;
                    Checker jumpedChecker = m_Board.GetCheckerAt(jumpedRow, jumpedCol);

                    if (jumpedChecker != null && isOpponentChecker(i_currentPlayer.m_Checkers[0], jumpedChecker))
                    {
                        var captureMove = new Move(i_checker.m_X, i_checker.m_Y, captureRow, captureCol);
                        allowCapture = true;

                        if (IsValidMoveDistance(captureMove, allowCapture))
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
            Checker movingChecker = m_Board.GetCheckerAt(i_Move.m_StartRow, i_Move.m_StartCol);

            if (Math.Abs(i_Move.m_EndRow - i_Move.m_StartRow) == 2)
            {
                int capturedRow = (i_Move.m_StartRow + i_Move.m_EndRow) / 2;
                int capturedCol = (i_Move.m_StartCol + i_Move.m_EndCol) / 2;
                Checker capturedChecker = m_Board.GetCheckerAt(capturedRow, capturedCol);
                Player opponentPlayer = i_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;

                if (capturedChecker != null)
                {
                    opponentPlayer.RemoveChecker(capturedChecker);
                }

                m_Board.RemoveChecker(capturedRow, capturedCol);
            }

            if (shouldPromoteToKing(movingChecker, i_Move.m_EndRow))
            {
                promoteToKing(movingChecker);
            }

            m_Board.MoveChecker(i_Move);
        }

        private bool shouldPromoteToKing(Checker i_Checker, int i_EndRow)
        {
            bool promotion = false;

            if (i_Checker.m_CheckerSymbol == eCheckerSymbol.WhiteChecker && i_EndRow == m_Board.m_Size - 1)
            {
                promotion = true;
            }

            if (i_Checker.m_CheckerSymbol == eCheckerSymbol.BlackChecker && i_EndRow == 0)
            {
                promotion = true;
            }
            return promotion;
        }

        private void promoteToKing(Checker i_Checker)
        {
            i_Checker.m_CheckerType = eCheckerType.King;

            if (i_Checker.m_CheckerSymbol == eCheckerSymbol.WhiteChecker)
            {
                i_Checker.m_CheckerSymbol = eCheckerSymbol.WhiteKing;
            }
            else if (i_Checker.m_CheckerSymbol == eCheckerSymbol.BlackChecker)
            {
                i_Checker.m_CheckerSymbol = eCheckerSymbol.BlackKing;
            }
        }

        public Move ProcessComputerTurn(Player i_ComputerPlayer)
        {
            List<Move> validMoves = new List<Move>();
            Move selectedMove = null;

            foreach (Checker checker in i_ComputerPlayer.m_Checkers)
            {
                validMoves.AddRange(getPossibleMovesForChecker(checker, i_ComputerPlayer));
            }

            List<Move> captureMoves = GetPossibleCapturesForPlayer(i_ComputerPlayer);

            if (captureMoves.Count > 0)
            {
                Random random = new Random();
                Move selectedCaptureMove = captureMoves[random.Next(captureMoves.Count)];
                selectedMove = selectedCaptureMove;
            }
            else
            {
                List<Move> normalMoves = validMoves
                    .Where(m => Math.Abs(m.m_EndRow - m.m_StartRow) == 1)
                    .ToList();

                if (normalMoves.Count > 0)
                {
                    Random random = new Random();
                    Move selectedNormalMove = normalMoves[random.Next(normalMoves.Count)];
                    selectedMove = selectedNormalMove;
                }
            }

            return selectedMove;
        }

        private bool isValidPosition(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < m_Board.m_Size && i_Col >= 0 && i_Col < m_Board.m_Size;
        }

        public bool CheckGameOver()
        {
            bool noCheckersLeft = m_Player1.m_Checkers.Count == 0 || m_Player2.m_Checkers.Count == 0;
            bool noPossibleMoves = !GetPossibleMovesForPlayer(m_Player1) || !GetPossibleMovesForPlayer(m_Player2);

            return noCheckersLeft || noPossibleMoves;
        }

        public bool GetPossibleMovesForPlayer(Player i_player)
        {
            bool possibleMoves = false;
            foreach (Checker checker in i_player.m_Checkers)
            {
                if (getPossibleMovesForChecker(checker, i_player).Count > 0)
                {
                    possibleMoves = true;
                }
            }
            return possibleMoves;
        }

        public Player switchPlayer(Player i_CurrentPlayer)
        {
            return i_CurrentPlayer = i_CurrentPlayer == m_Player1 ? m_Player2 : m_Player1;
        }

        //public void ExecuteCaptures(List<Move> i_Captures, Player i_CurrentPlayer)
        //{
        //    Move chosenCapture = null;

        //    while (chosenCapture == null)
        //    {
        //        chosenCapture = GameUI.ChooseFirstCaptureMove(ref i_Captures, i_CurrentPlayer);
        //    }

        //    ExecuteMove(chosenCapture, i_CurrentPlayer);
        //    RemoveMoveFromList(chosenCapture, i_Captures);
        //    Checker currentChecker = m_Board.GetCheckerAt(chosenCapture.m_EndRow, chosenCapture.m_EndCol);
            
        //    while (true)
        //    {
        //        if (i_Captures.Count < 1)
        //        {
        //            switchPlayer(i_CurrentPlayer);
        //            break;
        //        }

        //        Move nextCapture = GameUI.ChooseNextCaptureMove(i_Captures, currentChecker);

        //        if (nextCapture == null)
        //        {
        //            continue;
        //        }

        //        ExecuteMove(nextCapture, i_CurrentPlayer);
        //        currentChecker = m_Board.GetCheckerAt(nextCapture.m_EndRow, nextCapture.m_EndCol);         
        //    }    
        //}

        public List<Move> GetPossibleCapturesForPlayer(Player i_CurrentPlayer)
        {
            List<Move> captures = new List<Move>();
            foreach (Checker checker in i_CurrentPlayer.m_Checkers)
            {
                List<Move> moves = getPossibleMovesForChecker(checker, i_CurrentPlayer);
                foreach (Move move in moves)
                {
                    if (Math.Abs(move.m_EndRow - move.m_StartRow) == 2)
                    {
                        captures.Add(move);
                    }
                }
            }

            return captures;
        }

        public void RemoveMoveFromList(Move i_Move, List<Move> i_MoveList)
        {
            for (int i = i_MoveList.Count - 1; i >= 0; i--)
            {
                if (isEqualMoves(i_MoveList[i], i_Move))
                {
                    i_MoveList.RemoveAt(i);
                }
            }
        }

        private bool isEqualMoves(Move i_Move1, Move i_Move2)
        {
            return i_Move1.m_StartRow == i_Move2.m_StartRow && i_Move1.m_StartCol == i_Move2.m_StartCol &&
                i_Move1.m_EndRow == i_Move2.m_EndRow && i_Move1.m_EndCol == i_Move2.m_EndCol;
        }

        public void CalculateScore(Player i_WinnerPlayer)
        {
            int roundScore = 0;

            foreach (var checker in i_WinnerPlayer.m_Checkers)
            {
                if (checker.m_CheckerType == eCheckerType.King)
                {
                    roundScore += 4;
                }
                else
                {
                    roundScore += 1;
                }
            }

            i_WinnerPlayer.m_Score += roundScore;
        }

        public void InitializeNewRound(Player player1, Player player2, ePlayerType player2Type, int boardSize)
        {
            m_Player1 = player1;
            m_Player2 = player2;
            m_Board = new Board(boardSize, m_Player1, m_Player2);
            m_Player1.m_Checkers.Clear();
            m_Player2.m_Checkers.Clear();
            m_Board.InitializeBoard(m_Player1, m_Player2);
        }
    }
}