using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingRRTs
{
    public class Circle
    {
        public static bool Intersects(Circle circle1, Circle circle2, out CoordD[] intersections)
        { 
            double a1 = circle1.Center.x;
            double b1 = circle1.Center.y;

            double a2 = circle2.Center.x;
            double b2 = circle2.Center.y;

            double r1 = circle1.Radius;
            double r2 = circle2.Radius;
            intersections = new CoordD[2];

            double distance = Math.Sqrt(Math.Pow(a1 - a2, 2) + Math.Pow(b1 - b2,2));

            if (distance > r1 + r2) { return false; }
            if (distance < Math.Abs(r1 - r2)) { return false; }
            if (distance == 0 && (r2 == r1)) { return false; }


            //http://csharphelper.com/blog/2014/09/determine-where-two-circles-intersect-in-c/
            double a = ((r1 * r1) - (r2 * r2) + (distance * distance)) / (2 * distance);
            double b = ((r2 * r2) - (r1 * r1) + (distance * distance)) / (2 * distance);
            double h = Math.Sqrt((r1 * r1) - (a * a));


            double centerX = a1 + a * (a2 - a1) / distance;
            double centerY = b1 + a * (b2 - b1) / distance;

            double x1 = centerX + h * (b2 - b1) / distance;
            double y1 = centerY - h * (a2 - a1) / distance;


            double x2 = centerX - h * (b2 - b1) / distance;
            double y2 = centerY + h * (a2 - a1) / distance;


            CoordD intersection1 = new CoordD((x1), (y1));
            CoordD intersection2 = new CoordD(x2, y2);

            intersections[0] = intersection1;
            intersections[1] = intersection2;

            return true;
        }
        public CoordD Center { get; set; }
        public double Radius { get; set; }

        public Circle(CoordD center, double radius)
        {
            Center = center;
            Radius = radius;
        }

        public bool InCircle(CoordD coord)
        {
            if (GetDistance(coord, Center) < Radius)
            {
                return true;;
            }
            return false;
        }

        public bool OnCircle(CoordD coord)
        {
            if (Radius - 1 < GetDistance(coord, Center) && GetDistance(coord, Center) < Radius)
            {
                return true; ;
            }
            return false;
        }
        private double GetDistance(Coord coord1, Coord coord2)
        {
            return Math.Sqrt(Math.Pow(coord1.x - coord2.x, 2) + Math.Pow(coord1.y - coord2.y, 2));
        }

        private double GetDistance(CoordD coord1, Coord coord2)
        {
            return Math.Sqrt(Math.Pow(coord1.x - coord2.x, 2) + Math.Pow(coord1.y - coord2.y, 2));
        }
        private double GetDistance(Coord coord1, CoordD coord2)
        {
            return Math.Sqrt(Math.Pow(coord1.x - coord2.x, 2) + Math.Pow(coord1.y - coord2.y, 2));
        }

        private double GetDistance(CoordD coord1, CoordD coord2)
        {
            return Math.Sqrt(Math.Pow(coord1.x - coord2.x, 2) + Math.Pow(coord1.y - coord2.y, 2));
        }
    }
}
