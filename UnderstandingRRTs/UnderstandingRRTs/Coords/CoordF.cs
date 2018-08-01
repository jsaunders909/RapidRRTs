using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingRRTs
{
    public class CoordD
    {
        public double x;
        public double y;

        public CoordD(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static CoordD operator *(CoordD coord, double scalar)
        {
            return new CoordD(coord.x * scalar, coord.y * scalar);
        }
        public static CoordD operator /(CoordD coord, double scalar)
        {
            return new CoordD(coord.x / scalar, coord.y / scalar);
        }
        public static CoordD operator + (CoordD coord1, CoordD coord2)
        {
            return new CoordD(coord1.x + coord2.x, coord1.y + coord2.y);
        }

        public static CoordD operator - (CoordD coord1, CoordD coord2)
        {
            return new CoordD(coord1.x - coord2.x, coord1.y - coord2.y);
        }

        public static implicit operator Coord(CoordD coord)
        {
            int x = (int)Math.Round(coord.x);
            int y = (int)Math.Round(coord.y);
            return new Coord(x, y,0);
        }

        public static implicit operator CoordD (Coord other)
        {
            return new CoordD(other.x, other.y);
        }
    }
}
