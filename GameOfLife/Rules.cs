using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public static class Rules
    {
        public static CellStatusResult Underpopulated(string neighbours, CellStatus cellStatus)
        {
            var count = neighbours.Split('X').Length - 1;
            if (count < 2)
            {
                return CellStatusResult.Die;
            };

            return CellStatusResult.NoChange;
        }

        //Any dead cell with exactly three live neighbours becomes a live cell, as if by reproduction.
        public static CellStatusResult DeadAndCorrectAmountOfNeighboursToLive(string neighbours, CellStatus cellStatus)
        {
            var count = neighbours.Split('X').Length - 1;
            if (count == 3 && cellStatus == CellStatus.Dead)
            {
                return CellStatusResult.Live;
            }

            return CellStatusResult.NoChange;
        }

        //Any live cell with two or three live neighbours lives on to the next generation.
        public static CellStatusResult AliveAndCorrectAmountOfNeighboursToLive(string neighbours, CellStatus cellStatus)
        {
            var count = neighbours.Split('X').Length - 1;
            if ((count == 3 || count == 2) && cellStatus == CellStatus.Alive)
            {
                return CellStatusResult.Live;
            }

            return CellStatusResult.NoChange;
        }

        //Any live cell with more than three live neighbours dies, as if by overpopulation.
        public static CellStatusResult OverPopulated(string neighbours, CellStatus cellStatus)
        {
            var count = neighbours.Split('X').Length - 1;
            if (count > 3)
            {
                return CellStatusResult.Die;
            }

            return CellStatusResult.NoChange;
        }
    }
}
