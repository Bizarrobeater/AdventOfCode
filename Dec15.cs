using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Dec15 : AdvancedAdventCodeBase<string, int>
    {
        List<int> startNumbs;

        public Dec15(Dictionary<string, int> testDict) : base(testDict)
        {

        }

        public Dec15() : base(ReadDataFile.FileToListSimple)
        {
            startNumbs = dataList[0].Split(',').Select(int.Parse).ToList();
        }


        // based on specific memorygame rules find the 2020th number
        // Correct answer: 234
        public override int Solution1()
        {
            return GenerelSolution(2020);
        }


        // Same rules as before apply, but now it's the 30.000.000th number
        // Correct Answer: 8.984, takes ~5 seconds to calculate
        public override int Solution2()
        {
            return GenerelSolution(30_000_000);
        }

        private int GenerelSolution(int maxTurn)
        {
            Dictionary<int, int[]> memoryGame = new Dictionary<int, int[]>();

            
            // Initialising the game with the provided starting numbers
            for (int i = 1; i <= startNumbs.Count; i++)
            {
                memoryGame[startNumbs[i - 1]] = new int[] { 0, i };
            }
            
            int lastNumber = startNumbs[startNumbs.Count - 1];
            int[] lastAppeared;
            int newNumber;

            for (int i = startNumbs.Count + 1; i <= maxTurn; i++)
            {
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

                if (memoryGame.ContainsKey(newNumber))
                {
                    // if the new number has appeared before in the memory game the appearence is updated
                    memoryGame[newNumber][0] = memoryGame[newNumber][1];
                    memoryGame[newNumber][1] = i;
                }
                else
                {
                    // else it is added to the dict
                    memoryGame[newNumber] = new int[] { 0, i };
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
