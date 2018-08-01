using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnderstandingRRTs
{
    public struct Coord
    {
        public int x { get; set; }
        public int y { get; set; }
        public double heading { get; set; }
        public Coord(int x, int y, double heading)
        {
            this.x = x;
            this.y = y;
            this.heading = heading;
        }

    }
    public class RRT
    {
        Coord start;
        int[,] selected;
        List<Coord> seen;
        Random random = new Random();
        int width;
        int height;
        List<Tuple<Coord, Coord>> edges = new List<Tuple<Coord, Coord>>();

        IMetric metric = new EuclideanDistanceMetric();
        int speed = 10;
        int turnSpeed = 2;
        double maxTurn;

        public Tuple<Coord, Coord> mostRecentEdge;
        public int[,] obsitcleMap;
        public bool found = false;

        public RRT(int width, int height, Coord start)
        {
            this.start = start;
            maxTurn = speed * turnSpeed *  Math.PI / 180;
            this.width = width;
            this.height = height;
            seen = new List<Coord>() { start};
            mostRecentEdge = new Tuple<Coord, Coord>(start, start);
            selected = new int [width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    selected[i, j] = 0;
                }
            }
            ObsitcleMap om = new ObsitcleMap(width, height);
            obsitcleMap = om.obsticles;
        }

        private Coord GetRandomCoord()
        {
            int x = random.Next(width);
            int y = random.Next(height);
            return new Coord(x, y, Math.PI * 2 * random.NextDouble());
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

        private Coord GetNearestNeighbour(Coord coord)
        {
            double minDistance = double.PositiveInfinity;
            Coord nearest = new Coord(250, 250, 0);
            foreach(Coord seenCoord in seen)
            {
                double distance = metric.GetDistance(seenCoord, coord);
                if (distance < minDistance && distance != 0)
                {
                    minDistance = distance;
                    nearest = seenCoord;
                }
            }
            return nearest;
        }

        public void RunIteration()
        {
            Coord randomCoord = GetRandomCoord();
            Coord nearest = GetNearestNeighbour(randomCoord);
            Coord newCoord = InDirection(nearest, randomCoord, speed);
            if (Clear(nearest, newCoord) && newCoord.x != -1)
            {
                if (Goal(nearest, newCoord)) { found = true; }
                seen.Add(newCoord);
                edges.Add(new Tuple<Coord, Coord>(nearest, newCoord));
                mostRecentEdge = new Tuple<Coord, Coord>(nearest, newCoord);
            }

        }

        public List<Coord> GetReversed(Coord endPoint)
        {
            List<Coord> reversedList = new List<Coord>() { endPoint };
            Coord here = endPoint;
            while (here.x != start.x || here.y != start.y)
            {
                foreach (Tuple<Coord, Coord> edge in edges)
                {
                    if (here.x == edge.Item2.x && here.y == edge.Item2.y)
                    {
                        here = edge.Item1;
                        reversedList.Add(here);
                        break;
                    }
                }

            }
            return reversedList;

        }

        private Coord InDirection(Coord startPoint, Coord endPoint, double distance)
        {
            double totalDist = GetDistance(startPoint, endPoint);
            double startHeading = startPoint.heading;
            double heading;
            int besti = -2;
            double bestMoveCost = double.PositiveInfinity;
            Coord bestMove = new Coord(-1,-1,-1);
            for (int i = -1; i <= 1; i++)
            {
                heading = startHeading + i * maxTurn;
                double xStep = distance  * Math.Cos(heading);
                double yStep = distance  * Math.Sin(heading);
                Coord candidatePos = new Coord((int)(startPoint.x + xStep), (int)(startPoint.y + yStep), heading);
                double candidateCost = metric.GetDistance(candidatePos, endPoint);
                if (candidateCost < bestMoveCost )
                {
                    bestMoveCost = candidateCost;
                    bestMove = candidatePos;
                    besti = i;
                }
            }
            return bestMove;
        }
        private bool Clear(Coord coord1, Coord coord2)
        {
            CoordD currentPos = coord1;
            CoordD change = new CoordD(coord2.x - coord1.x, coord2.y - coord1.y) / GetDistance(coord1, coord2);

            for (int i =0; i <= GetDistance(coord1, coord2); i++)
            {
                currentPos = coord1 + change * i;
                Coord integerCoord = currentPos;
                if (integerCoord.x >= 500 || integerCoord.x < 0 || integerCoord.y >= 500 || integerCoord.y < 0) { return false; }
                bool x = (obsitcleMap[integerCoord.x, integerCoord.y] == -1);
                if (x == true) { return false; }
            }
            return true;

        }

        private bool Goal(Coord coord1, Coord coord2)
        {
            CoordD currentPos = coord1;
            CoordD change = new CoordD(coord2.x - coord1.x, coord2.y - coord1.y) / GetDistance(coord1, coord2);

            for (int i = 0; i <= GetDistance(coord1, coord2); i++)
            {
                currentPos = coord1 + change * i;
                Coord integerCoord = currentPos;
                if (integerCoord.x >= 500 || integerCoord.x < 0 || integerCoord.y >= 500 || integerCoord.y < 0) { return false; }
                bool x = (obsitcleMap[integerCoord.x, integerCoord.y] == 1);
                if (x == true) { return true; }
            }
            return false;

        }

    }
}
