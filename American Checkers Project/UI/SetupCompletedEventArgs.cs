using System;
using Logic.Enums;

namespace UI
{
    public class SetupCompletedEventArgs : EventArgs
    {
        private readonly string r_Player1Name;
        private readonly string r_Player2Name;
        private readonly int r_CheckersBoardSize;
        private readonly bool r_IsAComputer;

        public SetupCompletedEventArgs(string i_Player1, string i_Player2, int i_GameBoardSize, bool i_IsComputer)
        {
            r_Player1Name = i_Player1;
            r_Player2Name = i_Player2;
            r_CheckersBoardSize = i_GameBoardSize;
            r_IsAComputer = i_IsComputer;
        }

        public string Player1Name
        {
            get
            {
                return r_Player1Name;
            }
        }

        public string Player2Name
        {
            get
            {
                return r_Player2Name;
            }
        }

        public int CheckersBoardSize
        {
            get
            {
                return r_CheckersBoardSize;
            }
        }

        public ePlayerType IsPlayer2AComputer
        {
            get
            {
                return r_IsAComputer ? ePlayerType.Computer : ePlayerType.Human;
            }
        }
    }
}