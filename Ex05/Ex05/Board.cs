using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CheckersProject.Logic.Enums;

namespace CheckersProject.Logic
{
    public class Board
    {
        private Checker?[,] m_Board;
        public readonly int m_Size;

        public Board(int i_Size, Player i_Player1, Player i_Player2)
        {
            m_Size = i_Size;
            m_Board = new Checker[i_Size, i_Size];
            InitializeBoard(i_Player1, i_Player2);
        }

        public void InitializeBoard(Player i_Player1, Player i_Player2)
        {
            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    if ((i + j) % 2 != 0 && i < m_Size / 2 - 1)
                    {
                        Checker checker = new Checker(i, j, eCheckerType.Soldier, eCheckerSymbol.WhiteChecker);
                        m_Board[i, j] = checker;
                        i_Player1.m_Checkers.Add(checker);
                    }
                    else if ((i + j) % 2 != 0 && i > m_Size / 2)
                    {
                        Checker checker = new Checker(i, j, eCheckerType.Soldier, eCheckerSymbol.BlackChecker);
                        m_Board[i, j] = checker;
                        i_Player2.m_Checkers.Add(checker);
                    }
                    else
                    {
                        m_Board[i, j] = null;
                    }
                }
            }
        }

        public Checker?[,] GetBoardState()
        {
            return m_Board;
        }

        public bool IsInsideBoard(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < m_Board.Length && i_Col >= 0 && i_Col < m_Board.Length;
        }

        public Checker? GetCheckerAt(int i_Row, int i_Col)
        {
            return IsInsideBoard(i_Row, i_Col) ? m_Board[i_Row, i_Col] : null;
        }

        public bool IsEmpty(int i_Row, int i_Col)
        {
            return IsInsideBoard(i_Row, i_Col) && m_Board[i_Row, i_Col] == null;
        }

        public void MoveChecker(Move i_Move)
        {
            Checker? movingChecker = m_Board[i_Move.m_StartRow, i_Move.m_StartCol];

            movingChecker.m_X = i_Move.m_EndRow;
            movingChecker.m_Y = i_Move.m_EndCol;
            m_Board[i_Move.m_EndRow, i_Move.m_EndCol] = movingChecker;
            m_Board[i_Move.m_StartRow, i_Move.m_StartCol] = null;
        }

        public void RemoveChecker(int i_Row, int i_Col)
        {
            m_Board[i_Row, i_Col] = null;
        }
    }
}
