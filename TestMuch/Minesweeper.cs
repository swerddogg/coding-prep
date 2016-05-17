using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TestMuch
{
    public enum CellType
    {

        EMPTY,
        BOMB,
        ADJACENT
    }



    public class Cell
    {
        public int Row
        {
            get; private set;
        }
        public int Col
        {
            get; private set;
        }

        public CellType Type
        {
            get; set;
        }

        public int? AdjacentBombs { get; set; }
        
        public Cell (int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        public bool IsUncovered { get; private set; }

        public void Uncover()
        {
            IsUncovered = true;
        }      

        public override string ToString()
        {
            return ShowBoard();
        }

        public string ShowBoard(bool bypassLock = false)
        {
            if (!bypassLock && !IsUncovered)
                return "?";

            switch (Type)
            {
                case CellType.EMPTY:
                    return ".";
                case CellType.BOMB:
                    return "*";
                case CellType.ADJACENT:
                    return AdjacentBombs.Value.ToString();
            }
            return "!";
        }
    }

    public class Board
    {
        private Cell[,] grid;
        private int nValue;
        private int totalBombs;
        public Board(int n)
        {
            nValue = n;
            totalBombs = nValue - 1;
            grid = new Cell[n, n];
            ResetBoard();
        }

        public void ResetBoard()
        {
            InitializeCells();

            CreateRandomBombs();

            CalculateValues();
        }

        private void CreateRandomBombs()
        {
            Random r = new Random();
            int numBombs = totalBombs;

            while (numBombs > 0)
            {
                int row = r.Next(0, nValue - 1);
                int col = r.Next(0, nValue - 1);

                if (grid[row, col].Type != CellType.BOMB)
                {
                    numBombs--;
                    grid[row, col].Type = CellType.BOMB;
                }
            }
        }

        private void InitializeCells()
        {
            for (int col = 0; col < nValue; col++)
            {
                for (int row = 0; row < nValue; row++)
                {
                    grid[row, col] = new Cell(row, col);
                }
            }
        }

        private void CalculateValues()
        {
            for (int col = 0; col < nValue; col++)
            {
                for (int row = 0; row < nValue; row++)
                {
                    if (grid[row, col].Type == CellType.BOMB)
                    {
                        UpdateAdjacentCellsWithBombInfo(row, col);
                    }
                    else if (grid[row, col].Type != CellType.ADJACENT)
                    {
                        grid[row, col].Type = CellType.EMPTY;
                    }
                }
            }
        }

        private void UpdateAdjacentCellsWithBombInfo(int bombsRow, int bombsCol)
        {
            for (int myRow = bombsRow - 1; myRow <= bombsRow + 1; myRow++)
            {
                for (int myCol = bombsCol - 1; myCol <= bombsCol + 1; myCol++)
                {
                    if (myRow < 0 || myCol < 0 ||
                        myRow >= nValue || myCol >= nValue ||
                        (myRow == bombsRow && myCol == bombsCol))
                    {
                        continue;
                    }

                    if (grid[myRow, myCol].AdjacentBombs.HasValue)
                    {
                        grid[myRow, myCol].AdjacentBombs++;
                    }
                    else
                    {
                        if (grid[myRow, myCol].Type == CellType.BOMB)
                        {
                            continue;
                        }
                        grid[myRow, myCol].Type = CellType.ADJACENT;
                        grid[myRow, myCol].AdjacentBombs = 1;
                    }
                }
            }
        }

        public Cell GetCell(int row, int col)
        {
            if (row < 0 || row >= nValue || col < 0 || col >= nValue)
            {
                return null;
            }

            return grid[row, col];
        }

        Queue<Cell> cellsToUncover = new Queue<Cell>();

        public bool UncoverCell(Cell cell)
        {
            if (cell.Type == CellType.BOMB)
            {
                cell.Uncover();
                return true;
            }

            cellsToUncover.Enqueue(cell);

            while (cellsToUncover.Any())
            {
                Cell cellToVisit = cellsToUncover.Dequeue();

                if (cellToVisit.IsUncovered)
                    continue;

                cellToVisit.Uncover();

                if (cellToVisit.Type == CellType.EMPTY)
                {
                    for (int myRow = cellToVisit.Row - 1; myRow <= cellToVisit.Row + 1; myRow++)
                    {
                        for (int myCol = cellToVisit.Col - 1; myCol <= cellToVisit.Col + 1; myCol++)
                        {
                            if (myRow < 0 || myCol < 0 ||
                                myRow >= nValue || myCol >= nValue ||
                                (myRow == cellToVisit.Row && myCol == cellToVisit.Col))
                            {
                                continue;
                            }
                            
                            cellsToUncover.Enqueue(grid[myRow, myCol]);
                        }
                    }
                }
            }
            return false;
        }

        public void RevealBoard()
        {            
            foreach(var cell in grid)
            {
                cell.Uncover();
            }         
        }

        public int GetTotalBombs()
        {
            return totalBombs;
        }

        public int GetUncoveredCellCount()
        {
            int count = 0;
            foreach (var cell in grid)
            {
                count = cell.IsUncovered ? count + 1: count;
            }
            return count;
        }

        internal int GetTotalCellCount()
        {
            return grid.Length;
        }

        internal int GetRowColumnCount()
        {
            return nValue;
        }
    }

    public class GamePlay
    {
        Board gameBoard;
        public GamePlay(Board gameBoard)
        {
            this.gameBoard = gameBoard;
        }

        public bool UncoverCell(Cell cell)
        {
            return gameBoard.UncoverCell(cell);
        }

        public bool IsGameWon()
        {
            return (this.gameBoard.GetTotalCellCount() - this.gameBoard.GetUncoveredCellCount()) <= gameBoard.GetTotalBombs();
        }
    }

    interface Display
    {
        void DisplayBoard();

        Cell PickCellToUncover();

        void GameLost();

        void GameWon();

        bool WantToPlayAgain();
    }

    public class TextDisplay : Display
    {
        private Board gameBoard;
        public TextDisplay(Board gameBoard)
        {
            this.gameBoard = gameBoard;
        }

        public void DisplayBoard()
        {
            DisplayBoard(false);
        }
        public void DisplayBoard(bool bypassLock = false)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat($"   ");
            for (int row = 0; row < gameBoard.GetRowColumnCount(); row++)
            {
                sb.AppendFormat($" {row}");
            }
            sb.AppendLine(); sb.AppendFormat("   ");
            for (int row = 0; row < gameBoard.GetRowColumnCount(); row++)
            {
                sb.AppendFormat($"__");
            }
            sb.AppendLine();

            for (int row = 0; row < gameBoard.GetRowColumnCount(); row++)
            {
                sb.AppendFormat($" {row}|");
                for (int col = 0; col < gameBoard.GetRowColumnCount(); col++)
                {
                    if (!bypassLock)
                    {
                        sb.AppendFormat($" {gameBoard.GetCell(row, col).ToString()}");
                    }
                    else
                    {
                        sb.AppendFormat($" {gameBoard.GetCell(row, col).ShowBoard(true)}");
                    }
                }
                sb.AppendLine();
            }
            Console.WriteLine(sb.ToString());
        }

        public void GameLost()
        {
            Console.WriteLine("Bomb found, you lose!");
        }

        public void GameWon()
        {
            Console.WriteLine("You avoided all the bombs, winner!!");
        }

        public Cell PickCellToUncover()
        {
            Cell cellToUncover = null;
            Console.WriteLine("Select cell to Uncover: [Row, Column], example: 0,0");

            int row;
            int column;

            string line = Console.ReadLine();
            var chunks = line.Split(',');
            if (chunks.Length != 2)
            {
                Console.Error.WriteLine($"Invalid Cell specified!: {line}");
                return cellToUncover;
            }

            if (!int.TryParse(chunks[0], out row))
            {
                Console.Error.WriteLine($"Invalid row!: {chunks[0]}");
                return cellToUncover;
            }

            if (!int.TryParse(chunks[1], out column))
            {
                Console.Error.WriteLine($"Invalid column!: {chunks[1]}");
                return cellToUncover;
            }

            if (row >= gameBoard.GetRowColumnCount())
            {
                Console.Error.WriteLine($"Row is out of bounds!: {row}");
                return cellToUncover;
            }

            if (column >= gameBoard.GetRowColumnCount())
            {
                Console.Error.WriteLine($"Column is out of bounds!: {column}");
                return cellToUncover;
            }

            return gameBoard.GetCell(row, column);
        }

        public bool WantToPlayAgain()
        {
            Console.WriteLine("Do you want to play again? Yes[y], or any key for No {Enter}");

            string playAgain = Console.ReadLine();

            if (playAgain == "y" || playAgain == "Y" || playAgain == "yes" || playAgain == "Yes")
            {
                return true;
            }

            return false;
        }
    }

    public class Minesweeper
    {
        private Board board;
        private Display display;
        private GamePlay gamePlay;

        public Minesweeper(int boardSize)
        {
            board = new Board(boardSize);
            display = new TextDisplay(board);
            gamePlay = new GamePlay(board);
        }

        public void StartGame()
        {
            bool gameOver = false;
            bool playAgain = true;
           // (display as TextDisplay).DisplayBoard(true);


            while (playAgain)
            {
                while (!gameOver)
                {
                    display.DisplayBoard();

                    Cell cellToPlay = display.PickCellToUncover();

                    if (cellToPlay != null && gamePlay.UncoverCell(cellToPlay))
                    {
                        display.GameLost();
                        gameOver = true;
                    }
                    else if (gamePlay.IsGameWon())
                    {
                        display.GameWon();
                        gameOver = true;
                    }
                }

                board.RevealBoard();
                display.DisplayBoard();


                playAgain = display.WantToPlayAgain();

                if (playAgain)
                {
                    board.ResetBoard();
                }
                gameOver = false;
            }
        }
    }
}
