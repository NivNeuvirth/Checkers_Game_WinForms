using Logic.Enums;

namespace Logic
{
    public class Board
    {
        private readonly Checker[,] r_Board;
        private readonly int r_Size;

        public Board(eGameBoardSize i_Size, Player i_Player1, Player i_Player2)
        {
            r_Size = (int)i_Size;
            r_Board = new Checker[r_Size, r_Size];
            InitializeBoard(i_Player1, i_Player2);
        }

        public void InitializeBoard(Player i_Player1, Player i_Player2)
        {
            for (int i = 0; i < r_Size; i++)
            {
                for (int j = 0; j < r_Size; j++)
                {
                    if ((i + j) % 2 != 0 && i < r_Size / 2 - 1)
                    {
                        Checker checker = new Checker(i, j, eCheckerType.Soldier, eCheckerSymbol.WhiteChecker);
                        r_Board[i, j] = checker;
                        i_Player1.Checkers.Add(checker);
                    }
                    else if ((i + j) % 2 != 0 && i > r_Size / 2)
                    {
                        Checker checker = new Checker(i, j, eCheckerType.Soldier, eCheckerSymbol.BlackChecker);
                        r_Board[i, j] = checker;
                        i_Player2.Checkers.Add(checker);
                    }
                    else
                    {
                        r_Board[i, j] = null;
                    }
                }
            }
        }

        public bool IsInsideBoard(int i_Row, int i_Col)
        {
            return i_Row >= 0 && i_Row < r_Board.GetLength(0) && i_Col >= 0 && i_Col < r_Board.GetLength(1);
        }

        public Checker GetCheckerAt(int i_Row, int i_Col)
        {
            if (!IsInsideBoard(i_Row, i_Col))
            {
                return null;
            }

            Checker checker = r_Board[i_Row, i_Col];
            
            return checker;
        }

        public bool IsEmpty(int i_Row, int i_Col)
        {
            return IsInsideBoard(i_Row, i_Col) && r_Board[i_Row, i_Col] == null;
        }

        public void MoveChecker(Move i_Move)
        {
            Checker movingChecker = r_Board[i_Move.StartRow, i_Move.StartCol];

            movingChecker.LocationOnBoardX = i_Move.EndRow;
            movingChecker.LocationOnBoardY = i_Move.EndCol;
            r_Board[i_Move.EndRow, i_Move.EndCol] = movingChecker;
            r_Board[i_Move.StartRow, i_Move.StartCol] = null;
        }

        public void RemoveChecker(int i_Row, int i_Col)
        {
            r_Board[i_Row, i_Col] = null;
        }

        public int Size
        {
            get
            {
                return r_Size;
            }
        }
    }
}