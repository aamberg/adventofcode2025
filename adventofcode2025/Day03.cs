using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2025
{
    internal class Day03 : IDay
    {
        public void SolvePart1()
        {
            using StreamReader reader = new("files/Day03Input.txt");
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");
            double counter = 0;

            foreach (string line in lines)
            {
                counter += getHighestBattery(line, 2);
            }

            Console.WriteLine("Result Part1: " + counter);
        }

        public void SolvePart2()
        {
            using StreamReader reader = new("files/Day03Input.txt");
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");
            double counter = 0;

            foreach (string line in lines)
            {
                counter += getHighestBattery(line, 12);
            }

            Console.WriteLine("Result Part2: " + counter);
        }
        private (int, int) getHighest(string line, int from, int to)
        {
            int highestVal = 0;
            int highestPos = 0;
            for (int i = from; i < to; i++)
            {
                int curVal = int.Parse(line[i].ToString());

                if (curVal > highestVal)
                {
                    highestVal = curVal;
                    highestPos = i;
                    if (curVal == 9)
                    {
                        break;
                    }
                }
            }

            return (highestVal, highestPos);
        }

        private double getHighestBattery(string line, int maxLength)
        {
            int highestVal = 0;
            int highestPos = -1;
            double batteryMax = 0;

            for (int i = 0; i < maxLength; i++)
            {
                int lineEnd = (line.Length - (maxLength - 1) + i);
                (highestVal, highestPos) = getHighest(line, highestPos + 1, lineEnd);

                // kinda ugly
                batteryMax = double.Parse("" + batteryMax + highestVal);
            }

            return batteryMax;
        }
    }
}
