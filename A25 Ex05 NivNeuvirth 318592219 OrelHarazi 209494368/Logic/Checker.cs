using System.Drawing;
using Logic.Enums;

namespace Logic
{
    public class Checker
    {
        private int m_LocationOnBoardX;
        private int m_LocationOnBoardY;
        private eCheckerType m_CheckerType;
        private eCheckerSymbol m_CheckerSymbol;
        public Checker(int i_LocationX, int i_LocationY, eCheckerType i_CheckerType, eCheckerSymbol i_CheckerSymbol)
        {
            m_LocationOnBoardX = i_LocationX;
            m_LocationOnBoardY = i_LocationY;
            m_CheckerType = i_CheckerType;
            m_CheckerSymbol = i_CheckerSymbol;
        }

        public int LocationOnBoardX
        {
            get
            {
                return m_LocationOnBoardX;
            }
            set
            {
                m_LocationOnBoardX = value;
            }
        }

        public int LocationOnBoardY
        {
            get
            {
                return m_LocationOnBoardY;
            }
            set
            {
                m_LocationOnBoardY = value;
            }
        }

        public eCheckerType CheckerType
        {
            get
            {
                return m_CheckerType;
            }
            set
            {
                m_CheckerType = value;
            }
        }

        public eCheckerSymbol CheckerSymbol
        {
            get
            {
                return m_CheckerSymbol;
            }
            set
            {
                m_CheckerSymbol = value;
            }
        }

        public Point CheckerLocation
        {
            get
            {
                return new Point(m_LocationOnBoardX, m_LocationOnBoardY);
            }
        }

        public override string ToString()
        {
            return ((char)m_CheckerSymbol).ToString();
        }
    }
}