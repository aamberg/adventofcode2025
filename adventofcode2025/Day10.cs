using adventofcode2025.subClasses;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static adventofcode2025.subClasses.Bfs;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace adventofcode2025
{
    internal class Day10 : IDay
    {
        public void SolvePart1()
        {
            using StreamReader reader = new("files/Day10Input.txt");
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");

            int totalRowCount = 0;
            foreach (string line in lines)
            {
                totalRowCount += calcMinButtonPushes(line);
            }

            Console.WriteLine("Result Part1: " + totalRowCount);
        }

        private int calcMinButtonPushes(string input)
        {
            int minCount = 0;
            string[] lines = input.Split(" ");
            string desiredIndicator = lines[0].Remove(lines[0].Length - 1, 1).Remove(0, 1);
            string currentIndicator = "";
            for (int i = 0; i < desiredIndicator.Length; i++)
            {
                currentIndicator += ".";
            }

            List<int[]> buttons = new List<int[]>();

            // skip first (indicator lights) and last element (joltage)
            foreach (string line in lines.Skip(1).SkipLast(1))
            {
                int[] buttonParts = line.Remove(line.Length - 1, 1).Remove(0, 1).Split(",").Select(int.Parse).ToArray();
                buttons.Add(buttonParts);
            }

            int result = minCalculator(buttons, desiredIndicator);

            return result;
        }

        private int minCalculator(List<int[]> givenButtons, string desiredIndicator)
        {
            int N = givenButtons.Count;

            int totalCombinations = 1 << N;
            int minPresses = 99;

            string emptyIndicator = "";
            for (int i = 0; i < desiredIndicator.Length; i++)
            {
                emptyIndicator += ".";
            }

            List<int> usedItemIndices = new List<int>();
            for (int i = 1; i < totalCombinations; i++)
            {
                usedItemIndices = new List<int>();
                string tmpIndicator = emptyIndicator;

                for (int j = 0; j < N; j++)
                {
                    if ((i & (1 << j)) != 0)
                    {
                        tmpIndicator = pressButton(tmpIndicator, givenButtons[j]);
                        usedItemIndices.Add(j);
                    }
                }

                if (tmpIndicator == desiredIndicator && usedItemIndices.Count < minPresses)
                {
                    minPresses = usedItemIndices.Count;
                }
            }

            return minPresses;
        }


        private string pressButton(string currecIndicator, int[] button)
        {
            char[] stringParts = currecIndicator.ToCharArray();
            foreach (int buttonPart in button)
            {
                if (stringParts[buttonPart] == '.')
                {
                    stringParts[buttonPart] = '#';
                }
                else
                {
                    stringParts[buttonPart] = '.';
                }
            }

            return new string(stringParts);
        }


        public void SolvePart2()
        {
            Console.WriteLine("Result Part2: ");
        }
    }
}
