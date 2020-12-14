using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Dec14 : AdventCodeBase<string, long>
    {
        public Dec14() : base(ReadDataFile.FileToListSimple)
        {
        }

        public Dec14(List<string> testData) : base(testData)
        {
        }

        public override long Solution1()
        {
            MemoryList memory = null;
            foreach (string instruction in dataList)
            {
                if (memory == null)
                    memory = new MemoryList(instruction);
                else
                {
                    memory.NewInstruction(instruction);
                }
            }
            return memory.SumOfMemory;
        }

        public override long Solution2()
        {
            throw new NotImplementedException();
        }

        internal class MemoryList
        {
            List<Memory> memories = null;
            private string currentBitmask;

            public long SumOfMemory { get => GetSumOfMemories(); }

            public MemoryList(string firstInstruction)
            {
                UpdateBitmask(firstInstruction);
            }

            public void NewInstruction(string instruction)
            {
                if (instruction.Substring(0, 4) == "mask")
                    UpdateBitmask(instruction);
                else
                    AddToMemory(instruction);
            }

            private void UpdateBitmask(string bitmask)
            {
                string[] temp = bitmask.Split(" = ");
                currentBitmask = temp[1];
            }

            private void AddToMemory(string instruction)
            {
                Regex rx = new Regex(@"mem\[(\d+)\] = (\d+)");
                Match match = rx.Match(instruction);
                GroupCollection groups = match.Groups;

                int memId = Int32.Parse(groups[1].ToString());
                string valueToAdd = groups[2].ToString();
                
                if (memories == null)
                {
                    memories = new List<Memory>
                    {
                        new Memory(memId, valueToAdd, currentBitmask),
                    };
                }
                else if (memories.Exists(x => x.Id == memId))
                {
                    memories.Find(x => x.Id == memId).UpdateBinaryValue(valueToAdd, currentBitmask);
                }
                else
                {
                    memories.Add(new Memory(memId, valueToAdd, currentBitmask));
                }
            }

            private long GetSumOfMemories()
            {
                long sum = 0;
                foreach(Memory memory in memories)
                {
                    sum += memory.value;
                }
                return sum;
            }
        }

        internal class Memory : IEquatable<Memory>
        {
            public int Id { get; init; }
            private char[] reversedBinaryValue;

            public long value { get => GetValue(); }

            public Memory(int Id, string addedValue, string currentBitmask)
            {
                this.Id = Id;
                string tempString = "";
                // initialises the binaryvalue at 0
                for (int i = 0; i < 36; i++)
                {
                    tempString += "0";
                }

                reversedBinaryValue = tempString.ToCharArray();
                UpdateBinaryValue(addedValue, currentBitmask);
            }

            // Updates the binary value based on the input and bitmask
            public void UpdateBinaryValue(string addedValue, string currentBitmask)
            {
                // reverses the memory so it goes 2^0 -> 2^35                
                char[] reversedBitmask = currentBitmask.ToCharArray();
                Array.Reverse(reversedBitmask);

                char[] reversedBinary = ConvertValueToBin(addedValue);
                Array.Reverse(reversedBinary);

                for (int i = 0; i < reversedBitmask.Length; i++)
                {
                    if (reversedBitmask[i] != 'X')
                    {
                        reversedBinaryValue[i] = reversedBitmask[i];
                    }
                    else if (i < reversedBinary.Length)
                    {
                        reversedBinaryValue[i] = reversedBinary[i];
                    }
                }
            }

            private char[] ConvertValueToBin(string value)
            {
                long tempLong = Int64.Parse(value);
                string binaryValue = "";
                for (int i = 35; i >= 0; i--)
                {
                    if ((int)(tempLong / Math.Pow(2, i)) == 0)
                    {
                        binaryValue += "0";
                    }
                    else
                    {
                        binaryValue += "1";
                        tempLong -= (long)Math.Pow(2, i);
                    }
                }

                return binaryValue.ToCharArray();
            }


            // translate the binary value to long
            // (damn you int36)
            private long GetValue()
            {
                long value = 0;
                for (int i = 0; i < reversedBinaryValue.Length; i++)
                {
                    value += (long)Math.Pow(2, i) * Int32.Parse(reversedBinaryValue[i].ToString());
                }
                return value;
            }


            // Used to find existing memories
            public bool Equals(Memory other)
            {
                if (other == null) return false;
                return (this.Id.Equals(other.Id));
            }

        }
    }
}
