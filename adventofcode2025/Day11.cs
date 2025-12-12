using adventofcode2025.subClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace adventofcode2025
{
    internal class Day11 : IDay
    {
        Dictionary<string, Device> tmpDeviceStorage = new Dictionary<string, Device>();

        public void SolvePart1()
        {
            using StreamReader reader = new("files/Day11Input.txt");
            string text = reader.ReadToEnd();
            string[] lines = text.Split("\n");


            foreach (string line in lines)
            {
                Device tmpDevice = GetDeviceByName(line.Split(":", StringSplitOptions.TrimEntries)[0]);


                string[] children = line.Split(":", StringSplitOptions.TrimEntries)[1].Split(" ");
                foreach (string child in children)
                {
                    Device tmpChildDevice = GetDeviceByName(child);
                    tmpDevice.addChild(tmpChildDevice);

                    tmpDeviceStorage[tmpChildDevice.name] = tmpChildDevice;
                }


                tmpDeviceStorage[tmpDevice.name] = tmpDevice;
            }

            Device youDevice = tmpDeviceStorage["you"];
            int result = youDevice.getOutConnections();

            Console.WriteLine("Result Part1: " + result);
        }


        private Device? GetDeviceByName(string name)
        {
            Device tmpDevice;
            if (!tmpDeviceStorage.ContainsKey(name))
            {
                tmpDevice = new Device(name);
            }
            else
            {
                tmpDevice = tmpDeviceStorage[name];
            }

            return tmpDevice;
        }

        public void SolvePart2()
        {
            Console.WriteLine("Result Part2: ");
        }


    }
}
