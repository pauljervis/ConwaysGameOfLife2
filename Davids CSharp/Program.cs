using System;
using System.Threading;

namespace Davids_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // Number of cells we wish to display across the screen.
            const int ScreenWidth = 60;

            // Number of rows we wish to display down the screen
            const int ScreenHeight = 24;

            // Blinker (period 2)
            var board = new Board(ScreenWidth, ScreenHeight);
            board.SetCell(1, 8);
            board.SetCell(2, 8);
            board.SetCell(3, 8);

            // Glider
            board.SetCell(20, 10);
            board.SetCell(21, 10);
            board.SetCell(22, 10);
            board.SetCell(22, 9);
            board.SetCell(21, 8);

            while (true)
            {
                DisplayBoard(board);
                board = board.NextGeneration();
                Thread.Sleep(100);
            }
            
        }

        private static void DisplayBoard(Board board)
        {
            for (int y = 0; y < board.Height; y++)
            {
                var line = "";
                for (int x = 0; x < board.Width; x++)
                {
                    line += board.GetCell(x, y) ? "X" : ".";
                }
                Console.WriteLine(line);
            }
        }
    }
}
