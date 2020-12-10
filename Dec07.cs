using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public class Dec07 : AdventCodeBase<string, int>
    {
        List<BagNode> _bagNodes = new List<BagNode>();
        BagsInBags _inBags;

        private List<BagNode> BagNodes { get => _bagNodes; set => _bagNodes = value; }
        private BagsInBags InBags { get => _inBags; set => _inBags = value; }

        public Dec07() : base(ReadDataFile.FileToListSimple)
        {
            foreach (string rawData in dataList)
            {
                BagNodes.Add(new BagNode(rawData.Remove(rawData.Length - 1)));
            }
            InBags = new BagsInBags(BagNodes);
        }

        // for testing purposes
        public Dec07(List<string> testList) : base(testList)
        {
            foreach (string rawData in dataList)
            {
                BagNodes.Add(new BagNode(rawData.Remove(rawData.Length - 1)));
            }
            InBags = new BagsInBags(BagNodes);
        }

        // find the number of different bag that can at some level contain a shiny gold bag
        // Correct answer: 326
        public override int Solution1()
        {
            List<BagNode> resultBag = new List<BagNode>();
            List<BagNode> tempBags = new List<BagNode>();
            BagNode seachNode = BagNodes.Find(r => r.BagName == "shiny gold");
            foreach (BagNode bag in InBags.Bagtionary.Keys)
            {
                if (resultBag.Contains(bag))
                    continue;
                tempBags = InBags.TraverseSearch(bag, seachNode, out bool thrownBool);
                resultBag = resultBag.Union(tempBags, new BagnodeComparer()).ToList();
            }

            return resultBag.Count;
        }

        // Find the amount of bags you need inside 1 shiny gold bag
        // Correct answer: 5635
        public override int Solution2()
        {
            BagNode seachNode = BagNodes.Find(r => r.BagName == "shiny gold");
            return InBags.TraverseBagContains(seachNode);
        }

        private class BagsInBags
        {
            // Dictionary of bags, containing a dictionary of the bags it contains and that number of bags
            public Dictionary<BagNode, Dictionary<BagNode, int>> Bagtionary { get; set; }

            public BagsInBags(List<BagNode> bagNodes)
            {
                Bagtionary = new Dictionary<BagNode, Dictionary<BagNode, int>>();
                foreach (BagNode bag in bagNodes)
                {
                    // bag contains no other bags
                    if (bag.BagNodeNames == null)
                    {
                        Bagtionary.Add(bag, null);
                        continue;
                    }
                    // temporary dictionary
                    Dictionary<BagNode, int> tempDict = new Dictionary<BagNode, int>();
                    // look at the names of bags that is in current bag
                    foreach (KeyValuePair<string, int> kvp in bag.BagNodeNames)   
                    {
                        // find the bag by name in the list of bagNodes and adds that to the temp dict
                        tempDict.Add(bagNodes.Find(r => r.BagName == kvp.Key), kvp.Value);
                    }
                    // adds bag and tempdict to bagtionary
                    Bagtionary.Add(bag, tempDict);
                }
            }

            //
            // takes a bag and calculates the amount of bags it contains recursively
            public int TraverseBagContains(BagNode currentNode)
            {
                int result = 0;
                if (currentNode.BagNodeNames == null)
                    return 0;
                else
                {
                    Dictionary<BagNode, int> bagNodes = Bagtionary[currentNode];
                    foreach (KeyValuePair<BagNode, int> kvp in bagNodes)
                    {
                        result += kvp.Value * (TraverseBagContains(kvp.Key) + 1);
                    }
                }
                return result;
            }

            // Counts the number of bags a given bag contains recursively
            public List<BagNode> TraverseSearch(BagNode CurrentBagNode, BagNode SearchNode, out bool nodeFound)
            {
                nodeFound = false;
                List<BagNode> result = new List<BagNode>();
                // if the current bag contains no other bags return empty list
                if (CurrentBagNode.BagNodeNames == null)
                    return result;
                // if the current bag is the bag being searched for, return empty list and output true
                else if (CurrentBagNode == SearchNode)
                {
                    nodeFound = true;
                    return result;
                }

                bool bagsFound = false;
                // gets the bags in the current bag
                List<BagNode> bagsInBagList = Bagtionary[CurrentBagNode].Keys.ToList();
                foreach (BagNode nextBag in bagsInBagList)
                {
                    // recursively finds the number of bags in the current bag
                    List<BagNode> foundBags = TraverseSearch(nextBag, SearchNode, out nodeFound);

                    if (nodeFound)
                    {
                        // if the search node has been found it adds the difference in bags to the existing list of bags
                        bagsFound = true;
                        result = result.Union(foundBags, new BagnodeComparer()).ToList();
                    }
                }
                // adds the current bag to the result list if the search bag has been found
                if(bagsFound)
                    result.Add(CurrentBagNode);
                nodeFound = bagsFound;
                return result;
            }
        }

        // used to make a union of lists of bags by comparing bag names
        private class BagnodeComparer : IEqualityComparer<BagNode>
        {
            public bool Equals(BagNode x, BagNode y)
            {
                return x.BagName == y.BagName;
            }

            public int GetHashCode(BagNode obj)
            {
                return 0;
            }
        }


        private class BagNode
        {
            public string BagName { get; set; }
            public Dictionary<string, int> BagNodeNames { get; set; }

            public BagNode(string rawDataString)
            {
                    string[] tempSplit = rawDataString.Split(" bags contain ");
                    BagName = tempSplit[0];
                    Bagsplitter(tempSplit[1].Split(", "));
            }

            private void Bagsplitter(string[] containedBags)
            {
                BagNodeNames = new Dictionary<string, int>();
                if (containedBags[0] == "no other bags")
                    BagNodeNames = null;
                else
                {
                    Regex rx = new Regex(@"(\d+) ([a-z ]+) bags?");
                    foreach (string bagName in containedBags)
                    {
                        Match match = rx.Match(bagName);
                        GroupCollection groups = match.Groups;
                        BagNodeNames.Add(groups[2].ToString(), Int32.Parse(groups[1].ToString()));
                    }
                }
            }
        }
    }
}
