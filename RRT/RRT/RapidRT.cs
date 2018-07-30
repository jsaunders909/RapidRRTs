using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RRT
{
    public struct Coord
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Coord(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    public class RapidRT
    {
        List<Coord> seenList;
        public Tuple<Coord, Coord> mostRecentEdge;
        int width;
        int length;
        Random random;
        int stepSize;

        public RapidRT(int width, int length, int stepSize, Coord startPoint)
        {
            this.width = width;
            this.length = length;
            seenList = new List<Coord>() { startPoint };
            random = new Random();
            this.stepSize = stepSize;
        }

        private Coord GetRandomCoord()
        {
            int x = random.Next(0, width);
            int y = random.Next(0, length);
            return new Coord(x, y);
        }
        public double GetDistance(Coord coord1, Coord coord2)
        {
            int x = coord1.X - coord2.X;
            int y = coord1.Y - coord2.Y;
            return Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
        }

        private Coord GetNearest(Coord coord)
        {
            double mindistance = double.PositiveInfinity;
            Coord bestCoord = seenList[seenList.Count - 1];
            foreach (Coord c in seenList)
            {
                double distance = GetDistance(c, coord);
                if (distance < mindistance && distance != 0)
                {
                    mindistance = distance;
                    bestCoord = c;
                }
            }
            return bestCoord; 
        }

        private Coord StepInDirection(Coord nearestCoord, Coord randomCoord)
        {
            int x = randomCoord.X - nearestCoord.X;
            int y = randomCoord.Y - nearestCoord.Y;

            double distance = GetDistance(randomCoord, nearestCoord);
            double stepFactor = stepSize / distance;

            return new Coord((int)(nearestCoord.X + (x * stepFactor)), (int)(nearestCoord.Y + (y * stepFactor)));
        }

        public void RunIteration()
        {
            Coord randomCoord = GetRandomCoord();
            Coord nearestCoord = GetNearest(randomCoord);
            Coord newCoord = StepInDirection(nearestCoord, randomCoord);
            mostRecentEdge =  new Tuple<Coord, Coord>(newCoord, nearestCoord);
            if (newCoord.X < -1000)
            {
                newCoord.X++;
            }
            seenList.Add(newCoord);
        }
    }
}
