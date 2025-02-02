using System.Drawing;

namespace Logic
{
    public class Move
    {
        private readonly int r_StartRow;
        private readonly int r_StartCol;
        private readonly int r_EndRow;
        private readonly int r_EndCol;
        private Point m_CapturePosition;
        private bool m_IsCaptureMove = false;

        public Move() { }

        public Move(int i_StartRow, int i_StartCol, int i_EndRow, int i_EndCol)
        {
            r_StartRow = i_StartRow;
            r_StartCol = i_StartCol;
            r_EndRow = i_EndRow;
            r_EndCol = i_EndCol;
        }

        public int StartRow
        {
            get
            {
                return r_StartRow;
            }
        }

        public int StartCol
        {
            get
            {
                return r_StartCol;
            }
        }

        public int EndRow
        {
            get
            {
                return r_EndRow;
            }
        }

        public int EndCol
        {
            get
            {
                return r_EndCol;
            }
        }

        public Point FromPosition
        {
            get
            {
                return new Point(r_StartRow, r_StartCol);
            }
        }

        public Point ToPosition
        {
            get
            {
                return new Point(r_EndRow, r_EndCol);
            }
        }

        public Point CapturePosition
        {
            get
            {
                return m_CapturePosition;
            }

            set
            {
                m_CapturePosition = value;
            }
        }

        public bool IsACaptureMove
        {
            get
            {
                return m_IsCaptureMove;
            }

            set
            {
                m_IsCaptureMove = value;
            }
        }
    }
}