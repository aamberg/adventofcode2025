using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2025
{
    internal class Day05 : IDay
    {
        private List<double> freshItems = new List<double>();

        private List<(double from, double to)> freshItemRanges = new List<(double from, double to)>();

        public void SolvePart1()
        {
            readFileAnTransform();
            Console.WriteLine("Result Part1: "+ freshItems.Count);
        }

        public void SolvePart2()
        {
            Console.WriteLine("Result Part2: ");
        }

        private void readFileAnTransform()
        {
            using StreamReader reader = new("files/Day05Input.txt");

            // Read the stream as a string.
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");

            bool emtpyLineFound = false;
            foreach (string line in lines)
            {
                if (line == "")
                {
                    emtpyLineFound = true;
                    continue;
                }

                if (!emtpyLineFound)
                {
                    string[] parts = line.Split("-");

                    double from = double.Parse(parts[0]);
                    double to = double.Parse(parts[1]);

                    freshItemRanges.Add((from, to));
                }
                else
                {
                    double item = double.Parse(line);
                    if (checkIfItemIsFresh(item))
                    {
                        freshItems.Add(item);
                    }
                }
            }
        }

        private bool checkIfItemIsFresh(double item)
        {
            bool isFresh = false;
            foreach (var (from, to) in freshItemRanges) 
            {
                if (item >= from && item <=to)
                {
                    isFresh = true;
                    break;
                }
            }

            return isFresh;
        }
    }
}
