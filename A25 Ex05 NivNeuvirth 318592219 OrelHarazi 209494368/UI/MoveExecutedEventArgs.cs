using System;
using System.Drawing;

namespace UI
{
    public class MoveExecutedEventArgs : EventArgs
    {
        private readonly Point r_FromPosition = new Point();
        private readonly Point r_ToPosition = new Point();

        public MoveExecutedEventArgs(Point i_FromPosition, Point i_ToPosition)
        {
            r_FromPosition = i_FromPosition;
            r_ToPosition = i_ToPosition;
        }

        public Point FromPosition
        {
            get
            {
                return r_FromPosition;
            }
        }

        public Point ToPosition
        {
            get
            {
                return r_ToPosition;
            }
        }
    }
}