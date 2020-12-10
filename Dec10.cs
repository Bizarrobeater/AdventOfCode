using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AdventOfCode
{
    class Dec10 : ISolution
    {
        List<int> dataList;

        public Dec10()
        {
            dataList = ReadDataFile.FileToListInt("AdventCode10Dec.txt");
            //AddMissingData();
        }

        public Dec10(List<int> testList)
        {
            dataList = testList;
            //AddMissingData();
        }

        private List<int> AddMissingData(List<int> dataList)
        {
            // List starts at 0
            int min = 0;
            int max = dataList.Max() + 3;

            dataList.Add(min);
            dataList.Add(max);

            return dataList;
        }

        public void Timer()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            SolutionPart2Long();
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);
        }

        public void PrintTest()
        {
            List<int> sortedData = AddMissingData(dataList);
            sortedData.Sort();

            foreach (int number in sortedData)
            {
                Console.WriteLine(number);
            }
        }
        
        //
        // Finds the number of differences between sequential numbers.
        // differences can be 1-2-3, multiplys the count of 1 diff and 3 diff
        // Solution: 2775
        public int SolutionPart1()
        {
            List<int> sortedData = AddMissingData(dataList);
            sortedData.Sort();

            Dictionary<int, int> joltCounter = new Dictionary<int, int>();

            int diff;
            for (int i = 1; i < sortedData.Count; i++)
            {
                diff = sortedData[i] - sortedData[i - 1];
                if (joltCounter.ContainsKey(diff))
                    joltCounter[diff]++;
                else
                    joltCounter.Add(diff, 1);
            }

            foreach (KeyValuePair<int, int> kvp in joltCounter)
            {
                Console.WriteLine($"Key: {kvp.Key} - Value: {kvp.Value}");
            }

            return joltCounter[1] * joltCounter[3];
        }

        public long ReachTraversal(Dictionary<int, List<int>> joltReach, int searchInt)
        {
            Dictionary<int, long> combinationSum = new Dictionary<int, long>();

            for (int i = joltReach.Count - 1; i >= 0; i--)
            {


            }
            

            return -1;
        }

        //
        // Determine the number of sequence combinations in the list
        // each number in a sequence should have 1-3 between itself and the next number
        // correct answer 518.344.341.716.992 different combinations
        public long SolutionPart2Long()
        {
            // add 0 as a starting point for the combinations and sort the list
            List<int> sortedData = new List<int>(dataList);
            sortedData.Add(0);
            sortedData.Sort();

            // Dictionary will contain the ints a specific int can reach given the rules.
            Dictionary<int, List<int>> joltReach = new Dictionary<int, List<int>>();
            int key;
            List<int> values;
            for (int i = 0; i < sortedData.Count; i++)
            {
                key = sortedData[i];
                values = new List<int>();
                for (int j = i + 1; j <= i + 3; j++)
                {
                    if (j > sortedData.Count - 1)
                        break;
                    if (sortedData[j] <= key + 3)
                    {
                        values.Add(sortedData[j]);
                    }
                }
                joltReach.Add(key, values);
            }

            // Dictionary will contain the number of combination from a given int
            Dictionary<int, long> combinationSumDict = new Dictionary<int, long>();
            int currJolt;
            long combinationSum;
            // goes through the list from high to low
            for (int i = sortedData.Count - 1; i >= 0; i--)
            {
                currJolt = sortedData[i];
                combinationSum = 0;
                // the last int in the list will be set as 1 possible combination
                if (joltReach[currJolt].Count == 0)
                    combinationSum = 1;
                // all other ints will have an amount of combinations equal to the sum combination of its reach
                else
                {
                    foreach (int reach in joltReach[currJolt])
                    {
                        combinationSum += combinationSumDict[reach];
                    }
                }
                combinationSumDict.Add(currJolt, combinationSum);
            }
            // returns the number of combinations for 0
            return combinationSumDict[0];
        }

        public int SolutionPart2()
        {
            throw new NotImplementedException();
        }

    }
}
