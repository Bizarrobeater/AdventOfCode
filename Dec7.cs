using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Dec7 : ISolution
    {
        List<string> dataList;
        List<BagNode> _bagNodes = new List<BagNode>();

        private List<BagNode> BagNodes { get => _bagNodes; set => _bagNodes = value; }

        public Dec7()
        {
            dataList = ReadDataFile.FileToListSimple("AdventCode7Dec.txt");
            foreach (string rawData in dataList)
            {
                BagNodes.Add(new BagNode(rawData.Remove(rawData.Length - 1)));
            }

        }

        public void test()
        {

            Console.WriteLine(BagNodes[0].PrintBags());
        }

        public int SolutionPart1()
        {
            throw new NotImplementedException();
        }

        public int SolutionPart2()
        {
            throw new NotImplementedException();
        }

        private class BagNode
        {
            string _bagName;
            Dictionary<string, int> _bagNodeNames = new Dictionary<string, int>();
            string[] containedBags;
            public string BagName { get => _bagName; set => _bagName = value; }
            public Dictionary<string, int> BagNodeNames { get => _bagNodeNames; set => _bagNodeNames = value; }

            public BagNode(string rawDataString)
            {
                string[] tempSplit = rawDataString.Split(" bags contain ");
                BagName = tempSplit[0];
                containedBags = tempSplit[1].Split(",");

            }

            public void PrintBags()
            {
                foreach (string bag in containedBags)
                {
                    Console.WriteLine(bag);
                }
            }
        }
    }
}
