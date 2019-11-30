using System;
using System.Threading;

namespace GameOfLife
{
    class GameOfLife
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Conways Game of life!");

            var grid = new Grid(20, 20);
            grid.SeedGrid();

            for (var i = 0; i < 100; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"------Generation {i}------");
                PrintcellInConsole(grid.GetCells());
                grid.Tick();
                Thread.Sleep(500);
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
    }
}
