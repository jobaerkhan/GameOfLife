using System;
using System.Collections.Generic;
using System.Threading;

namespace GameOfLife
{
    class GameOfLife
    {
        private static Cell[,] _cells;
        private const char SelfToken = '0';
        private const char OutsideBoundsToken = 'B';
        private delegate CellStatusResult Rule(string neighbours, CellStatus cellStatus);
        private static List<Rule> rules;
        static void Main(string[] args)
        {
            Console.WriteLine("Conways Game of life!");
            _cells = new Cell[20, 20];
            rules = new List<Rule>
            {
                new Rule(Rules.Underpopulated),
                new Rule(Rules.OverPopulated),
                new Rule(Rules.AliveAndCorrectAmountOfNeighboursToLive),
                new Rule(Rules.DeadAndCorrectAmountOfNeighboursToLive)
            };
            Seed();

            for (var i = 0; i < 100; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"------Generation {i}------");
                PrintcellInConsole(GetCells());
                Tick();
                Thread.Sleep(500);
                //Console.Clear();
            }
        }

        public static string PrintHorizontalLine(Cell[,] cell, int line)
        {
            var result = "";

            for (int i = 0; i < cell.GetLength(0); i++)
            {
                result = result + cell[line, i].Print();
            }

            return result;
        }

        public static void PrintcellInConsole(Cell[,] cell)
        {
            for (int i = 0; i < cell.GetLength(0); i++)
            {
                Console.WriteLine(PrintHorizontalLine(cell,i));
            }
        }

        public static Cell[,] GetCells()
        {
            return _cells;
        }

        public static void Seed()
        {
            for (var i = 0; i < _cells.GetLength(0); i++)
            {
                for (var j = 0; j < _cells.GetLength(1); j++)
                {
                    _cells[i, j] = new Cell();
                    _cells[i, j].SetToRandomState();
                }
            }
        }

        public static void Tick()
        {
            var nextGeneration = new Cell[_cells.GetLength(0), _cells.GetLength(1)];
            for (var i = 0; i < nextGeneration.GetLength(0); i++)
            {
                for (var j = 0; j < nextGeneration.GetLength(1); j++)
                {
                    nextGeneration[i, j] = new Cell(false);
                }
            }

            for (var i = 0; i < _cells.GetLength(0); i++)
            {
                for (var j = 0; j < _cells.GetLength(1); j++)
                {
                    var nextGenerationCellResult = CellStatusResult.NoChange;

                    var neighbours = GetNeighbours(i, j);

                    foreach (var rule in rules)
                    {
                        var result = rule(neighbours, _cells[i, j].CellStatus());

                        if (result == CellStatusResult.Live || result == CellStatusResult.Die)
                        {
                            nextGenerationCellResult = result;
                        }
                    }

                    //Appy Rule to next world
                    switch (nextGenerationCellResult)
                    {
                        case CellStatusResult.Live:
                            nextGeneration[i, j].SetToAlive();
                            break;
                        case CellStatusResult.Die:
                            nextGeneration[i, j].SetToDie();
                            break;
                        case CellStatusResult.NoChange:
                            nextGeneration[i, j] = _cells[i, j];
                            break;
                    }
                }
            }
            _cells = nextGeneration;
        }

        public static string GetNeighbours(int x, int y)
        {
            var neighbours = new[]
            {
                PrintSafeChar(x - 1, y - 1), PrintSafeChar(x - 1, y), PrintSafeChar(x - 1, y + 1),
                PrintSafeChar(x    , y - 1), SelfToken              , PrintSafeChar(x    , y + 1),
                PrintSafeChar(x + 1, y - 1), PrintSafeChar(x + 1, y), PrintSafeChar(x + 1, y + 1)
            };

            return new string(neighbours);
        }

        private static char PrintSafeChar(int x, int y)
        {
            if (x > -1 && y > -1 && y < (_cells.GetUpperBound(1) + 1) && x < (_cells.GetUpperBound(0) + 1))
            {
                return _cells[x, y].Print();
            }
            else
            {
                return OutsideBoundsToken;
            }
        }
    }
}
