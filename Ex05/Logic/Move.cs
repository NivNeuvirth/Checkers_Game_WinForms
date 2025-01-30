using System;
using System.Drawing;
using Logic.Enums;

namespace Logic
{
    public class Move
    {
        public int m_StartRow;
        public int m_StartCol;
        public int m_EndRow;
        public int m_EndCol;
        public Point m_Eaten;
        private bool m_isSkipMove = false;

        public Move()
        {
        }

        public Move(int i_startRow, int i_startCol, int i_endRow, int i_endCol)
        {
            m_StartRow = i_startRow;
            m_StartCol = i_startCol;
            m_EndRow = i_endRow;
            m_EndCol = i_endCol;
        }

        public Point From
        {
            get
            {
                return new Point(m_StartRow, m_StartCol);
            }
        }

        public Point To
        {
            get
            {
                return new Point(m_EndRow, m_EndCol);
            }
        }

        public Point Eaten
        {
            get
            {
                return m_Eaten;
            }

            set
            {
                m_Eaten = value;
            }
        }

        public bool IsSkipMove
        {
            get
            {
                return m_isSkipMove;
            }

            set
            {
                m_isSkipMove = value;
            }
        }
    }
}
