using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingRRTs
{
    public class EuclideanDistanceMetric : IMetric
    {
        public double GetDistance(Coord coord1, Coord coord2)
        {
            return Math.Sqrt(Math.Pow(coord1.x - coord2.x, 2) + Math.Pow(coord1.y - coord2.y, 2));
        }
    }
}
