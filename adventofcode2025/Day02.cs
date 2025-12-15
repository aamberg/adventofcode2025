using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2025
{
    internal class Day02 : IDay
    {
        public void SolvePart1()
        {
            using StreamReader reader = new("files/Day02Input.txt");
            string text = reader.ReadToEnd();
            string[] ranges = text.Split(",");

            double counterEqual = 0;
            foreach (string range in ranges)
            {
                string[] parts = range.Split("-");
                double low = double.Parse(parts[0]);
                double high = double.Parse(parts[1]);

                for (double i = low; i <= high; i++)
                {
                    string iAsString = i.ToString();
                    if (iAsString.Length % 2 != 0)
                    {
                        continue;
                    }

                    string firstHalf = iAsString.Substring(0, iAsString.Length / 2);
                    string secondHalf = iAsString.Substring(iAsString.Length / 2);
                    if (secondHalf == firstHalf)
                    {
                        counterEqual += double.Parse(iAsString);
                    }
                }

            }

            Console.WriteLine("Result Part1: " + counterEqual);
        }

        public void SolvePart2()
        {
            using StreamReader reader = new("files/TestDay02Input.txt");
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");

            int totalRowCount = 0;
            foreach (string line in lines)
            {

            }

            Console.WriteLine("Result Part2: ");
        }
    }
}
