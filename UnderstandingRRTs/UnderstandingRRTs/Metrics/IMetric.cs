using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingRRTs
{
    public interface IMetric
    {
        double GetDistance(Coord coord1, Coord coord2);
    }
}
