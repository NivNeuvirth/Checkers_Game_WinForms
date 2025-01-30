using System;
using System.Collections.Generic;
using System.Text;
using Logic.Enums;

namespace UI
{
    public class DetailsFilledEventArgs : EventArgs
    {
        private string m_Player1Name, m_Player2Name;
        private int m_GameBoardSize;
        private bool m_IsPc;

        public DetailsFilledEventArgs(string i_Player1, string i_Player2, int i_GameBoardSize, bool i_IsPc)
        {
            m_Player1Name = i_Player1;
            m_Player2Name = i_Player2;
            m_GameBoardSize = i_GameBoardSize;
            m_IsPc = i_IsPc;
        }

        public string Player1Name
        {
            get
            {
                return m_Player1Name;
            }
        }

        public string Player2Name
        {
            get
            {
                return m_Player2Name;
            }
        }

        public int GameBoardSize
        {
            get
            {
                return m_GameBoardSize;
            }
        }

        public ePlayerType IsPlayer2PC
        {
            get
            {
                if(m_IsPc)
                {
                    return ePlayerType.Computer;
                }
                else
                {
                    return ePlayerType.Human;
                };
            }
        }
    }
}
