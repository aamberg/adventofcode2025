namespace adventofcode2025
{
    internal class Day09 : IDay
    {

        public void SolvePart1()
        {
            using StreamReader reader = new("files/Day09Input.txt");
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");

            List<(double xCord, double yCord)> redTiles = new List<(double xCord, double yCord)>();

            double maxTileSite = 0;
            foreach (string line in lines)
            {
                (double xPos, double yPos) = Deconstruct(line.Split(","));
                foreach (var (tmpX, tmpY) in redTiles)
                {
                    double size = (Math.Abs(xPos - tmpX) + 1) * (Math.Abs(yPos - tmpY) + 1);
                    maxTileSite = Math.Max(maxTileSite, size);
                }

                redTiles.Add((xPos, yPos));
            }

            Console.WriteLine("Result Part1: " + maxTileSite);
        }

        public void SolvePart2()
        {
            using StreamReader reader = new("files/Day09Input.txt");
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");

            List<(double xCord, double yCord)> redTiles = new List<(double xCord, double yCord)>();
            List<(double x1Cord, double y1Cord, double x2Cord, double y2Cord, double size)> redTilesSizes = new List<(double x1Cord, double y1Cord, double x2Cord, double y2Cord, double size)>();
            string[] allowTilesReach = new string[100000];

            double size = 0;

            //var bmp = new Bitmap(1000,1000);
            //Graphics graphics = Graphics.FromImage(bmp);
            //graphics.Clear(System.Drawing.Color.White);
            //Pen bPen = new Pen(System.Drawing.Color.Black, 1);

            // load last line as first "last" position
            (double lastX, double lastY) = Deconstruct(lines[lines.Length - 1].Split(","));

            foreach (string line in lines)
            {
                (double xPos, double yPos) = Deconstruct(line.Split(","));

                //int drawX1 = int.Parse(xPos.ToString()) / 100;
                //int drawY1 = int.Parse(yPos.ToString()) / 100;
                //int drawX2 = int.Parse(lastX.ToString()) / 100;
                //int drawY2 = int.Parse(lastY.ToString()) / 100;

                //graphics.DrawLine(bPen, drawX1, drawY1, drawX2, drawY2);

                if (lastX == xPos)
                {
                    double smallerY = Math.Min(yPos, lastY);
                    double higherY = Math.Max(yPos, lastY);

                    for (double i = smallerY; i <= higherY; i++)
                    {
                        string tmpReach = allowTilesReach[doubleToInt(i)];
                        allowTilesReach[doubleToInt(i)] = calculateReach(tmpReach, xPos);
                    }
                }
                else
                {
                    double smallerX = Math.Min(lastX, xPos);
                    double higherX = Math.Max(lastX, xPos);

                    string tmpReach = allowTilesReach[doubleToInt(yPos)];
                    allowTilesReach[doubleToInt(yPos)] = calculateReach(tmpReach, smallerX, higherX);
                }

                foreach (var (tmpX, tmpY) in redTiles)
                {
                    size = (Math.Abs(xPos - tmpX) + 1) * (Math.Abs(yPos - tmpY) + 1);
                    redTilesSizes.Add((xPos, yPos, tmpX, tmpY, size));
                }

                lastX = xPos;
                lastY = yPos;
                redTiles.Add((xPos, yPos));
            }

            //bmp.Save("C:\\Users\\Home\\Desktop\\test.bmp", ImageFormat.Jpeg);

            redTilesSizes = redTilesSizes.OrderByDescending(x => x.size).ToList();

            double maxTileSite = 0, maxCoordX1 = 0, maxCoordY1 = 0, maxCoordX2 = 0, maxCoordY2 = 0;
            foreach (var redTilesSize in redTilesSizes)
            {
                if (checkAllFourLines(allowTilesReach, redTilesSize.x1Cord, redTilesSize.y1Cord, redTilesSize.x2Cord, redTilesSize.y2Cord))
                {
                    maxTileSite = redTilesSize.size;
                    maxCoordX1 = redTilesSize.x1Cord;
                    maxCoordY1 = redTilesSize.y1Cord;
                    maxCoordX2 = redTilesSize.x2Cord;
                    maxCoordY2 = redTilesSize.y2Cord;

                    break;
                }
            }

            Console.WriteLine("Result Part2: " + maxTileSite + " (coordinates: " + maxCoordX1 + "," + maxCoordY1 + ":" + maxCoordX2 + "," + maxCoordY2 + ")");
        }


        private bool checkAllFourLines(string[] allowTilesReach, double x1, double y1, double x2, double y2)
        {
            double xLow = Math.Min(x1, x2);
            double xHigh = Math.Max(x1, x2);
            double yLow = Math.Min(y1, y2);
            double yHigh = Math.Max(y1, y2);


            for (double i = xLow; i < xHigh; i = i + 1000)
            {
                if (i > xHigh)
                {
                    throw new Exception("");
                }
                string xReachLowRow = allowTilesReach[doubleToInt(yLow)];
                (double xTmpLow, double xTmpHigh) = Deconstruct(xReachLowRow.Split("-"));

                if (i < xTmpLow || i > xTmpHigh)
                {
                    return false;
                }

                string xReachTopRow = allowTilesReach[doubleToInt(yHigh)];
                (double xTmpLowLow, double xTmpHighHigh) = Deconstruct(xReachTopRow.Split("-"));

                if (i < xTmpLowLow || i > xTmpHighHigh)
                {
                    return false;
                }
            }

            for (double i = yLow; i < yHigh; i = i + 1000)
            {
                string yReachLowRow = allowTilesReach[doubleToInt(i)];
                (double xTmpLow, double xTmpHigh) = Deconstruct(yReachLowRow.Split("-"));

                if (!(xLow >= xTmpLow && xHigh <= xTmpHigh))
                {
                    return false;
                }
            }

            return true;
        }


        private int doubleToInt(double d)
        {
            return int.Parse(d.ToString());
        }

        private string calculateReach(string reach, double newXPos)
        {
            string newReach = reach;
            if (reach == null || reach == "")
            {
                newReach = "" + newXPos;
            }
            else if (!reach.Contains("-"))
            {
                double oldVar = double.Parse(reach);

                newReach = "" + Math.Min(oldVar, newXPos) + "-" + Math.Max(oldVar, newXPos);
            }
            else
            {
                (double xLow, double xHigh) = Deconstruct(reach.Split("-"));
                newReach = "" + Math.Min(xLow, newXPos) + "-" + Math.Max(xHigh, newXPos);
            }

            return newReach;
        }


        private string calculateReach(string reach, double x1, double x2)
        {
            string newReach = reach;
            double xLow = Math.Min(x1, x2);
            double xHigh = Math.Max(x1, x2);

            if (reach == null || reach == "")
            {
                newReach = "" + xLow + "-" + xHigh;
            }
            else if (!reach.Contains("-"))
            {
                double oldVar = double.Parse(reach);

                newReach = "" + Math.Min(xLow, oldVar) + "-" + Math.Max(xHigh, oldVar);
            }
            else
            {
                (double xOldLow, double xOldHigh) = Deconstruct(reach.Split("-"));
                newReach = "" + Math.Min(xLow, xOldLow) + "-" + Math.Max(xHigh, xOldHigh);
            }

            return newReach;
        }

        private (double, double) Deconstruct(string[] parts)
        {
            return (double.Parse(parts[0]), double.Parse(parts[1]));
        }
    }
}
