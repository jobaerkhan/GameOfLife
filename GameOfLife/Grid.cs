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
        private delegate GridSquareStatusResult Rule(string neighbours, GridSquareStatus cellStatus);
        private List<Rule> rules;
        public Grid(int universeWidth, int universeHeight)
        {
            var _cells = new Cell[universeWidth, universeHeight];

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
            Console.WriteLine("Initial grid will be setup here");
        }

        public void Tick()
        {
            Console.WriteLine("Rule for next tick will be applied to generate the next cells");
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
