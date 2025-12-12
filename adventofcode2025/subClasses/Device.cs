using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adventofcode2025.subClasses
{
    internal class Device(string name)
    {
        public string name { get; } = name;
        private List<Device>? children = new List<Device>();

        public int getOutConnections()
        {
            int result = 0;
            foreach (Device child in children)
            {
                if (child.name == "out")
                {
                    result++;
                }
                else
                {
                    result = result + child.getOutConnections();
                }
            }

            return result;
        }

        public void addChild(Device Child)
        {
            children.Add(Child);
        }

        public List<Device> getAllChildren()
        {
            return children;
        }
    }
}
