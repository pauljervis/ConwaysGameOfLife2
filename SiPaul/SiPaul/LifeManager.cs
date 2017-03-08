using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiPaul
{
    public class LifeManager
    {
        private bool[,] cell;

        public LifeManager(int width, int height)
        {
            cell = new bool[width, height];
        }

        //public void SetCell(int x, int y, bool status)
        //{
        //    cell[x, y] = status;
        //}

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

        public static bool CheckAlive(bool current, int siblingCount)
        {
            return (siblingCount == 3 || current && siblingCount == 2);
        }
    }
}
