using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public static class Rules
    {
        public static GridCellStatusResult Underpopulated(string neighbours, GridCellStatus cellStatus)
        {
            var count = neighbours.Split('X').Length - 1;
            if (count < 2)
            {
                return GridCellStatusResult.Die;
            };

            return GridCellStatusResult.NoChange;
        }

        //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
        public static GridCellStatusResult DeadAndCorrectAmountOfNeighboursToLive(string neighbours, GridCellStatus cellStatus)
        {
            var count = neighbours.Split('X').Length - 1;
            if (count == 3 && cellStatus == GridCellStatus.Dead)
            {
                return GridCellStatusResult.Live;
            }

            return GridCellStatusResult.NoChange;
        }

        //Any live cell with two or three live neighbours lives on to the next generation.
        public static GridCellStatusResult AliveAndCorrectAmountOfNeighboursToLive(string neighbours, GridCellStatus cellStatus)
        {
            var count = neighbours.Split('X').Length - 1;
            if ((count == 3 || count == 2) && cellStatus == GridCellStatus.Alive)
            {
                return GridCellStatusResult.Live;
            }

            return GridCellStatusResult.NoChange;
        }

        //Any live cell with more than three live neighbours dies, as if by overpopulation.
        public static GridCellStatusResult OverPopulated(string neighbours, GridCellStatus cellStatus)
        {
            var count = neighbours.Split('X').Length - 1;
            if (count > 3)
            {
                return GridCellStatusResult.Die;
            }

            return GridCellStatusResult.NoChange;
        }
    }
}
