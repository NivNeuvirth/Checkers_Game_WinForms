using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Enums;

namespace Logic
{
    public class Checker
    {
        public int m_X;
        public int m_Y;
        public eCheckerType m_CheckerType;
        public eCheckerSymbol m_CheckerSymbol;
        public Checker(int i_x, int i_y, eCheckerType i_checkerType, eCheckerSymbol i_checkerSymbol)
        {
            m_X = i_x;
            m_Y = i_y;
            m_CheckerType = i_checkerType;
            m_CheckerSymbol = i_checkerSymbol;
        }

        public override string ToString()
        {
            return ((char)m_CheckerSymbol).ToString();
        }
    }
}
