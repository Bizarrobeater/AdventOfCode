using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    public class Dec09 : AdventCodeBase<long, long>
    {
        bool testRun = false;
        
        public Dec09() : base(ReadDataFile.FileToListLong)
        {
        }

        public Dec09(List<long> dataList) : base(dataList)
        {
            this.dataList = dataList;
            testRun = true;
        }

        // checks if a given preamble is valid
        // TODO: Should be done by binary search
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

        //
        // based on a preamble of numbers
        // if the sum of any 2 numbers in a preamble of a specific number the number is valid
        // find the first number that is not valid
        // Correct answer: 70.639.851
        public override long Solution1()
        {
            // preambles length depends on whether or not it is a testrun
            int preambleLength = 25;
            if (testRun)
                preambleLength = 5;
            long goal;
            List<long> preambleList;

            // solved by bruteforcing
            for (int i = preambleLength + 1; i < dataList.Count; i++)
            {
                goal = dataList[i];
                preambleList = dataList.GetRange(i - preambleLength, preambleLength);

                if (!ValidPreamble(preambleList, goal))
                {
                    return goal;
                }
            }
            return -1;
        }
        
        //
        // based on the invalid number from solution 1
        // find a contiguous set of number that adds up to this number
        // result is the sum of the lowest and highest number in this list
        // Correct answer: 8.249.240
        public override long Solution2()
        {
            long goalSum = Solution1();
            Queue<long> goalQueue = new Queue<long>();

            for (int i = 0; i < dataList.Count; i++)
            {
                // add a given number to queue
                goalQueue.Enqueue(dataList[i]);
                //if the sum of the queue is above the goal
                if (goalQueue.Sum() > goalSum)
                {
                    // dequeue
                    goalQueue.Dequeue();
                    // continue dequeueing until the sum becomes less than or equal to the goal
                    while (goalQueue.Sum() > goalSum)
                    {
                        goalQueue.Dequeue();
                    }
                }
                // if the correct queue has been found return result
                if (goalQueue.Sum() == goalSum)
                {
                    return goalQueue.Min() + goalQueue.Max();
                }
            }
            // in case of invalid data
            return -1;
        }
    }
}
