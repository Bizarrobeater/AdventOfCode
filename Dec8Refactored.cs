using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    class Dec8Refactored
    {
        List<string> dataList;
        List<Instruction> allInstructions = new List<Instruction>();

        public Dec8Refactored()
        {
            dataList = ReadDataFile.FileToListSimple("AdventCode8Dec.txt");
            CreateInstructions();
        }

        public Dec8Refactored(List<string> testList)
        {
            dataList = testList;
            CreateInstructions();
        }

        private void CreateInstructions()
        {
            allInstructions = InstructionMethods.CreateVM(dataList);
        }


        // Finds total of accumulator at the start of the infinite loop
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

        // Flips exactly one(1) instruction from Nop->Jmp or Jmp->Nop so the program can run all the way through.
        // Prints the accumulator after that.
        public int SolutionPart2()
        {
            // list of all indices of Instructions that can be reached if starting from the last instruction and working backwards
            List<int> vmReversed = InstructionMethods.BackTravelFromIndex(allInstructions, allInstructions.Count - 1);

            int accumulator = 0;
            // VM should run from the top
            int currentIndex = 0;
            // Current instruction in the loop and a tempInstruction used for switching
            Instruction currInstr;
            Instruction tempInstr = null;

            // if the switch is found, no reason to test anymore
            bool switchFound = false;
            
            // Program should stop when the index reaches the end
            while (currentIndex != allInstructions.Count)
            {
                currInstr = allInstructions[currentIndex];

                // test for Instruction type and makes a temp swapped version of it.
                if (!switchFound && currInstr is Nop)
                    tempInstr = InstructionMethods.SwapType<Jmp>(currInstr);
                else if (!switchFound && currInstr is Jmp)
                    tempInstr = InstructionMethods.SwapType<Nop>(currInstr);

                // if the next index of the swapped instruction is in the vmreversed list - 
                // then the switch will cause the program to complete
                if (tempInstr != null && vmReversed.Contains(tempInstr.NextIndex))
                {
                    // current instruction is set to the swapped
                    currInstr = tempInstr;
                    switchFound = true;
                }
                // current instruction runs its action against index and accumulator
                currInstr.Action(ref currentIndex, ref accumulator);
                // the temp instruction is removed
                tempInstr = null;
            }
            return accumulator;
        }
    }
}
