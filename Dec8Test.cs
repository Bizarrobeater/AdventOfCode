using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Dec8Test
    {
        List<string> dataList;
        List<Instruction> allInstructions = new List<Instruction>();

        public Dec8Test()
        {
            dataList = ReadDataFile.FileToListSimple("AdventCode8Dec.txt");
            CreateInstructions();
        }

        public Dec8Test(List<string> testList)
        {
            dataList = testList;
            CreateInstructions();
        }

        private void CreateInstructions()
        {
            allInstructions = InstructionMethods.CreateVM(dataList);
        }

        public int SolutionPart1()
        {
            List<Instruction> visistedInstr = new List<Instruction>();
            int currentIndex = 0;
            Instruction currentInstr = allInstructions[currentIndex];
            int accumulator = 0;

            while (!visistedInstr.Contains(currentInstr))
            {
                visistedInstr.Add(currentInstr);
                currentInstr.Action(ref currentIndex, ref accumulator);
                currentInstr = allInstructions[currentIndex];
            }
            return accumulator;
        }

        //public void testCount()
        //{
        //    Dictionary<int, int> counter = new Dictionary<int, int>();
        //    int key;
        //    foreach (Instruction instr in allInstructions)
        //    {
        //        key = instr.FormerIndices.Count;
        //        if (counter.ContainsKey(key))
        //            counter[key]++;
        //        else
        //            counter.Add(key, 1);
        //    }

        //    foreach (int intKey in counter.Keys)
        //    {
        //        Console.WriteLine($"Former Indices {intKey} - Count: {counter[intKey]}");
        //    }
        //}


        public int SolutionPart2()
        {
            List<int> vmReversed = InstructionMethods.BackTravelFromIndex(allInstructions, allInstructions.Count - 1);

            int accumulator = 0;
            int currentIndex = 0;
            int tempIndex;
            Instruction currInstr;
            Instruction tempInstr = null;

            bool switchFound = false;
            while (currentIndex != allInstructions.Count)
            {
                currInstr = allInstructions[currentIndex];
                tempIndex = currentIndex;

                if (!switchFound && currInstr is Nop)
                    tempInstr = InstructionMethods.SwapType<Jmp>(currInstr);
                else if (!switchFound && currInstr is Jmp)
                    tempInstr = InstructionMethods.SwapType<Nop>(currInstr);

                if (tempInstr != null && vmReversed.Contains(tempInstr.NextIndex))
                {
                    currInstr = tempInstr;
                    switchFound = true;
                }
                currInstr.Action(ref currentIndex, ref accumulator);
                tempInstr = null;
            }
            
            return accumulator;
        }

        
    }
}
