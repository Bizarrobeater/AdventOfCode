using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Dec10 : ISolution
    {
        List<int> dataList;

        public Dec10()
        {
            dataList = ReadDataFile.FileToListInt("AdventCode10Dec.txt");
            AddMissingData();
        }

        public Dec10(List<int> testList)
        {
            dataList = testList;
            AddMissingData();
        }

        private void AddMissingData()
        {
            // List starts at 0
            int min = 0;
            int max = dataList.Max() + 3;

            dataList.Add(min);
            dataList.Add(max);
        }
        
        //
        // Finds the number of differences between sequential numbers.
        // differences can be 1-2-3, multiplys the count of 1 diff and 3 diff
        // Solution: 2775
        public int SolutionPart1()
        {
            List<int> sortedData = dataList;
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

        //
        // Determine the number of sequence combinations in the list
        // each number in a sequence should have 1-3 between itself and the next number
        public long SolutionPart2long()
        {
            List<int> sortedData = dataList;
            sortedData.Sort();

            long result = 1;
            int multiplier;
            for (int i = sortedData.Count - 1; i >= 0; i--)
            {
                multiplier = 0;
                for (int j = i - 1; j > i - 3; j--)
                {
                    if (sortedData[j] >= sortedData[i] - 3)
                    {
                        multiplier++;
                    }

                }
                result *= multiplier;
            }
            return result;
        }

        public int SolutionPart2()
        {
            throw new NotImplementedException();
        }
    }
}
