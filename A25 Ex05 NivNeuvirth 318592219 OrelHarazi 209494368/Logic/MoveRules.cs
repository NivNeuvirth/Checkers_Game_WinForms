using System;
using Logic.Enums;

namespace Logic
{
    public static class MoveRules
    {
        private static readonly Direction[] sr_Player1Directions =
        {
            new Direction(1, -1),
            new Direction(1, 1)
        };

        private static readonly Direction[] sr_Player2Directions =
        {
            new Direction(-1, -1),
            new Direction(-1, 1)
        };

        private static readonly Direction[] sr_KingDirections =
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

            public Direction(int i_RowDelta, int i_ColDelta)
            {
                RowDelta = i_RowDelta;
                ColDelta = i_ColDelta;
            }
        }

        public static Direction[] GetAllowedDirections(Checker i_Checker)
        {
            Direction[] allowedDirections;

            if (i_Checker.CheckerType == eCheckerType.King)
            {
                allowedDirections = sr_KingDirections;
            }
            else
            {
                allowedDirections = i_Checker.CheckerSymbol == eCheckerSymbol.WhiteChecker
                                        ? sr_Player1Directions
                                        : sr_Player2Directions;
            }

            return allowedDirections;
        }

        public static bool IsValidMoveDistance(Move i_Move, bool i_AllowCapture)
        {
            int rowDistance = Math.Abs(i_Move.EndRow - i_Move.StartRow);
            int colDistance = Math.Abs(i_Move.EndCol - i_Move.StartCol);

            bool isNormalMove = rowDistance == 1 && colDistance == 1;

            bool isCaptureMove = rowDistance == 2 && colDistance == 2;

            return isNormalMove || i_AllowCapture && isCaptureMove;
        }
    }
}