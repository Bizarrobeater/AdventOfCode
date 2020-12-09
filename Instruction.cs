using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public static class InstructionConstructor
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
            int thrownInt = 0;
            int tempIndex;
            Instruction tempInstr;
            for (int i = 0; i < instructions.Count; i++)
            {
                tempIndex = i;
                tempInstr = instructions[i];
                tempInstr.Action(ref tempIndex, ref thrownInt);
                if (tempIndex > 0 && tempIndex < instructions.Count)
                {
                    tempInstr.NextIndex = tempIndex;
                    instructions[tempIndex].FormerIndex= tempInstr.Index;
                }
            }
            return instructions;
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
    }


    public abstract class Instruction
    {
        // Index of the instruction in the VM and the next index and the one that comes before
        private int _index;
        
        // The next Index, and the Index that points here.
        private Instruction _nextInstruct = null;
        private Instruction _formerInstruct = null;

        private int _nextIndex = -1;
        private int _formerIndex = -1;

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
        public int NextIndex { get => _nextIndex; set => _nextIndex = value; }
        public int FormerIndex { get => _formerIndex; set => _formerIndex = value; }

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
