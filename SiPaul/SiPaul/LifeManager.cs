using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiPaul
{
    public class LifeManager
    {
        private class CellValue
        {
            readonly int _x;
            readonly int _y;
            readonly bool _value;

            public CellValue(int x, int y, bool value)
            {
                _x = x;
                _y = y;
                _value = value;
            }

            public int GetX()
            {
                return _x;
            }

            public int GetY()
            {
                return _y;
            }

            public bool GetValue()
            {
                return _value;
            }
        }

        private bool[,] cell;

        public LifeManager(int width, int height)
        {
            cell = new bool[width, height];
        }

        public bool[,] GetCell()
        {
            return cell;
        }

        public int this[int x, int y]
        {
            get
            {
                return 
                    x > -1 && 
                    x < cell.GetLength(0) && 
                    y > -1 && 
                    y < cell.GetLength(1) && 
                    cell[x, y] ? 1 : 0;
            }
            set
            {
                if(x>-1 && x<cell.GetLength(0) && y >-1 && y<cell.GetLength(1))
                    cell[x, y] = value > 0;
            }
        }

        public bool[,] Step()
        {
            bool[,] nextcell = new bool[cell.GetLength(0), cell.GetLength(1)];
            for (int i = 0; i < cell.GetLength(0); i++)
            {
                for (int j = 0; j < cell.GetLength(1); j++)
                {
                    int count = (this[i - 1, j - 1] + this[i - 1, j] + this[i - 1, j + 1] + this[i, j - 1] + this[i, j + 1] + this[i + 1, j - 1] + this[i + 1, j] + this[i + 1, j + 1]);
                    nextcell[i, j] = CheckAlive(cell[i, j], count);
                }
            }
            cell = nextcell;
            return cell;
        }

        public bool[,] ParallelStep()
        {
            bool[,] nextcell = new bool[cell.GetLength(0), cell.GetLength(1)];

            for(int i = 0; i < cell.GetLength(0); i++)
            {
                Parallel.For(0, cell.GetLength(1),
                    j =>
                    {
                        int count = (this[i - 1, j - 1] + this[i - 1, j] + this[i - 1, j + 1] + this[i, j - 1] + this[i, j + 1] + this[i + 1, j - 1] + this[i + 1, j] + this[i + 1, j + 1]);
                        nextcell[i, j] = CheckAlive(cell[i, j], count);
                    });
            }

            cell = nextcell;

            return cell;
        }

        public static bool CheckAlive(bool current, int siblingCount)
        {
            return (siblingCount == 3 || current && siblingCount == 2);
        }
    }
}
