using System;
using Logic.Enums;

namespace Logic
{
    public class Move
    {
        public int m_StartRow;
        public int m_StartCol;
        public int m_EndRow;
        public int m_EndCol;

        public Move(int i_startRow, int i_startCol, int i_endRow, int i_endCol)
        {
            m_StartRow = i_startRow;
            m_StartCol = i_startCol;
            m_EndRow = i_endRow;
            m_EndCol = i_endCol;
        }
    }
}
