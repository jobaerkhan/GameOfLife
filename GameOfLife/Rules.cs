using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public static class Rules
    {
        public static GridSquareStatusResult Underpopulated(string neighbours, GridSquareStatus cellStatus)
        {
            var count = neighbours.Split('X').Length - 1;
            if (count < 2)
            {
                return GridSquareStatusResult.Die;
            };

            return GridSquareStatusResult.NoChange;
        }

        //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
        public static GridSquareStatusResult DeadAndCorrectAmountOfNeighboursToLive(string neighbours, GridSquareStatus cellStatus)
        {
            var count = neighbours.Split('X').Length - 1;
            if (count == 3 && cellStatus == GridSquareStatus.Dead)
            {
                return GridSquareStatusResult.Live;
            }

            return GridSquareStatusResult.NoChange;
        }

        //Any live cell with two or three live neighbours lives on to the next generation.
        public static GridSquareStatusResult AliveAndCorrectAmountOfNeighboursToLive(string neighbours, GridSquareStatus cellStatus)
        {
            var count = neighbours.Split('X').Length - 1;
            if ((count == 3 || count == 2) && cellStatus == GridSquareStatus.Alive)
            {
                return GridSquareStatusResult.Live;
            }

            return GridSquareStatusResult.NoChange;
        }

        //Any live cell with more than three live neighbours dies, as if by overpopulation.
        public static GridSquareStatusResult OverPopulated(string neighbours, GridSquareStatus cellStatus)
        {
            var count = neighbours.Split('X').Length - 1;
            if (count > 3)
            {
                return GridSquareStatusResult.Die;
            }

            return GridSquareStatusResult.NoChange;
        }
    }
}
