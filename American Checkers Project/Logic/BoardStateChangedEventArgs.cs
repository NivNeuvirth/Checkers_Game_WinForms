using System;

namespace Logic
{
    public class BoardStateChangedEventArgs : EventArgs
    {
        private readonly Move r_ExecutedMove;

        public BoardStateChangedEventArgs(Move i_ExecutedMove)
        {
            r_ExecutedMove = i_ExecutedMove;
        }

        public Move ExecutedMove
        {
            get
            {
                return r_ExecutedMove;
            }
        }
    }
}