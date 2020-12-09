using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Dec9 : ISolution
    {
        List<string> dataList;
        List<long> longDataList;
        int preamble = 25;
        bool testRun = false;
        
        public Dec9()
        {
            dataList = ReadDataFile.FileToListSimple("AdventCode9Dec.txt");
            longDataList = ConvertDataToLongList(dataList);
        }

        public Dec9(List<long> dataList)
        {
            longDataList = dataList;
            testRun = true;
        }

        private List<long> ConvertDataToLongList(List<string> dataList)
        {
            List<long> newList = new List<long>();
            foreach (string number in dataList)
            {
                newList.Add(long.Parse(number));
            }
            return newList;
        }

        private bool ValidPreamble(List<long> preambleList, long goal)
        {
            long first;
            long second;
            for (int i = 0; i < preambleList.Count - 1; i++)
            {
                first = preambleList[i];
                for (int j = i+1; j < preambleList.Count; j++)
                {
                    second = preambleList[j];
                    if (first + second == goal)
                        return true;
                }
            }
            return false;
        }

        public long SolutionPart1Long()
        {
            int preambleLength = 25;
            if (testRun)
                preambleLength = 5;
            long goal;
            List<long> preambleList;
            
            for (int i = preambleLength + 1; i < longDataList.Count; i++)
            {
                goal = longDataList[i];
                preambleList = longDataList.GetRange(i - preambleLength, preambleLength);
                
                if (!ValidPreamble(preambleList, goal))
                {
                    return goal;
                }
            }
            return -1;
        }

        public long SolutionPart2Long()
        {
            long goalSum = SolutionPart2Long();



            return -1;


        }

        public int SolutionPart1()
        {
            throw new NotImplementedException();
        }

        public int SolutionPart2()
        {
            throw new NotImplementedException();
        }
    }
}
