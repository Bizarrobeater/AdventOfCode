using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Dec01 : AdventCodeBase<int, long>
    {
        public Dec01() : base(ReadDataFile.FileToListInt)
        {
        }

        // finds the 2 numbers in a list that sums to 2020
        // result is the 2 numbers multiplied
        // Correct answer: 121396
        public override long Solution1()
        {
            List<int> tempList = new List<int>(dataList);
            // Sorting the list to make a binary search
            tempList.Sort();
            int goalNumber;
            BinarySearch search = new BinarySearch(tempList);
            int resultIndex;

            // Bruteforcing solution
            foreach (int i in tempList)
            {
                goalNumber = 2020 - i;
                resultIndex = search.FindIndex(goalNumber);


                if (resultIndex >= 0)
                {
                    return i * tempList[resultIndex];
                }
            }
            // In case of no correct answer
            return -1;
        }

        // Same deal, but now with 3 numbers
        // Correct answer is: 73.616.634
        public override long Solution2()
        {
            List<int> tempList = new List<int>(dataList);
            tempList.Sort();
            int goalNumber;
            BinarySearch baseSearch = new BinarySearch(tempList);
            int resultIndex;

            // Starts by finding 1 number from the low end of list
            for (int i = 0; i < tempList.Count - 2; i++)
            {
                // test with all number from the high end of list
                for (int j = tempList.Count - 1; j > i; j--)
                {
                    goalNumber = 2020 - tempList[i] - tempList[j];
                    // performs binary search
                    resultIndex = baseSearch.FindIndex(goalNumber);

                    // if a index is found returns the result
                    if (resultIndex >= 0)
                    {
                        return tempList[i] * tempList[j] * tempList[resultIndex];
                    }
                }
            }
            // in case of invalid dataset
            return -1;
        }
    }
}
