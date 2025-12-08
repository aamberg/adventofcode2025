using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace adventofcode2025
{
    internal class Day01 : IDay
    {
        private int dialPosition = 0;

        public void SolvePart1()
        {
            dialPosition = 50;
            int CounterResult = 0;
            using StreamReader reader = new("files/Day01Input.txt");
            string text = reader.ReadToEnd();
            string[] instructions = text.Split("\n");

            foreach (string instruction in instructions)
            {
                string direction = instruction.Substring(0, 1);
                int amount = int.Parse(instruction.Substring(1));

                amount = amount%100;
                dialRotate(direction, amount);

                if (dialPosition == 0)
                {
                    CounterResult++;
                }
            }


            Console.WriteLine("Result Part1: "+CounterResult);
        }

        public void SolvePart2()
        {
            Console.WriteLine("Result Part2: ");
        }


        private void dialRotate(string direction, int amount)
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

        }
    }
}
