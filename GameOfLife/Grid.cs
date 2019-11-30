using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class Grid
    {
        private Cell[,] _cells;
        private const char SelfToken = '0';
        private const char OutsideBoundsToken = 'B';
        private delegate GridCellStatusResult Rule(string neighbours, GridCellStatus cellStatus);
        private List<Rule> rules;
        public Grid(int width, int height)
        {
            _cells = new Cell[width, height];

            rules = new List<Rule>
            {
                new Rule(Rules.Underpopulated),
                new Rule(Rules.OverPopulated),
                new Rule(Rules.AliveAndCorrectAmountOfNeighboursToLive),
                new Rule(Rules.DeadAndCorrectAmountOfNeighboursToLive)
            };
        }

        public Cell[,] GetCells()
        {
            return _cells;
        }

        public void SeedGrid()
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

        public void Tick()
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
                    var nextGenerationCellResult = GridCellStatusResult.NoChange;

                    var neighbours = GetNeighbours(i, j);

                    foreach (var rule in rules)
                    {
                        var result = rule(neighbours, _cells[i, j].GridStatus());

                        if (result == GridCellStatusResult.Live || result == GridCellStatusResult.Die)
                        {
                            nextGenerationCellResult = result;
                        }
                    }

                    //Appy Rule to next world
                    switch (nextGenerationCellResult)
                    {
                        case GridCellStatusResult.Live:
                            nextGeneration[i, j].SetToAlive();
                            break;
                        case GridCellStatusResult.Die:
                            nextGeneration[i, j].SetToDie();
                            break;
                        case GridCellStatusResult.NoChange:
                            nextGeneration[i, j] = _cells[i, j];
                            break;
                    }
                }
            }
            _cells = nextGeneration;
        }

        public string GetNeighbours(int x, int y)
        {
            var neighbours = new[]
            {
                PrintSafeChar(x - 1, y - 1), PrintSafeChar(x - 1, y), PrintSafeChar(x - 1, y + 1),
                PrintSafeChar(x    , y - 1), SelfToken              , PrintSafeChar(x    , y + 1),
                PrintSafeChar(x + 1, y - 1), PrintSafeChar(x + 1, y), PrintSafeChar(x + 1, y + 1)
            };

            return new string(neighbours);
        }

        private char PrintSafeChar(int x, int y)
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
