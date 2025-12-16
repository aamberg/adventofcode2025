using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2025
{
    internal class Day12 : IDay
    {
        private class PuzzlePiece
        {
            public string[,] grid { get; set; }
        }
        private class QuizRow
        {
            public int dimensionX { get; set; }
            public int dimensionY { get; set; }

            public List<int> counters { get; set; }
        }

        private static List<PuzzlePiece> pieces = new List<PuzzlePiece>();

        private static List<QuizRow> quizRows = new List<QuizRow>();

        public void SolvePart1()
        {
            initStuff();
            int rowsFitted = 0;

            foreach (QuizRow row in quizRows)
            {
                if (stupidSolverCanRowFitAllItems(row))
                {
                    rowsFitted++;
                }
            }

            Console.WriteLine("Result Part1: " + rowsFitted);
        }

        private bool stupidSolverCanRowFitAllItems(QuizRow row)
        {
            // resize grid to %3 by %3
            double itemsPerRow = Math.Floor((double)row.dimensionX / 3);
            double itemsMultiplier = Math.Floor((double)row.dimensionY / 3);

            double totalItemsToPack = 0;
            foreach (int tmpCounter in row.counters)
            {
                totalItemsToPack += tmpCounter;
            }

            return (totalItemsToPack < (itemsPerRow * itemsMultiplier));
        }

        public void SolvePart2()
        {
            Console.WriteLine("Result Part1: ");
        }

        private void initStuff()
        {
            using StreamReader reader = new("files/Day12Input.txt");
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");

            int tmpIndex = 0;
            List<string> tmpGrid = new List<string>();

            foreach (string line in lines)
            {
                if (line.EndsWith(":"))
                {
                    tmpIndex = int.Parse(line.Substring(0, line.Length - 1));
                    continue;
                }
                else if (line == "")
                {
                    string[,] strGrid = new string[tmpGrid[0].Length, tmpGrid.Count()];
                    for (int y = 0; y < tmpGrid.Count(); y++)
                    {
                        for (int x = 0; x < tmpGrid[y].Length; x++)
                        {
                            strGrid[y, x] = tmpGrid[y][x].ToString();
                        }
                    }

                    PuzzlePiece piece = new PuzzlePiece();
                    piece.grid = strGrid;
                    pieces.Add(piece);

                    tmpGrid.Clear();
                }
                else if (!line.EndsWith(":") && line.Contains(":"))
                {
                    // checker
                    string[] parts = line.Split(" ");
                    string dimensions = parts[0].Substring(0, parts[0].Length - 1);

                    List<int> tmpCounters = new List<int>();
                    foreach (string counter in parts.Skip(1))
                    {
                        tmpCounters.Add(int.Parse(counter));
                    }

                    string[] splitDimension = dimensions.Split("x");

                    QuizRow tmpRow = new QuizRow();
                    tmpRow.dimensionX = int.Parse(splitDimension[0]);
                    tmpRow.dimensionY = int.Parse(splitDimension[1]);
                    tmpRow.counters = tmpCounters;

                    quizRows.Add(tmpRow);
                }
                else
                {
                    tmpGrid.Add(line);
                }
            }
        }
    }
}
