using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
            using StreamReader reader = new("files/Day06Input.txt");
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");
            var linesWithoutOperator = lines.SkipLast(1);

            string operatorLine = lines[lines.Length-1];

            int pointer = 0;
            double finalResult = 0;

            while (pointer < operatorLine.Length)
            {
                char curOperator = operatorLine[pointer];
                int posNextOperator = pointer + 1;
                bool foundOperator = false;
                for (posNextOperator = pointer + 1; posNextOperator < operatorLine.Length; posNextOperator++)
                {
                    if (operatorLine[posNextOperator] == '+' || operatorLine[posNextOperator] == '*')
                    {
                        foundOperator = true;
                        break;
                    }
                }

                int maxPosition = posNextOperator - 2;
                if (!foundOperator)
                {
                    maxPosition = posNextOperator - 1;
                }

                double tmpValue = 0;
                for (int curPos = maxPosition; curPos >= pointer; curPos--)
                {
                    string rowValue = "";
                    foreach (string line in linesWithoutOperator)
                    {
                        rowValue += line[curPos];
                    }


                    double parsedValue = double.Parse(rowValue);
                    if (curPos == maxPosition)
                    {
                        tmpValue = parsedValue;
                    }
                    else
                    {
                        switch (curOperator.ToString())
                        {
                            case "+":
                                tmpValue = tmpValue + parsedValue;
                                break;

                            case "*":
                                tmpValue = tmpValue * parsedValue;
                                break;
                        }
                    }
                }

                finalResult = finalResult + tmpValue;

                pointer = posNextOperator;
            }

            Console.WriteLine("Result Part2: "+ finalResult);
        }
    }
}
