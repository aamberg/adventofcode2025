using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2025
{
    internal class Day09 : IDay
    {

        public void SolvePart1()
        {
            using StreamReader reader = new("files/Day09Input.txt");
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");

            List<(double xCord, double yCord)> redTiles = new List<(double xCord, double yCord)>();

            double maxTileSite = 0;
            foreach (string line in lines)
            {
                (double xPos, double yPos) = Deconstruct(line.Split(","));
                foreach (var (tmpX, tmpY) in redTiles)
                {
                    double size = (Math.Abs(xPos - tmpX) + 1) * (Math.Abs(yPos - tmpY) + 1);
                    maxTileSite = Math.Max(maxTileSite, size);
                }

                redTiles.Add((xPos, yPos));
            }

            Console.WriteLine("Result Part1: " + maxTileSite);
        }

        public void SolvePart2()
        {
            Console.WriteLine("Result Part2: ");
        }

        private (double, double) Deconstruct(string[] parts)
        {
            return (double.Parse(parts[0]), double.Parse(parts[1]));
        }
    }
}
