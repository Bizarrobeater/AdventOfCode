using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Dec15 : AdvancedAdventCodeBase<string, long>
    {
        List<int> startNumbs;

        public Dec15(Dictionary<string, long> testDict) : base(testDict)
        {

        }

        public Dec15() : base(ReadDataFile.FileToListSimple)
        {
            startNumbs = dataList[0].Split(',').Select(int.Parse).ToList();
        }


        // based on specific memorygame rules find the 2020th number
        // Correct answer: 234
        public override long Solution1()
        {
            return GeneralSolution(2020);
        }


        // Same rules as before apply, but now it 30.000.000 number
        // Correct Answer: 8.984, takes +10 seconds to calculate
        public override long Solution2()
        {
            return GeneralSolution(30_000_000);
        }

        private long GeneralSolution(long maxTurn)
        {
            Dictionary<int, int[]> memoryGame = new Dictionary<int, int[]>();
            int lastNumber = 0;
            List<int> numbersSaid = new List<int>();
            // Initialising the game with the provided starting numbers
            for (int i = 1; i <= startNumbs.Count; i++)
            {
                memoryGame.Add(startNumbs[i - 1], new int[] { 0, i });
                lastNumber = startNumbs[i - 1];
            }

            int[] lastAppeared;
            int newNumber;

            for (int i = startNumbs.Count + 1; i <= maxTurn; i++)
            {
                // current turn is 2 ahead of highest index
                lastAppeared = memoryGame[lastNumber];

                if (lastAppeared[0] == 0)
                {
                    // if the last number was said for the first time, the current number is 0
                    newNumber = 0;
                }
                else
                {
                    // if the number has appeared before
                    // the new number is calculated based on the difference between it 2 last appearances
                    newNumber = lastAppeared[1] - lastAppeared[0];
                }

                if (memoryGame.TryGetValue(newNumber, out int[] newNumbLastSeen))
                {
                    // if the new number has appeared before in the memory game the appearence is updated
                    memoryGame[newNumber] = new int[] { newNumbLastSeen[1], i };
                }
                else
                {
                    // else it is added to the dict
                    memoryGame.Add(newNumber, new int[] { 0, i });
                }

                lastNumber = newNumber;
            }
            return lastNumber;
        }

        internal override void ConvertTestDataToUseful(string testDataKey)
        {
            startNumbs = testDataKey.Split(',').Select(int.Parse).ToList();
        }
    }
}
