﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Enums;

namespace Logic
{
    public class Player
    {
        public readonly string m_Name;
        public int m_Score;
        public readonly ePlayerType m_PlayerType;
        public readonly eCheckerSymbol m_CheckerSymbol;
        public List<Checker> m_Checkers;

        public Player(string i_Name, ePlayerType i_PlayerType, eCheckerSymbol i_CheckerSymbol)
        {
            m_Name = i_Name;
            m_Score = 0;
            m_PlayerType = i_PlayerType;
            m_CheckerSymbol = i_CheckerSymbol;
            m_Checkers = new List<Checker>();
        }

        public void RemoveChecker(Checker i_Checker)
        {
            m_Checkers.Remove(i_Checker);
        }

        public static void Swap(ref Player io_PlayerOne, ref Player io_PlayerTwo)
        {
            Player temp;

            temp = io_PlayerOne;
            io_PlayerOne = io_PlayerTwo;
            io_PlayerTwo = temp;
        }
    }
}
