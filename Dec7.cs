using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace AdventOfCode
{
    public class Dec7 : ISolution
    {
        List<string> dataList;
        List<BagNode> _bagNodes = new List<BagNode>();
        BagsInBags _inBags;

        private List<BagNode> BagNodes { get => _bagNodes; set => _bagNodes = value; }
        private BagsInBags InBags { get => _inBags; set => _inBags = value; }

        public Dec7()
        {
            dataList = ReadDataFile.FileToListSimple("AdventCode7Dec.txt");
            foreach (string rawData in dataList)
            {
                BagNodes.Add(new BagNode(rawData.Remove(rawData.Length - 1)));
            }
            InBags = new BagsInBags(BagNodes);
        }

        public void Timer()
        {
            Stopwatch watch = new Stopwatch();
            Console.Write("Part 1 Solution: ");
            watch.Start();
            Console.WriteLine(SolutionPart1());
            watch.Stop();
            Console.Write("Milliseconds taken: ");
            Console.WriteLine(watch.ElapsedMilliseconds + Environment.NewLine);
            watch.Reset();
            Console.Write("Part 2 Solution: ");
            watch.Start();
            Console.WriteLine(SolutionPart2());
            watch.Stop();
            Console.Write("Milliseconds taken: ");
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        // for testing purposes
        public Dec7(List<string> testList)
        {
            dataList = testList;
            foreach (string rawData in dataList)
            {
                BagNodes.Add(new BagNode(rawData.Remove(rawData.Length - 1)));
            }
            InBags = new BagsInBags(BagNodes);
        }

        public int SolutionPart1()
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

        public int SolutionPart2()
        {
            BagNode seachNode = BagNodes.Find(r => r.BagName == "shiny gold");
            return InBags.TraverseBagContains(seachNode);
        }

        private class BagsInBags
        {
            Dictionary<BagNode, Dictionary<BagNode, int>> _bagtionary = new Dictionary<BagNode, Dictionary<BagNode, int>>();

            public Dictionary<BagNode, Dictionary<BagNode, int>> Bagtionary { get => _bagtionary; set => _bagtionary = value; }

            public BagsInBags(List<BagNode> bagNodes)
            {
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

            public List<BagNode> TraverseSearch(BagNode CurrentBagNode, BagNode SearchNode, out bool nodeFound)
            {
                nodeFound = false;
                List<BagNode> result = new List<BagNode>();
                if (CurrentBagNode.BagNodeNames == null)
                    return result;
                else if (CurrentBagNode == SearchNode)
                {
                    nodeFound = true;
                    return result;
                }
                else
                {
                    bool bagsFound = false;
                    List<BagNode> bagsInBagList = Bagtionary[CurrentBagNode].Keys.ToList();
                    foreach (BagNode nextBag in bagsInBagList)
                    {
                        List<BagNode> foundBags = TraverseSearch(nextBag, SearchNode, out nodeFound);
                        if (nodeFound)
                        {
                            bagsFound = true;
                            result = result.Union(foundBags, new BagnodeComparer()).ToList();
                        }

                    }
                    if(bagsFound)
                        result.Add(CurrentBagNode);
                    nodeFound = bagsFound;
                }
                return result;
            }

            public StringBuilder PrintTraverse(BagNode bagNode, int amount = 0, int bagDepth = 0)
            {
                StringBuilder stringBuilder = new StringBuilder();

                // adds two spaces per depth of bags
                string tabLength = new String(' ', bagDepth * 2);

                if (amount == 0)
                    // Toplevel bag
                    stringBuilder.Append($"--{bagNode.BagName}--\n");
                else
                    // Any other bag that also contains an amount of bags
                    stringBuilder.Append($"{tabLength}{bagNode.BagName} -> {amount}\n");

                // If theres no bags in the bag return that.
                if (bagNode.BagNodeNames == null)
                {
                    stringBuilder.Append($"{tabLength}  Nothing\n");
                    return stringBuilder;
                }
                
                // Dict for from the 
                Dictionary<BagNode, int> BagDict = Bagtionary[bagNode];
                foreach (KeyValuePair<BagNode, int> kvp in BagDict)
                {

                    stringBuilder.Append(PrintTraverse(kvp.Key, kvp.Value, bagDepth + 1));

                }

                return stringBuilder;
            }


            public void PrintBags()
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (BagNode bag in Bagtionary.Keys)
                {
                    stringBuilder.Append(PrintTraverse(bag));
                }
                Console.WriteLine(stringBuilder.ToString());
            }

        }

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
            string _bagName;
            Dictionary<string, int> _bagNodeNames = new Dictionary<string, int>();
            public string BagName { get => _bagName; set => _bagName = value; }
            public Dictionary<string, int> BagNodeNames { get => _bagNodeNames; set => _bagNodeNames = value; }

            public BagNode(string rawDataString, bool raw = true)
            {
                    string[] tempSplit = rawDataString.Split(" bags contain ");
                    BagName = tempSplit[0];
                    Bagsplitter(tempSplit[1].Split(", "));

            }

            private void Bagsplitter(string[] containedBags)
            {
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

            public string PrintBags()
            {
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append($"--{BagName}--\n");

                if (BagNodeNames != null)
                {
                    foreach (KeyValuePair<string, int> kvp in BagNodeNames)
                    {
                        stringBuilder.Append($"\t{kvp.Key} - {kvp.Value}\n");
                    }
                }
                else
                    stringBuilder.Append("\tNo bags\n");

                return stringBuilder.ToString();
            }
        }
    }
}
