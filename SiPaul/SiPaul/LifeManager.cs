using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiPaul
{
    class LifeManager
    {
        readonly bool[,] cell;

        public LifeManager(int width, int height)
        {
            cell = new bool[width, height];
        }

        public void GetCell()
        {
            return cell;
        }

        public int this[int x, int y]
        {
            get
            {
                return cell[x, y] ? 1 : 0;
            }
            set
            {
                cell[x, y] = value > 0;
            }
        }

        public void Step()
        {
            for (int i = 0; i < cell.GetLength(0); i++)
            {
                for (int j = 0; j < cell.GetLength(1); j++)
                {
                    int count = (this[i - 1, j - 1] + this[i - 1, j] + this[i - 1, j + 1] + this[i, j - 1] + this[i, j] + this[i, j + 1] + this[i + 1, j - 1] + this[i + 1, j] + this[i + 1, j + 1]);
                    cell[i, j] = checkAlive(cell[i, j], count);
                }
            }
        }

        public bool checkAlive(bool current, int siblingCount)
        {
            return (siblingCount == 3 || current && siblingCount == 2);
        }
    }
}
