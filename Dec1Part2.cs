using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    public class Dec1Part2 : ISolution
    {
        List<int> dataList;

        public Dec1Part2()
        {
            var dataStrList = ReadDataFile.FileToListSimple("AdventCode1Dec1Input.txt"); 
            dataList = dataStrList.Select(s => Convert.ToInt32(s)).ToList();
        }

        public int SolutionPart1()
        {
            List<int> tempList = dataList;
            tempList.Sort();
            int goalNumber;
            BinarySearch search = new BinarySearch(tempList);
            int resultIndex;
            

            foreach (int i in tempList)
            {
                goalNumber = 2020 - i;
                resultIndex = search.FindIndex(goalNumber);


                if (resultIndex >= 0)
                {
                    return i * tempList[resultIndex];
                }
            }

            return -1;
        }

        public int SolutionPart2()
        {
            List<int> tempList = dataList;
            tempList.Sort();
            int goalNumber;
            BinarySearch baseSearch = new BinarySearch(tempList);
            int resultIndex;

            for (int i = 0; i < tempList.Count - 2; i++)
            {
                for (int j = tempList.Count - 1; j > i; j--)
                {
                    goalNumber = 2020 - tempList[i] - tempList[j];
                    resultIndex = baseSearch.FindIndex(goalNumber);

                    if (resultIndex >= 0)
                    {
                        return tempList[i] * tempList[j] * tempList[resultIndex];
                    }
                }
            }

            return -1;
        }


        public int Solution()
        {
            int first = 0;
            int second = 0;
            int third = 0;

            for (int i = 0; i < dataList.Count - 3; i++)
            {
                first = dataList[i];
                for (int j = i + 1; j < dataList.Count - 2; j++)
                {
                    second = dataList[j];
                    if (!(first + second >= 2020))
                    {
                        for (int k = j + 1; k < dataList.Count - 1; k++)
                        {
                            third = dataList[k];
                            if (first + second + third == 2020)
                            {
                                return first * second * third;

                            }
                        }
                    }
                }
            }
            return -1;
        }
    }
}
