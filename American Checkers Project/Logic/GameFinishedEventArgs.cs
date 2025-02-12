using System;

namespace Logic
{
    public class GameFinishedEventArgs : EventArgs
    {
        private readonly string r_ResultMessage;

        public GameFinishedEventArgs(string i_Message)
        {
            r_ResultMessage = i_Message;
        }

        public string ResultMessage
        {
            get
            {
                return r_ResultMessage;
            }
        }           
    }
}