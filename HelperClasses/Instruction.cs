using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public static class InstructionMethods
    {
        //
        // creates a list of instruction based on the raw VM
        public static List<Instruction> CreateVM(List<string> rawInstructions)
        {
            List<Instruction> instructions = new List<Instruction>();
            
            // creates list of basic Instructions
            for (int i = 0; i < rawInstructions.Count; i++)
            {
                instructions.Add(CreateInstruction(rawInstructions[i], i));
            }

            // finds former indices for all instructions
            UpdateFormerIndices(ref instructions, false);
            
            return instructions;
        }
        
        //
        // Updates the list of indices that can lead directly to an instruction.
        public static void UpdateFormerIndices(ref List<Instruction> instructions, bool reset = true)
        {
            if (reset)
            {
                // resets all former indices lists.
                foreach (Instruction instruction in instructions)
                {
                    instruction.FormerIndices = new List<int>();
                }
            }


            // updates the new list
            Instruction tempInstr;
            for (int i = 0; i < instructions.Count; i++)
            {
                tempInstr = instructions[i];
                if (tempInstr.NextIndex > 0 && tempInstr.NextIndex < instructions.Count)
                {
                    instructions[tempInstr.NextIndex].FormerIndices.Add(tempInstr.Index);
                }
            }
        }
        
        //
        // Creates a single instruction based on a line of VM string
        private static Instruction CreateInstruction(string rawInstruction, int index)
        {
            string[] splitData = rawInstruction.Split(' ');
            string instrName = splitData[0];
            int argument = Int32.Parse(splitData[1]);

            //switch case easily expandable
            switch (instrName)
            {
                case "acc":
                    return new Acc(argument, index);
                case "jmp":
                    return new Jmp(argument, index);
                case "nop":
                    return new Nop(argument, index);
                default:
                    return new Nop(argument, index);
            }
        }
        
        //
        // From a List of Instructions and a Index, travels backwards and makes a list of all instructions visited that way.
        public static List<int> BackTravelFromIndex(List<Instruction> instructions, int fromIndex)
        {
            // Uses Graph traversal logic - the list of instructions never change.
            List<int> visitedInstructions = new List<int>();
            Instruction currInstr = instructions[fromIndex];

            // always add current index to the list
            visitedInstructions.Add(currInstr.Index);
            
            // returns from here, if no instructions leads to the current instruction
            if (currInstr.FormerIndices.Count == 0)
                return visitedInstructions;
            
            foreach (int index in currInstr.FormerIndices)
            {
                visitedInstructions.AddRange(InstructionMethods.BackTravelFromIndex(instructions, index));
            }
            return visitedInstructions;
        }
        
        //
        // Swaps the type of instruction
        public static Instruction SwapType<T>(Instruction instruction) where T : Instruction
        {
            instruction = (T)Activator.CreateInstance(typeof(T), instruction);
            return instruction;
        }
        
    }

    // The abstract instruction class - all Instructions will be derived from this.
    // Instruction itself is not callable.
    public abstract class Instruction
    {
        // Index of the instruction in the VM and the next index and the one that comes before
        private int _index;

        // List of Indexes that leads directly to this instruction
        public List<int> FormerIndices { get; set; }
        
        // The instruction data
        private int _argument;

        // The calculated index that follows this instruction
        public int NextIndex { get => GetNextIndex(); }
        
        public int Argument { get => _argument; }
        public int Index { get => _index; }
        

        public Instruction(int argument, int index)
        {
            FormerIndices = new List<int>();
            _index = index;
            _argument = argument;
        }
        
        //
        // Overload method used if switching from one type of instruction to another
        public Instruction(Instruction instruction)
        {
            FormerIndices = new List<int>();
            _index = instruction.Index;
            _argument = instruction.Argument;
        }

        // What action should be peformed by the instruction
        public abstract void Action(ref int index, ref int accumulator);
        // if the action needs to be reverted
        public abstract void RevertAction(ref int index, ref int accumulator);

        // finds the next index in the vm, by running it's action against its own index
        public int GetNextIndex()
        {
            int thrownInt = 0;
            int nextIndex = Index;
            Action(ref nextIndex, ref thrownInt);
            return nextIndex;
        }
    }
    
    //
    // acc class - adds it's argument to accumulator and goes to next instruction
    public class Acc : Instruction
    {
        public Acc(int argument, int index) : base(argument, index)
        {
        }
        public Acc(Instruction instruction) : base(instruction)
        {
        }

        // Accs increase the index and adds its argument to the accumelator
        public override void Action(ref int index, ref int accumelator)
        {
            index++;
            accumelator += Argument;
        }

        public override void RevertAction(ref int index, ref int accumulator)
        {
            index--;
            accumulator -= Argument;
        }
    }

    //
    // Jmp class - jumps the next instruction by adding its argument to its index
    public class Jmp : Instruction
    {
        public Jmp(int argument, int index) : base(argument, index)
        {
        }
        public Jmp(Instruction instruction) : base(instruction)
        {
        }

        // jmps increase the index by the instructions argument
        public override void Action(ref int index, ref int accumulator) => index += Argument;

        public override void RevertAction(ref int index, ref int accumulator) => index -= Argument;

    }

    //
    // Nop class - goes to next instruction
    public class Nop : Instruction
    {
        public Nop(int argument, int index) : base(argument, index)
        {
        }
        public Nop(Instruction instruction) : base(instruction)
        {
        }

        // Nops only action is increase the index by one
        public override void Action(ref int index, ref int accumulator) => index++;

        public override void RevertAction(ref int index, ref int accumulator) => index--;
    }
}
