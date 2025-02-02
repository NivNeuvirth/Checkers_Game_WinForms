using System;

namespace UI
{
    public class MessageBoxResponseEventArgs : EventArgs
    {
        private readonly bool r_IsAPositiveResponse;

        public MessageBoxResponseEventArgs(bool i_IsAPositiveResponse)
        {
            r_IsAPositiveResponse = i_IsAPositiveResponse;
        }

        public bool IsAPositiveResponse
        {
            get
            {
                return r_IsAPositiveResponse;
            }
        }
    }
}