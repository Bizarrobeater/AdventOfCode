using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class Dec1Part2
    {
        public void Solution()
        {
            List<string> dataStrList = File.ReadAllLines(@"DataSources\AdventCode1Dec1input.txt").ToList();
            List<int> dataList = dataStrList.Select(s => Convert.ToInt32(s)).ToList();

            int first = 0;
            int second = 0;
            int third = 0;
            bool foundResult = false;
            int result = 0;

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
                                result = first * second * third;
                                foundResult = true;
                                break;
                            }
                        }
                    }
                    if (foundResult) break;
                }
                if (foundResult) break;
            }

            Console.WriteLine($"{first}, {second}, {third}\nResult: {result}");
        }
    }
}
