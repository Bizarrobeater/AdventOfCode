using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public class Dec3 : ISolution
    {

        List<string> dataList;
        int width;

        public List<string> DataList { get => dataList; }

        public Dec3()
        {
            dataList = ReadDataFile.FileToListSimple("AdventCode3Dec.txt");
            width = dataList[0].Length;
        }

        private int TreeCounter(int columnsMove, int rowMove)
        {
            List<string> list = new List<string>(DataList);
            int columnPos = 0;

            int treeCount = 0;

            for (int i = rowMove; i < list.Count; i += rowMove)
            {
                string row = list[i];
                columnPos += columnsMove;
                if (columnPos > width - 1)
                {
                    columnPos -= width;
                }

                if (row[columnPos] == '#')
                {
                    treeCount++;
                }

            }
            return treeCount;
        }

        public long SolutionPart2Long()
        {
            int[,] slopes = new int[,]
{
                {1, 1},
                {3, 1},
                {5, 1},
                {7, 1},
                {1, 2},
};

            long multiplication = 1;

            for (int i = 0; i < slopes.GetLength(0); i++)
            {
                multiplication *= TreeCounter(slopes[i, 0], slopes[i, 1]);
            }


            return multiplication;
        }


        public int SolutionPart1()
        {
            return TreeCounter(3,1);
        }

        public int SolutionPart2()
        {
            // Result larger than Int can handle

            return -1;
        }
    }
}
