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

        public int[,] obsticles;

        public ObsitcleMap(int width, int length)
        {
            this.width = width;
            this.length = length;
            SetAllClear();
            MakeMap();
        }

        private void SetAllClear()
        {
            obsticles = new int[width, length];
            for (int i =0; i < width; i++)
            {
                for (int j =0; j < length; j++)
                {
                    obsticles[i, j] = 0;
                }
            }
        }

        private void MakeObsticle(int minX, int maxX, int minY, int maxY)
        {
            for (int i = minX; i < maxX; i++)
            {
                for (int j= minY; j < maxY; j++)
                {
                    obsticles[i, j] = -1;
                }
            }
        }

        private void MakeGoal(int minX, int maxX, int minY, int maxY)
        {
            for (int i = minX; i < maxX; i++)
            {
                for (int j = minY; j < maxY; j++)
                {
                    obsticles[i, j] = 1;
                }
            }
        }

        private void MakeMap()
        {
            MakeObsticle(0, 100, 400, 405);
            MakeObsticle(150, 500, 400, 405);
            MakeObsticle(0, 300, 300, 305);
            MakeObsticle(350, 500, 300, 305);
            MakeGoal(450, 500, 405, 500);
        }
    }
}
