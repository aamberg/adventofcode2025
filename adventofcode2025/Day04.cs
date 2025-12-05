using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2025
{
    internal class Day04
    {
        private int[,] gridPaper;

        public Day04()
        {
            this.FillgridFromFile();
        }

        private void FillgridFromFile()
        {
            using StreamReader reader = new("files/Day04Input.txt");

            // Read the stream as a string.
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");
            int yMax = lines.Length;
            

            string line = lines[0];
            char[] chars = line.ToCharArray();
            int xMax = chars.Length;

            gridPaper = new int[yMax, xMax];

            int curX = 0;
            int curY = 0;
            foreach (var tmpLine in lines)
            {
                char[] tmpChars = tmpLine.ToCharArray();

                foreach (char tmpChar in tmpChars)
                {
                    int tmpVal = 0;
                    if (tmpChar.ToString() == "@")
                    {
                        tmpVal = 1;
                    }
                    gridPaper[curY, curX] = tmpVal;
                    curX++;
                }
                curX = 0;
                curY++;
            }
        }

        public void SolvePart1()
        {
            int countOfMovablePaper = 0;
            int rows = gridPaper.GetLength(0);
            int cols = gridPaper.GetLength(1);
            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < cols; x++)
                {
                    if (gridPaper[y, x] == 1)
                    {
                        int numAdjacentPaper = GetNumOfAdjacentPaper(y, x);
                        if (numAdjacentPaper < 4)
                        {
                            countOfMovablePaper++;
                        }
                    }
                }
            }

            Console.WriteLine(countOfMovablePaper);
        }

        public void SolvePart2()
        {
            int countOfMovablePaper = 0;
            int rows = gridPaper.GetLength(0);
            int cols = gridPaper.GetLength(1);
            bool keepsearching = true;

            while (keepsearching)
            {
                keepsearching = false;
                for (int y = 0; y < rows; y++)
                {
                    for (int x = 0; x < cols; x++)
                    {
                        if (gridPaper[y, x] == 1)
                        {
                            int numAdjacentPaper = GetNumOfAdjacentPaper(y, x);
                            if (numAdjacentPaper < 4)
                            {
                                countOfMovablePaper++;
                                gridPaper[y, x] = 0;
                                keepsearching = true;
                            }
                        }
                    }
                }
            }

            Console.WriteLine(countOfMovablePaper);
            drawPart02ToFile("files/FinalOutputDay04.txt");            
        }

        private void drawPart02ToFile(string filename)
        {
            int rows = gridPaper.GetLength(0);
            int cols = gridPaper.GetLength(1);
            using StreamWriter writer = new(filename);
            for (int y = 0; y < rows; y++)
            {
                string line = "";
                for (int x = 0; x < cols; x++)
                {
                    string charc = ".";
                    if (gridPaper[y, x] == 1)
                    {
                        charc = "@";
                    }
                    line += charc;
                }
                writer.WriteLine(line);
            }
        }

        private int GetNumOfAdjacentPaper(int yPos, int xPos)
        {
            int numAdjacentPaper = 0;
            for (int tmpY = yPos-1; tmpY <= yPos + 1; tmpY++)
            {
                for (int tmpX = xPos-1; tmpX <= xPos + 1; tmpX++)
                {
                    if (tmpY == yPos && tmpX == xPos)
                    {
                        continue;
                    }

                    if (GetValueOfPosition(tmpY, tmpX) != 0)
                    {
                        numAdjacentPaper++;
                    }
                }
            }


            return numAdjacentPaper;
        }

        private int GetValueOfPosition(int yPos, int xPos)
        {
            int rows = gridPaper.GetLength(0);
            int cols = gridPaper.GetLength(1);
            int value = 0;

            if (yPos >= 0 && yPos < rows && xPos >= 0 && xPos < cols)
            {
                value = gridPaper[yPos, xPos];
            }
            

            return value;
        }
    }
}
