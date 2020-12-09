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
            allInstructions = InstructionConstructor.CreateVM(dataList);
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



        public int SolutionPart2()
        {
            List<int> vmReversed = new List<int>();
            Instruction reverseInstr = allInstructions[allInstructions.Count - 1];

            while (reverseInstr.FormerInstruct != null)
            {
                vmReversed.Add(reverseInstr.Index);
                reverseInstr = allInstructions[reverseInstr.FormerIndex];
            }

            int accumulator = 0;
            int currentIndex = 0;
            int tempInt;
            Instruction currInstr;;
            while (currentIndex != allInstructions.Count)
            {
                currInstr = allInstructions[currentIndex];
                tempInt = currentIndex;
                if (currInstr.GetType() != typeof(Acc))
                {
                    switch (currInstr.GetType())
                    {
                        case Nop:
                            break;
                        default:
                            break;
                    }
                }

            }
            



            return -1;
            //Stack<Instruction> visitInstruct = new Stack<Instruction>();
            //int currIndex = 0;
            //int accumulator = 0;

            //Instruction currInstruction;
            //Instruction switchedInstruct = null;

            //while (currIndex < allInstructions.Count)
            //{
            //    currInstruction = allInstructions[currIndex];

            //    if (visitInstruct.Contains(currInstruction))
            //    {
            //        Instruction popInstruction;

            //        if (switchedInstruct != null)
            //        {
            //            switchedInstruct.SwitchOperation();
            //            // Pops until switched instruct is hit and removes that as well
            //            do
            //            {
            //                popInstruction = visitInstruct.Pop();
            //                if (popInstruction.Operation == Operation.acc)
            //                {
            //                    accumulator -= popInstruction.Argument;
            //                }

            //            } while (popInstruction != switchedInstruct);
            //        }
            //        popInstruction = visitInstruct.Pop();


            //        // pops until a non acc is hit
            //        while (popInstruction.Operation == Operation.acc)
            //        {
            //            accumulator -= popInstruction.Argument;
            //            popInstruction = visitInstruct.Pop();
            //        }

            //        switchedInstruct = popInstruction;
            //        popInstruction.SwitchOperation();

            //        currInstruction = popInstruction;
            //        currIndex = currInstruction.Index;
            //    }

            //    switch (currInstruction.Operation)
            //    {
            //        case Operation.nop:
            //            currIndex++;
            //            break;
            //        case Operation.acc:
            //            accumulator += currInstruction.Argument;
            //            currIndex++;
            //            break;
            //        case Operation.jmp:
            //            currIndex += currInstruction.Argument;
            //            break;
            //    }
            //    visitInstruct.Push(currInstruction);
            //}
            //return accumulator;
        }

        //public enum Operation
        //{
        //    acc,
        //    nop,
        //    jmp,
        //}

        //private class Instruction
        //{
        //    int _index;
        //    Operation _operation;
        //    int _argument;
        //    bool switched = false;
        //    internal Operation Operation { get => _operation; set => _operation = value; }
        //    public int Argument { get => _argument; }
        //    public bool Switched { get => switched; set => switched = value; }
        //    public int Index { get => _index; }

        //    public Instruction(string stringInstruction, int index)
        //    {
        //        _index = index;

        //        string[] splitData = stringInstruction.Split(' ');
        //        Operation = SetOperation(splitData[0]);
        //        _argument = Int32.Parse(splitData[1]);
        //    }

        //    private Operation SetOperation(string operation)
        //    {
        //        switch (operation)
        //        {
        //            case "acc":
        //                return Operation.acc;
        //            case "jmp":
        //                return Operation.jmp;
        //            case "nop":
        //                return Operation.nop;
        //            default:
        //                throw new ArgumentOutOfRangeException();
        //        }
        //    }

        //    public void SwitchOperation()
        //    {
        //        if (Operation == Operation.acc)
        //        {
        //            return;
        //        }
        //        switched = !switched;

        //        if (Operation == Operation.nop)
        //            Operation = Operation.jmp;
        //        else
        //            Operation = Operation.nop;
        //    }
        //}

    }
}
