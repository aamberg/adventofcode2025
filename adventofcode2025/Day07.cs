using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace adventofcode2025
{
    internal class Day07 : IDay
    {
        private string[,] gridTachyon;

        public void SolvePart1()
        {
            fillTachyonGrid();
            int consoleLeftPos = Console.CursorLeft;
            int consoleTopPos = Console.CursorTop;
            if (Environment.GetEnvironmentVariable("DOTNET_MODIFIABLE_ASSEMBLIES") != "debug")
            {
                outputGridToConsole(0);
            }

            int result = startTachyon();

            if (Environment.GetEnvironmentVariable("DOTNET_MODIFIABLE_ASSEMBLIES") != "debug")
            {
                Console.CursorLeft = consoleLeftPos;
                Console.CursorTop = consoleTopPos;
                outputGridToConsole(100, true);
            }

            Console.WriteLine("Result Part1: " + result);
        }

        public void SolvePart2()
        {
            fillTachyonGrid();
            double result = startTachyonDimesionCounter();

            Console.WriteLine("Result Part2: " + result);
        }

        private int startTachyon()
        {
            int counterBeamSplits = 0;
            for (int y = 0; y < gridTachyon.GetLength(0); y++)
            {
                for (int x = 0; x < gridTachyon.GetLength(1); x++)
                {
                    if ("S" == gridTachyon[y, x])
                    {
                        gridTachyon[y + 1, x] = "|";
                    }
                    else if ("|" == gridTachyon[y, x])
                    {
                        if (isPositionAvaible(y + 1, x) && gridTachyon[y + 1, x] == "^")
                        {
                            counterBeamSplits++;
                            if (isPositionAvaible(y + 1, x - 1))
                            {
                                gridTachyon[y + 1, x - 1] = "|";
                            }

                            if (isPositionAvaible(y + 1, x + 1))
                            {
                                gridTachyon[y + 1, x + 1] = "|";
                            }
                        }
                        else if (isPositionAvaible(y + 1, x))
                        {
                            gridTachyon[y + 1, x] = "|";
                        }
                    }
                }
            }

            return counterBeamSplits;
        }

        private double startTachyonDimesionCounter()
        {
            double counterDimensions = 0;
            double[,] valuesOfField = new double[gridTachyon.GetLength(0), gridTachyon.GetLength(1)];
            double valueOfField = 0;

            for (int y = 0; y < gridTachyon.GetLength(0); y++)
            {
                for (int x = 0; x < gridTachyon.GetLength(1); x++)
                {
                    valueOfField = Math.Max(valuesOfField[y, x], 1);

                    if ("S" == gridTachyon[y, x])
                    {
                        gridTachyon[y + 1, x] = "|";
                    }
                    else if ("|" == gridTachyon[y, x])
                    {
                        if (isPositionAvaible(y + 1, x) && gridTachyon[y + 1, x] == "^")
                        {
                            List<(int y, int x)> values = new List<(int y, int x)>();
                            values.Add((y + 1, x - 1));
                            values.Add((y + 1, x + 1));
                            foreach (var (checkY, checkX) in values)
                            {
                                if (isPositionAvaible(checkY, checkX))
                                {
                                    if (gridTachyon[checkY, checkX] == "|")
                                    {
                                        valuesOfField[checkY, checkX] = Math.Max(valuesOfField[checkY, checkX], 1) + valueOfField;
                                    }
                                    else
                                    {
                                        valuesOfField[checkY, checkX] = valueOfField;
                                    }

                                    gridTachyon[checkY, checkX] = "|";
                                }
                            }
                        }
                        else if (isPositionAvaible(y + 1, x))
                        {
                            gridTachyon[y + 1, x] = "|";
                            valuesOfField[y + 1, x] = Math.Max(valuesOfField[y + 1, x], 0) + valueOfField;
                        }
                        else
                        {
                            counterDimensions = counterDimensions + valueOfField;
                        }
                    }
                }
            }

            return counterDimensions;
        }

        private bool isPositionAvaible(int yPos, int xPos)
        {
            return yPos >= 0 && yPos < gridTachyon.GetLength(0) && xPos >= 0 && xPos < gridTachyon.GetLength(1);
        }

        private void fillTachyonGrid()
        {
            using StreamReader reader = new("files/Day07Input.txt");

            // Read the stream as a string.
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");
            int yMax = lines.Length;
            int xMax = lines[0].Length;

            gridTachyon = new string[yMax, xMax];

            for (int y = 0; y < yMax; y++)
            {
                for (int x = 0; x < xMax; x++)
                {
                    gridTachyon[y, x] = lines[y][x].ToString();
                }
            }
        }

        private void outputGridToConsole(int delay = 200, bool withColor = false)
        {
            string wholeLine = "";

            for (int y = 0; y < gridTachyon.GetLength(0); y++)
            {
                wholeLine = "";
                for (int x = 0; x < gridTachyon.GetLength(1); x++)
                {
                    string chr = gridTachyon[y, x];

                    if (withColor && chr == "^")
                    {
                        chr = "[red]^[/]";
                    }
                    else if (withColor && chr == "|")
                    {
                        chr = "[yellow]|[/]";
                    }
                    else if (withColor && chr == "S")
                    {
                        chr = "[green]S[/]";
                    }
                    else if (chr == ".")
                    {
                        chr = " ";
                    }

                    wholeLine = wholeLine + chr;
                }

                var greetingsOutput = new Markup($"{wholeLine}\n");
                AnsiConsole.Write(greetingsOutput);
                Thread.Sleep(delay);
            }
        }
    }
}
