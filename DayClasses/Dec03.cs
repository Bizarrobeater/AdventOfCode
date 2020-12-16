using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public class Dec03 : AdventCodeBase<string, long>
    {
        int width;

        public Dec03() : base(ReadDataFile.FileToListSimple)
        {
            width = dataList[0].Length;
        }

        // Given a slope (moved columns and rows)
        // returns the number of trees(#) hit
        private int TreeCounter(int columnsMove, int rowMove)
        {
            List<string> list = new List<string>(dataList);
            int columnPos = 0;

            int treeCount = 0;

            for (int i = rowMove; i < list.Count; i += rowMove)
            {
                string row = list[i];
                columnPos += columnsMove;
                // Resets column position at overflow
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

        // Counts the number of tree(#) hits in a decent given by a slope
        // Correct answer: 284
        public override long Solution1()
        {
            return TreeCounter(3, 1);
        }

        // a list of different slopes are provided
        // the number of trees hit are multiplied
        // Correct answer: 3.510.149.120
        public override long Solution2()
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
    }
}
