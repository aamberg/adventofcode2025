using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2025
{
    internal class Day08 : IDay
    {
        public void SolvePart1()
        {
            using StreamReader reader = new("files/Day08Input.txt");
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");

            List<(int xCord, int yCord, int zCord)> boxes = new List<(int xCord, int yCord, int zCord)>();
            List<(int indexLow, int indexHigh, double distance)> sorter = new List<(int indexLow, int indexHigh, double distance)>();

            int counter = 0;

            foreach (string line in lines)
            {
                string[] splitCoords = line.Split(",");
                boxes.Add((int.Parse(splitCoords[0]), int.Parse(splitCoords[1]), int.Parse(splitCoords[2])));

                for (int tmpI = 0; tmpI < counter; tmpI++)
                {
                    double distance = calcDistance(boxes[tmpI].xCord, boxes[tmpI].yCord, boxes[tmpI].zCord, boxes[counter].xCord, boxes[counter].yCord, boxes[counter].zCord);
                    sorter.Add((tmpI, counter, distance));
                }

                counter++;
            }

            sorter = sorter.OrderBy(x => x.distance).ToList();

            List<int[]> circuits = new List<int[]>();

            foreach (var (indexLow, indexHigh, distance) in sorter.GetRange(0, 1000))
            {
                int[] tmpBox = new int[] { indexLow, indexHigh };

                if (circuits.FindIndex(x => tmpBox.All(x.Contains)) >= 0)
                {
                    continue;
                }
                else
                {
                    List<int[]> all = circuits.FindAll(x => tmpBox.Any(x.Contains));

                    if (all.Count == 2)
                    {
                        int c0 = circuits.FindIndex(x => all[0].All(x.Contains));
                        int c1 = circuits.FindIndex(x => all[1].All(x.Contains));
                        tmpBox = circuits[c0].Union(circuits[c1]).ToArray();

                        if (c0 > c1)
                        {
                            circuits.RemoveAt(c0);
                            circuits.RemoveAt(c1);
                        }
                        else
                        {
                            circuits.RemoveAt(c1);
                            circuits.RemoveAt(c0);
                        }
                    }
                    else if (all.Count == 1)
                    {
                        int c = circuits.FindIndex(x => all[0].All(x.Contains));
                        tmpBox = circuits[c].Union(tmpBox).ToArray();
                        circuits.RemoveAt(c);
                    }
                }

                circuits.Add(tmpBox);
            }

            circuits = circuits.OrderByDescending(x => x.Count()).ToList();
            int result = 1;
            foreach (int[] item in circuits.GetRange(0, 3))
            {
                result = result * item.Count();
            }

            Console.WriteLine("Result Part1: " + result);
        }

        private double calcDistance(int x1, int y1, int z1, int x2, int y2, int z2)
        {
            double distance = 0;

            distance = Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2) + Math.Pow(z1 - z2, 2);

            return distance;
        }

        public void SolvePart2()
        {
            using StreamReader reader = new("files/Day08Input.txt");
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");

            List<(int xCord, int yCord, int zCord)> boxes = new List<(int xCord, int yCord, int zCord)>();
            List<(int indexLow, int indexHigh, double distance)> sorter = new List<(int indexLow, int indexHigh, double distance)>();

            int counter = 0;

            foreach (string line in lines)
            {
                string[] splitCoords = line.Split(",");
                boxes.Add((int.Parse(splitCoords[0]), int.Parse(splitCoords[1]), int.Parse(splitCoords[2])));

                for (int tmpI = 0; tmpI < counter; tmpI++)
                {
                    double distance = calcDistance(boxes[tmpI].xCord, boxes[tmpI].yCord, boxes[tmpI].zCord, boxes[counter].xCord, boxes[counter].yCord, boxes[counter].zCord);
                    sorter.Add((tmpI, counter, distance));
                }

                counter++;
            }

            sorter = sorter.OrderBy(x => x.distance).ToList();

            List<int[]> circuits = new List<int[]>();
            int result = 0;

            foreach (var (indexLow, indexHigh, distance) in sorter)
            {
                int[] tmpBox = new int[] { indexLow, indexHigh };

                if (circuits.FindIndex(x => tmpBox.All(x.Contains)) >= 0)
                {
                    continue;
                }
                else
                {
                    List<int[]> all = circuits.FindAll(x => tmpBox.Any(x.Contains));

                    if (all.Count == 2)
                    {
                        int c0 = circuits.FindIndex(x => all[0].All(x.Contains));
                        int c1 = circuits.FindIndex(x => all[1].All(x.Contains));
                        tmpBox = circuits[c0].Union(circuits[c1]).ToArray();

                        if (c0 > c1)
                        {
                            circuits.RemoveAt(c0);
                            circuits.RemoveAt(c1);
                        }
                        else
                        {
                            circuits.RemoveAt(c1);
                            circuits.RemoveAt(c0);
                        }
                    }
                    else if (all.Count == 1)
                    {
                        int c = circuits.FindIndex(x => all[0].All(x.Contains));
                        tmpBox = circuits[c].Union(tmpBox).ToArray();
                        circuits.RemoveAt(c);
                    }
                }

                circuits.Add(tmpBox);
                if (circuits.Count == 1 && circuits[0].Count() == boxes.Count)
                {
                    result = boxes[indexLow].xCord * boxes[indexHigh].xCord;
                    break;
                }
            }

            Console.WriteLine("Result Part2: " + result);
        }
    }
}
