using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingRRTs
{
    public class ObsitcleMap
    {
        public int width { get; set; }
        public int length { get; set; }

        public bool[,] obsticles;

        public ObsitcleMap(int width, int length)
        {
            this.width = width;
            this.length = length;
            SetAllClear();
            MakeObsticles();
        }

        private void SetAllClear()
        {
            obsticles = new bool[width, length];
            for (int i =0; i < width; i++)
            {
                for (int j =0; j < length; j++)
                {
                    obsticles[i, j] = false;
                }
            }
        }

        private void MakeObsticle(int minX, int maxX, int minY, int maxY)
        {
            for (int i = minX; i < maxX; i++)
            {
                for (int j= minY; j < maxY; j++)
                {
                    obsticles[i, j] = true;
                }
            }
        }

        private void MakeObsticles()
        {
            MakeObsticle(0, 50, 400, 405);
            MakeObsticle(100, 500, 400, 405);
            MakeObsticle(100, 150, 100, 105);
            MakeObsticle(200, 500, 100, 105);
            MakeObsticle(100, 105, 300, 305);
            MakeObsticle(300, 500, 200, 205);
        }
    }
}
