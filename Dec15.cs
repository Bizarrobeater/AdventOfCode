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
            int maxTurn = 2020;
            Dictionary<int, int[]> memoryGame = new Dictionary<int, int[]>();
            List<int> numbersSaid = new List<int>();
            // Initialising the game with the provided starting numbers
            for (int i = 1; i <= startNumbs.Count; i++)
            {
                memoryGame.Add(startNumbs[i - 1], new int[] { 0, i });
                numbersSaid.Add(startNumbs[i - 1]);
            }

            int lastNumber;
            int[] lastAppeared;
            int newNumber;

            for (int i = startNumbs.Count + 1; i <= maxTurn; i++)
            {
                // current turn is 2 ahead of highest index
                lastNumber = numbersSaid[i - 2];
                lastAppeared = memoryGame[lastNumber];

                if (lastAppeared[0] == 0)
                {
                    // if the last number was said for the first time, the current number is 0
                    // 0 last appearence is updated in the dict
                    numbersSaid.Add(0);
                    if (memoryGame.ContainsKey(0))
                        memoryGame[0] = new int[] { memoryGame[0][1], i };
                    else
                        memoryGame.Add(0, new int[] { 0, i });
                }
                else
                {
                    // if the number has appeared before
                    // the new number is calculated based on the difference between it 2 last appearances
                    newNumber = lastAppeared[1] - lastAppeared[0];
                    
                    // the new number is added to the range
                    numbersSaid.Add(newNumber);
                    if (memoryGame.ContainsKey(newNumber))
                    {
                        // if the new number has appeared before in the memory game the appearence is updated
                        memoryGame[newNumber] = new int[] { memoryGame[newNumber][1], i };
                    }
                    else
                    {
                        // else it is added to the dict
                        memoryGame.Add(newNumber, new int[] { 0, i });
                    }

                }
            }
            return numbersSaid[maxTurn - 1];
        }


        // Same rules as before apply, but now it 30.000.000 number
        // list sorted list of keys is generated for searching through instead of dict.containskey method
        public override long Solution2()
        {
            long maxTurn = 30_000_000;

            Dictionary<long, long[]> memoryGame = new Dictionary<long, long[]>();
            List<long[]> dictValueSorted = null;
            List<long> dictKeysSorted = null;
            long lastNumber = 0;
            long[] lastAppeared;
            int sortedIndex = 0;
            // Initialising the game with the provided starting numbers
            for (int i = 1; i <= startNumbs.Count; i++)
            {
                lastNumber = startNumbs[i - 1];
                lastAppeared = new long[] { 0, i };

                if (dictKeysSorted == null)
                {
                    dictValueSorted = new List<long[]>
                    {
                       lastAppeared
                    };
                    dictKeysSorted = new List<long>
                    {
                        lastNumber,
                    };
                }
                else
                {
                    // keys are now their own sorted list, for faster search
                    BinarySearch.ItemExists(dictKeysSorted, lastNumber, out sortedIndex);
                    dictKeysSorted.Insert(sortedIndex, lastNumber);
                    dictValueSorted.Insert(sortedIndex, lastAppeared);
                }
            }

            long newNumber;

            for (int i = startNumbs.Count + 1; i <= maxTurn; i++)
            {
                lastAppeared = dictValueSorted[sortedIndex];

                if (lastAppeared[0] == 0)
                {
                    newNumber = 0;
                }
                else
                {
                    // if the number has appeared before
                    // the new number is calculated based on the difference between it 2 last appearances
                    newNumber = lastAppeared[1] - lastAppeared[0];
                }

                // the new number is added to the range
                if (BinarySearch.ItemExists(dictKeysSorted, newNumber, out sortedIndex))
                {
                    // if the new number has appeared before in the memory game the appearence is updated
                    dictValueSorted[sortedIndex] = new long[] { dictValueSorted[sortedIndex][1], i };
                }
                else
                {
                    // else it is added to the dict
                    dictValueSorted.Insert(sortedIndex, new long[] { 0, i });
                    dictKeysSorted.Insert(sortedIndex, newNumber);
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
