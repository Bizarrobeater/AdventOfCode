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
            Dictionary<int, int> memoryGame = new Dictionary<int, int>();
            
            // Initialising the game with the provided starting numbers
            for (int i = 0; i < startNumbs.Count; i++)
            {
                memoryGame[startNumbs[i]] = i + 1;
            }

            int currNumber = -1;
            int nextNumber = 0;
            int lastAppeared;

            for (int i = startNumbs.Count + 1; i <= maxTurn; i++)
            {
                currNumber = nextNumber;
                if (memoryGame.TryGetValue(currNumber, out lastAppeared))
                {
                    nextNumber = i - lastAppeared;
                }
                else
                {
                    nextNumber = 0;
                }
                memoryGame[currNumber] = i;
            }
            return currNumber;
        }

        internal override void ConvertTestDataToUseful(string testDataKey)
        {
            startNumbs = testDataKey.Split(',').Select(int.Parse).ToList();
        }
    }
}
