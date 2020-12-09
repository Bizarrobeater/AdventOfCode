using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public static class InstructionMethods
    {
        // creates a list of instruction based on the raw VM
        public static List<Instruction> CreateVM(List<string> rawInstructions)
        {
            List<Instruction> instructions = new List<Instruction>();
            // creates list of basic Instructions
            for (int i = 0; i < rawInstructions.Count; i++)
            {
                instructions.Add(CreateInstruction(rawInstructions[i], i));
            }

            UpdateFormerIndices(ref instructions);
            
            return instructions;
        }

        public static void UpdateFormerIndices(ref List<Instruction> instructions)
        {
            // resets all former indices lists.
            foreach (Instruction instruction in instructions)
            {
                instruction.FormerIndices = new List<int>();
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

        // Creates a single instruction based on the VM
        private static Instruction CreateInstruction(string rawInstruction, int index)
        {
            string[] splitData = rawInstruction.Split(' ');
            string instrName = splitData[0];
            int argument = Int32.Parse(splitData[1]);

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

        // From a List of Instructions and a Index, travels backwards and makes a list of all instructions visited that way.
        public static List<int> BackTravelFromIndex(List<Instruction> instructions, int fromIndex)
        {
            List<int> visitedInstructions = new List<int>();
            Instruction currInstr = instructions[fromIndex];

            visitedInstructions.Add(currInstr.Index);            
            if (currInstr.FormerIndices.Count == 0)
                return visitedInstructions;
            
            foreach (int index in currInstr.FormerIndices)
            {
                visitedInstructions.AddRange(InstructionMethods.BackTravelFromIndex(instructions, index));
            }
            return visitedInstructions;
        }

        // Swaps the type of instruction
        public static Instruction SwapType<T>(Instruction instruction) where T : Instruction
        {
            instruction = (T)Activator.CreateInstance(typeof(T), instruction);
            return instruction;
        }
        
    }


    public abstract class Instruction
    {
        // Index of the instruction in the VM and the next index and the one that comes before
        private int _index;
        
        // The next Index, and the Index that points here.
        private Instruction _nextInstruct = null;
        private Instruction _formerInstruct = null;

        private List<int> _formerIndices = new List<int>();

        // The instruction data
        private int _argument;
        
        // Is it the original type
        private bool _origInstruct = true;

        // If the instruction have been switched - this is the original
        private Instruction _oldInstruct = null;
        //private  _oldInstruct = null;

        public int Argument { get => _argument; }
        public bool OrigInstruct { get => _origInstruct; }
        public int Index { get => _index; }
        internal Instruction OldType { get => _oldInstruct; }
        public Instruction NextInstruct { get => _nextInstruct; set => _nextInstruct = value; }
        public Instruction FormerInstruct { get => _formerInstruct; set => _formerInstruct = value; }
        public int NextIndex { get => GetNextIndex(); }
        public List<int> FormerIndices{ get => _formerIndices; set => _formerIndices = value; }

        public Instruction(int argument, int index)
        {
            _index = index;
            _argument = argument;
        }

        // Overload method used if switching from one instruction to another
        public Instruction(Instruction instruction)
        {
            _index = instruction.Index;
            _argument = instruction.Argument;
            _origInstruct = !instruction.OrigInstruct;
            _oldInstruct = instruction;
        }

        public abstract void Action(ref int index, ref int accumulator);
        public abstract void RevertAction(ref int index, ref int accumulator);

        public int GetNextIndex()
        {
            int thrownInt = 0;
            int nextIndex = this.Index;
            Action(ref nextIndex, ref thrownInt);
            return nextIndex;
        }

    }

    public class Acc : Instruction
    {
        public Acc(int argument, int index) : base(argument, index)
        {
        }

        public Acc(Instruction instruction) : base(instruction)
        {
        }
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

    public class Jmp : Instruction
    {
        public Jmp(int argument, int index) : base(argument, index)
        {

        }

        public Jmp(Instruction instruction) : base(instruction)
        {

        }

        public override void Action(ref int index, ref int accumulator)
        {
            index += Argument;
        }

        public override void RevertAction(ref int index, ref int accumulator)
        {
            index -= Argument;
        }
    }

    public class Nop : Instruction
    {
        public Nop(int argument, int index) : base(argument, index)
        {

        }

        public Nop(Instruction instruction) : base(instruction)
        {

        }

        public override void Action(ref int index, ref int accumulator)
        {
            index++;
        }

        public override void RevertAction(ref int index, ref int accumulator)
        {
            index--;
        }
    }
}
