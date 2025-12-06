using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adventofcode2025
{
    internal class Day06 : IDay
    {
        private string[,] grid;

        public void SolvePart1()
        {
            using StreamReader reader = new("files/Day06Input.txt");
            string text = reader.ReadToEnd();

            Regex trimmer = new Regex(@"\s\s+");
            
            string[] lines = text.Split("\n");

            string cleanFirstLine = trimmer.Replace(lines[0], " ").Trim();
            string[] partsFirstLine = cleanFirstLine.Split(" ");

            grid = new string[lines.Length, partsFirstLine.Length];

            int yPos = 0;
            int xPos = 0;
            foreach (string line in lines)
            {
                string cleanLine = trimmer.Replace(line, " ").Trim();
                string[] parts = cleanLine.Split(" ");
                foreach (string part in parts)
                {
                    grid[yPos, xPos] = part;
                    xPos++;
                }

                yPos++;
                xPos = 0;
            }

            double finalResult = 0;

            for (int curPosX = 0; curPosX < partsFirstLine.Length; curPosX++)
            {
                string operation = grid[lines.Length - 1, curPosX];
                double tmpVal = 0;

                for (int curPosY = 0; curPosY < lines.Length-1; curPosY++)
                {
                    if (curPosY == 0)
                    {
                        tmpVal = int.Parse(grid[curPosY, curPosX]);
                    }
                    else
                    {
                        switch (operation)
                        {
                            case "+":
                                tmpVal = tmpVal + int.Parse(grid[curPosY, curPosX]);
                                break;
                            case "*":
                                tmpVal = tmpVal * int.Parse(grid[curPosY, curPosX]);
                                break;
                        }
                    }
                }

                finalResult = finalResult + tmpVal; 
            }

            Console.WriteLine("Result Part1: "+ finalResult);
        }

        public void SolvePart2()
        {
            Console.WriteLine("Result Part2: ");
        }
    }
}
