using System;

namespace Davids_Example
{
    internal class Board
    {
        public int Width { get; private set; }
        public int Height { get; private set; }

        private bool[,] cells;

        public const bool ALIVE = true;
        public const bool DEAD = false;

        /// <summary>
        /// Initialises a new board
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        public Board(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.cells = new bool[this.Width, this.Height];
        }

        /// <summary>
        /// Returns a new board with the same dimensions as this board but
        /// containing the next generation of cells
        /// </summary>
        /// <returns></returns>
        public Board NextGeneration()
        {
            var next = new Board(this.Width, this.Height);
            for (int x = 0; x < this.Width; x++)
            {
                for (int y = 0; y < this.Height; y++)
                {
                    var liveCell = this.cells[x, y];
                    var deadCell = !liveCell;
                    var numberOfNeighbours = CountNeighbours(x, y);

                    //Any live cell with fewer than two live neighbours dies, as if caused by underpopulation
                    if (liveCell && numberOfNeighbours < 2)
                        next.SetCell(x, y, DEAD);

                    //Any live cell with more than three live neighbours dies, as if by overcrowding.
                    if (liveCell && numberOfNeighbours > 3)
                        next.SetCell(x, y, DEAD);

                    //Any live cell with two or three live neighbours lives on to the next generation.
                    if (liveCell && (numberOfNeighbours == 2 || numberOfNeighbours == 3))
                        next.SetCell(x, y, ALIVE);

                    //Any dead cell with exactly three live neighbours becomes a live cell.
                    if (deadCell && numberOfNeighbours == 3)
                        next.SetCell(x, y, ALIVE);
                }
            }

            return next;
        }

        /// <summary>
        /// Returns the current value of a cell
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool GetCell(int x, int y)
        {
            if (x < 0 || x > this.Width - 1) throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y > this.Height - 1) throw new ArgumentOutOfRangeException(nameof(y));

            return this.cells[x, y];
        }

        /// <summary>
        /// Sets the value of this single cell
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="alive"></param>
        public void SetCell(int x, int y, bool alive = true)
        {
            if (x < 0 || x > this.Width - 1) throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y > this.Height - 1) throw new ArgumentOutOfRangeException(nameof(y));

            this.cells[x, y] = alive;
        }

        /// <summary>
        /// How many live neighbours does this cell work?
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public byte CountNeighbours(int x, int y)
        {
            if (x < 0 || x > this.Width - 1) throw new ArgumentOutOfRangeException(nameof(x));
            if (y < 0 || y > this.Height - 1) throw new ArgumentOutOfRangeException(nameof(y));

            var number = 0;

            //Up
            if (y > 0) number += this.cells[x, y - 1] ? 1 : 0;
            if (y > 0 && x > 0) number += this.cells[x - 1, y - 1] ? 1 : 0;
            if (y > 0 && x < this.Width - 1) number += this.cells[x + 1, y - 1] ? 1 : 0;

            //Down
            if (y < this.Height - 1) number += this.cells[x, y + 1] ? 1 : 0;
            if (y < this.Height - 1 && x > 0) number += this.cells[x - 1, y + 1] ? 1 : 0;
            if (y < this.Height - 1 && x < this.Width - 1) number += this.cells[x + 1, y + 1] ? 1 : 0;

            //Left/Right
            if (x > 0) number += this.cells[x - 1, y] ? 1 : 0;
            if (x < this.Width - 1) number += this.cells[x + 1, y] ? 1 : 0;

            return (byte)number;
        }
    }
}
