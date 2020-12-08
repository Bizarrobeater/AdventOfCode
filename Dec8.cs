using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Dec8 : ISolution
    {
        List<string> dataList;
        List<Instruction> allInstructions = new List<Instruction>();

        public Dec8()
        {
            dataList = ReadDataFile.FileToListSimple("AdventCode8Dec.txt");
            for (int i = 0; i < dataList.Count; i++)
            {
                allInstructions.Add(new Instruction(dataList[i], i));
            }
        }

        public Dec8(List<string> testList)
        {
            dataList = testList;
            for (int i = 0; i < dataList.Count; i++)
            {
                allInstructions.Add(new Instruction(dataList[i], i));
            }
        }

        public int SolutionPart1()
        {
            //Regex rx = new Regex(@"(acc|jmp|nop) (\+|\-)(\d+)");
            List<int> visitedInstructions = new List<int>();
            int currentInstruction = 0;
            int accumulator = 0;

            string[] splitData;
            string operation;
            int argument;
            while (!visitedInstructions.Contains(currentInstruction))
            {
                visitedInstructions.Add(currentInstruction);
                splitData = dataList[currentInstruction].Split(' ');
                operation = splitData[0];
                argument = Int32.Parse(splitData[1]);

                switch (operation)
                {
                    case "nop":
                        currentInstruction++;
                        break;
                    case "acc":
                        accumulator += argument;
                        currentInstruction++;
                        break;
                    case "jmp":
                        currentInstruction += argument;
                        break;
                }
                

            }
            return accumulator;
        }



        public int SolutionPart2()
        {
            Stack<Instruction> visitInstruct = new Stack<Instruction>();
            List<Instruction> triedSwitched = new List<Instruction>();
            int currIndex = 0;
            int accumulator = 0;

            
            Instruction currInstruction;

            while (currIndex < allInstructions.Count)
            {
                currInstruction = allInstructions[currIndex];
                
                if (visitInstruct.Contains(currInstruction))
                {
                    Instruction popInstruction;
                    do
                    {
                        popInstruction = visitInstruct.Pop();
                        switch (popInstruction.Operation)
                        {
                            case Operation.nop:
                                currIndex--;
                                break;
                            case Operation.acc:
                                accumulator -= popInstruction.Argument;
                                currIndex--;
                                break;
                            case Operation.jmp:
                                currIndex -= popInstruction.Argument;
                                break;
                        }
                        if (popInstruction.Switched)
                            popInstruction.SwitchOperation();
                        
                    } while (popInstruction.Operation == Operation.acc && triedSwitched.Contains(popInstruction));
                    triedSwitched.Add(popInstruction);
                    popInstruction.SwitchOperation();
                    currInstruction = popInstruction;
                }
                
                switch (currInstruction.Operation)
                {
                    case Operation.nop:
                        currIndex++;
                        break;
                    case Operation.acc:
                        accumulator += currInstruction.Argument;
                        currIndex++;
                        break;
                    case Operation.jmp:
                        currIndex += currInstruction.Argument;
                        break;
                }

                visitInstruct.Push(currInstruction);
            }

            return accumulator;
        }

        public enum Operation
        {
            acc,
            nop,
            jmp,
        }

        private class InstructionComparer : EqualityComparer<Instruction>
        {
            public override bool Equals(Instruction x, Instruction y)
            {
                return x.Index == y.Index;
            }

            public override int GetHashCode([DisallowNull] Instruction obj)
            {
                return 0;
            }
        }


        private class Instruction
        {
            int _index;
            Operation _operation;
            int _argument;
            bool switched = false;
            internal Operation Operation { get => _operation; set => _operation = value; }
            public int Argument { get => _argument;  }
            public bool Switched { get => switched; set => switched = value; }
            public int Index { get => _index; }

            public Instruction(string stringInstruction, int index)
            {
                _index = index;

                string[] splitData = stringInstruction.Split(' ');
                Operation = SetOperation(splitData[0]);
                _argument = Int32.Parse(splitData[1]);
            }

            private Operation SetOperation(string operation)
            {
                switch (operation)
                {
                    case "acc":
                        return Operation.acc;
                    case "jmp":
                        return Operation.jmp;
                    case "nop":
                        return Operation.nop;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            public void SwitchOperation()
            {
                if (Operation == Operation.acc)
                {
                    return;
                }
                
                switched = !switched;
                
                if (Operation == Operation.nop)
                    Operation = Operation.jmp;
                else
                    Operation = Operation.nop;
            } 



        }

    }
}
