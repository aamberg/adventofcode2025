namespace adventofcode2025
{
    internal class Day05 : IDay
    {
        private List<double> freshItems = new List<double>();

        private List<(double from, double to)> freshItemRanges = new List<(double from, double to)>();

        public void SolvePart1()
        {
            freshItems.Clear();
            freshItemRanges.Clear();
            using StreamReader reader = new("files/Day05Input.txt");
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

            Console.WriteLine("Result Part1: " + freshItems.Count);
        }

        public void SolvePart2()
        {
            freshItems.Clear();
            freshItemRanges.Clear();
            using StreamReader reader = new("files/Day05Input.txt");
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");

            bool emtpyLineFound = false;
            foreach (string line in lines)
            {
                if (line == "")
                {
                    emtpyLineFound = true;
                    break;
                }

                if (!emtpyLineFound)
                {
                    string[] parts = line.Split("-");

                    double from = double.Parse(parts[0]);
                    double to = double.Parse(parts[1]);

                    freshItemRanges.Add((from, to));
                }
            }

            freshItemRanges = freshItemRanges.OrderBy(x => x.from).ToList();

            List<(double from, double to)> optimizedFreshRanges = new List<(double from, double to)>();
            optimizedFreshRanges.Add(freshItemRanges[0]);

            bool isFirst = true;
            foreach (var (start, end) in freshItemRanges)
            {
                if (isFirst)
                {
                    isFirst = false;
                    continue;
                }

                var lastOptimizedRow = optimizedFreshRanges[optimizedFreshRanges.Count - 1];
                if (start <= lastOptimizedRow.to)
                {
                    optimizedFreshRanges[optimizedFreshRanges.Count - 1] = (lastOptimizedRow.from, Math.Max(end, lastOptimizedRow.to));
                }
                else
                {
                    optimizedFreshRanges.Add((start, end));
                }

                var b = 0;
            }

            double countFreshItems = 0;
            foreach (var (start, end) in optimizedFreshRanges)
            {
                countFreshItems = countFreshItems + end - start + 1;
            }

            Console.WriteLine("Result Part2: " + countFreshItems);
        }


        private bool checkIfItemIsFresh(double item)
        {
            bool isFresh = false;
            foreach (var (from, to) in freshItemRanges)
            {
                if (item >= from && item <= to)
                {
                    isFresh = true;
                    break;
                }
            }

            return isFresh;
        }
    }
}
