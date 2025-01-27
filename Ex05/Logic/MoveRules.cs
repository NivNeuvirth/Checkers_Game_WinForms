using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.Enums;

namespace Logic
{
    public static class MoveRules
    {
        public static Direction[] GetAllowedDirections(Checker i_Checker)
        {
            Direction[] allowedDirections;

            if (i_Checker.m_CheckerType == eCheckerType.King)
            {
                allowedDirections = KingDirections;
            }
            else
            {
                allowedDirections = i_Checker.m_CheckerSymbol == eCheckerSymbol.WhiteChecker
                    ? Player1Directions
                    : Player2Directions;
            }

            return allowedDirections;
        }


        public static readonly Direction[] Player1Directions =
        {
            new Direction(1, -1),
            new Direction(1, 1)
        };

        public static readonly Direction[] Player2Directions =
        {
            new Direction(-1, -1),
            new Direction(-1, 1)
        };

        public static readonly Direction[] KingDirections =
        {
            new Direction(-1, -1),
            new Direction(-1, 1),
            new Direction(1, -1),
            new Direction(1, 1)
        };

        public struct Direction
        {
            public int RowDelta { get; }
            public int ColDelta { get; }

            public Direction(int rowDelta, int colDelta)
            {
                RowDelta = rowDelta;
                ColDelta = colDelta;
            }
        }
        public static bool IsValidMoveDistance(Move i_Move, bool i_AllowCapture)
        {
            int rowDistance = Math.Abs(i_Move.m_EndRow - i_Move.m_StartRow);
            int colDistance = Math.Abs(i_Move.m_EndCol - i_Move.m_StartCol);

            bool isNormalMove = rowDistance == 1 && colDistance == 1;

            bool isCaptureMove = rowDistance == 2 && colDistance == 2;

            return isNormalMove || i_AllowCapture && isCaptureMove;
        }

        public static (int o_Row, int o_Col) GetCapturedPosition(Move i_Move)
        {
            return (
               o_Row: (i_Move.m_StartRow + i_Move.m_EndRow) / 2,
               o_Col: (i_Move.m_StartCol + i_Move.m_EndCol) / 2
            );
        }
    }
}

