namespace adventofcode2025
{
    internal class Day01 : IDay
    {
        public void SolvePart1()
        {
            int dialPosition = 50;
            int CounterResult = 0;
            using StreamReader reader = new("files/Day01Input.txt");
            string text = reader.ReadToEnd();
            string[] instructions = text.Split("\n");

            foreach (string instruction in instructions)
            {
                string direction = instruction.Substring(0, 1);
                int amount = int.Parse(instruction.Substring(1));

                amount = amount%100;
                dialPosition = dialRotate(dialPosition, direction, amount);

                if (dialPosition == 0)
                {
                    CounterResult++;
                }
            }


            Console.WriteLine("Result Part1: "+CounterResult);
        }

        public void SolvePart2()
        {
            int dialPosition = 50;
            int CounterResult = 0;
            using StreamReader reader = new("files/Day01Input.txt");
            string text = reader.ReadToEnd();
            string[] instructions = text.Split("\n");

            foreach (string instruction in instructions)
            {
                string direction = instruction.Substring(0, 1);
                int amount = int.Parse(instruction.Substring(1));

                decimal fullRotates = amount / 100;
                decimal fullRotateClicks = Math.Floor(fullRotates);

                CounterResult = CounterResult + int.Parse(fullRotateClicks.ToString());

                amount = amount % 100;
                int newDialPosition = dialRotate(dialPosition, direction, amount);

                if (direction == "R" && newDialPosition < dialPosition)
                {
                    CounterResult++;
                }
                else if (direction == "L" && dialPosition != 0 && (newDialPosition > dialPosition || newDialPosition == 0))
                {
                    CounterResult++;
                }

                dialPosition = newDialPosition;
            }

            Console.WriteLine("Result Part2: "+ CounterResult);
        }


        private int dialRotate(int dialPosition, string direction, int amount)
        {
            if (direction == "R")
            {
                dialPosition = (dialPosition + amount) % 100;
            }
            else
            {
                dialPosition = dialPosition - amount;
                if (dialPosition < 0)
                {
                    dialPosition = 100 + dialPosition;
                }
            }

            return dialPosition;
        }
    }
}
