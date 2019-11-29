using System;

namespace GameOfLife
{
    class GameOfLife
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Conways Game of life World!");

            var grid = new Grid(20, 20);
            grid.SeedGrid();

            for (var i = 0; i < 100; i++)
            {
                Console.WriteLine();
                Console.WriteLine($"------Tick {i}------");
                grid.Tick();

                //Thread.Sleep(500);
                Console.ReadLine();
            }
        }
    }
}
