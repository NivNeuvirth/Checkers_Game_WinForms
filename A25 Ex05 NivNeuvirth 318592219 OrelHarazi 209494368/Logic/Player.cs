using System.Collections.Generic;
using Logic.Enums;

namespace Logic
{
    public class Player
    {
        private readonly string r_PlayerName;
        private int m_PlayerScore;
        private readonly ePlayerType r_PlayerType;
        private readonly eCheckerSymbol r_CheckerSymbol;
        private readonly List<Checker> r_Checkers;

        public Player(string i_Name, ePlayerType i_PlayerType, eCheckerSymbol i_CheckerSymbol)
        {
            r_PlayerName = i_Name;
            m_PlayerScore = 0;
            r_PlayerType = i_PlayerType;
            r_CheckerSymbol = i_CheckerSymbol;
            r_Checkers = new List<Checker>();
        }

        public void RemoveChecker(Checker i_Checker)
        {
            r_Checkers.Remove(i_Checker);
        }

        public static void Swap(ref Player io_PlayerOne, ref Player io_PlayerTwo)
        {
            Player tempPlayer;

            tempPlayer = io_PlayerOne;
            io_PlayerOne = io_PlayerTwo;
            io_PlayerTwo = tempPlayer;
        }

        public string PlayerName
        {
            get
            {
                return r_PlayerName;
            }
        }

        public int PlayerScore
        {
            get
            {
                return m_PlayerScore;
            }
            set
            {
                m_PlayerScore = value;
            }
        }

        public ePlayerType PlayerType
        {
            get
            {
                return r_PlayerType;
            }
        }

        public eCheckerSymbol CheckerSymbol
        {
            get
            {
                return r_CheckerSymbol;
            }
        }

        public List<Checker> Checkers
        {
            get
            {
                return r_Checkers;
            }
        }
    }
}