using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingRRTs
{
    public class DubinsDistance : IMetric
    {
        private double turnRadius;

        public DubinsDistance(double turnRadius)
        {
            this.turnRadius = turnRadius;
        }

        public double GetDistance(Coord coord1, Coord coord2)
        {
            CoordD centreOfCircle1 = new CoordD((coord1.x - (turnRadius * Math.Cos(coord1.heading))), (coord1.y + (turnRadius * Math.Sin(coord1.heading))));
            CoordD centreOfCircle2 = new CoordD((coord1.x + (turnRadius * Math.Cos(coord1.heading))), (coord1.y - (turnRadius * Math.Sin(coord1.heading))));

            double lengthOfTangent1 = Math.Sqrt(Math.Pow(GetEuclidDistance(centreOfCircle1, coord2), 2) - (turnRadius * turnRadius));
            Circle turnCircle1 = new Circle(centreOfCircle1, turnRadius);
            CoordD midpoint = new CoordD(coord2.x + 0.5 * (turnCircle1.Center.x - coord2.x), (coord2.y + 0.5 * (turnCircle1.Center.y - coord2.y)));

            Circle constructedCircle1 = new Circle(midpoint, GetEuclidDistance(midpoint, centreOfCircle1));
            CoordD[] interestions1;
            if (!Circle.Intersects(turnCircle1, constructedCircle1, out interestions1))
            {
                return double.PositiveInfinity;
            }

            double gradient1_1 = (centreOfCircle1.x - interestions1[0].x) / (interestions1[0].y - centreOfCircle1.y);
            double gradient1_2 = (centreOfCircle1.x - interestions1[1].x) / (interestions1[1].y - centreOfCircle1.y);

            double arcLength1_1 = turnRadius * (Math.PI - coord1.heading - Math.Atan(1 / gradient1_1));
            double arcLength1_2 = turnRadius * (Math.PI - coord1.heading - Math.Atan(1 / gradient1_2));
            double candidate1 = lengthOfTangent1 + Math.Max(arcLength1_1, arcLength1_2);


            double lengthOfTangent2 = Math.Sqrt(Math.Pow(GetEuclidDistance(centreOfCircle2, coord2), 2) - (turnRadius * turnRadius));
            Circle turnCircle2 = new Circle(centreOfCircle2, turnRadius);
            CoordD midpoint2 = new CoordD(coord2.x + 0.5 * (turnCircle2.Center.x - coord2.x), (coord2.y + 0.5 * (turnCircle2.Center.y - coord2.y)));

            Circle constructedCircle2 = new Circle(midpoint2, GetEuclidDistance(midpoint2, centreOfCircle2));
            CoordD[] interestions2;
            if (!Circle.Intersects(turnCircle2, constructedCircle2, out interestions2))
            {
                return double.PositiveInfinity;
            }

            double gradient2_1 = (centreOfCircle2.x - interestions2[0].x) / (interestions2[0].y - centreOfCircle2.y);
            double gradient2_2 = (centreOfCircle2.x - interestions2[1].x) / (interestions2[1].y - centreOfCircle2.y);

            double arcLength2_1 = turnRadius * (Math.PI - coord1.heading - Math.Atan(1 / gradient1_1));
            double arcLength2_2 = turnRadius * (Math.PI - coord1.heading - Math.Atan(1 / gradient1_2));
            double candidate2 = lengthOfTangent2 + Math.Max(arcLength2_1, arcLength2_2);

            return Math.Min(candidate1, candidate2);

        }

        private double GetEuclidDistance(Coord coord1, Coord coord2)
        {
            return Math.Sqrt(Math.Pow(coord1.x - coord2.x, 2) + Math.Pow(coord1.y - coord2.y, 2));
        }
        private double GetEuclidDistance(CoordD coord1, Coord coord2)
        {
            return Math.Sqrt(Math.Pow(coord1.x - coord2.x, 2) + Math.Pow(coord1.y - coord2.y, 2));
        }
        private double GetEuclidDistance(CoordD coord1, CoordD coord2)
        {
            return Math.Sqrt(Math.Pow(coord1.x - coord2.x, 2) + Math.Pow(coord1.y - coord2.y, 2));
        }
    }
}
