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
            using StreamReader reader = new("files/Day02Input.txt");
            string text = reader.ReadToEnd();
            string[] ranges = text.Split(",");

            double counterEqualParts = 0;
            List<int> possibleDivides = new List<int>();
            foreach (string range in ranges)
            {
                string[] parts = range.Split("-");
                double low = double.Parse(parts[0]);
                double high = double.Parse(parts[1]);

                for (double i = low; i <= high; i++)
                {
                    possibleDivides.Clear();
                    string iAsString = i.ToString();

                    for (int j = 2; j <= iAsString.Length; j++)
                    {
                        double ku = (double)iAsString.Length / (double)j;
                        if (ku == Math.Floor((double)iAsString.Length / j))
                        {
                            possibleDivides.Add(j);
                        }
                    }

                    foreach (int divider in possibleDivides)
                    {
                        List<string> chunks = new List<string>();
                        int chunkSize = iAsString.Length / divider;
                        for (int s = 0; s < iAsString.Length; s += chunkSize)
                        {
                            chunks.Add(iAsString.Substring(s, chunkSize));
                        }

                        if (chunks.Distinct().Count() == 1)
                        {
                            counterEqualParts += double.Parse(iAsString);
                            break;
                        }
                    }
                }
            }

            Console.WriteLine("Result Part2: " + counterEqualParts);
        }
    }
}
