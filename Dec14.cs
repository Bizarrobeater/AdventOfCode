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


        // Get the sum of values of all the memories
        // Correct answer: 9.615.006.043.476
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


        // bitmask now changes the memory address to include floating values
        // meaning addresses are masked and can contain several addresses as X's are now floating
        public override long Solution2()
        {
            MemoryListAdv memory = null;
            foreach (string instruction in dataList)
            {
                if (memory == null)
                    memory = new MemoryListAdv(instruction);
                else
                {
                    memory.NewInstruction(instruction);
                }
            }
            return memory.SumOfMemory;
        }

        internal class MemoryList
        {
            internal List<Memory> memories = null;
            internal string currentBitmask;

            public long SumOfMemory { get => GetSumOfMemories(); }

            public MemoryList(string firstInstruction)
            {
                UpdateBitmask(firstInstruction);
            }

            public virtual void NewInstruction(string instruction)
            {
                if (instruction.Substring(0, 4) == "mask")
                    UpdateBitmask(instruction);
                else
                {
                    int memAddress;
                    string valueToAdd;
                    SplitInstruction(instruction, out memAddress, out valueToAdd);
                    AddToMemory(memAddress, valueToAdd);
                }    
            }

            internal void SplitInstruction(string instruction, out int memAddress, out string valueToAdd)
            {
                Regex rx = new Regex(@"mem\[(\d+)\] = (\d+)");
                Match match = rx.Match(instruction);
                GroupCollection groups = match.Groups;

                memAddress = Int32.Parse(groups[1].ToString());
                valueToAdd = groups[2].ToString();
            }

            internal virtual void UpdateBitmask(string bitmask)
            {
                string[] temp = bitmask.Split(" = ");
                currentBitmask = temp[1];
            }

            internal virtual void AddToMemory(long memAddress, string valueToAdd)
            {
                char[] binValueToAdd = ConvertLongToBin(valueToAdd);

                if (memories == null)
                {
                    memories = new List<Memory>
                    {
                        MemoryConstr.NewMemory(memAddress, binValueToAdd, currentBitmask),
                    };
                }
                else if (memories.Exists(x => x.Address == memAddress))
                {
                    memories.Find(x => x.Address == memAddress).UpdateBinaryValue(binValueToAdd, currentBitmask);
                }
                else
                {
                    memories.Add(MemoryConstr.NewMemory(memAddress, binValueToAdd, currentBitmask));
                }
            }

            // Converts long to binary Int36
            internal char[] ConvertLongToBin(string value)
            {
                long tempLong = Int64.Parse(value);
                string binaryValueRev = "";
                for (int i = 35; i >= 0; i--)
                {
                    if ((int)(tempLong / Math.Pow(2, i)) == 0)
                    {
                        binaryValueRev += "0";
                    }
                    else
                    {
                        binaryValueRev += "1";
                        tempLong -= (long)Math.Pow(2, i);
                    }
                }
                char[] binaryValue = binaryValueRev.ToCharArray();
                return binaryValue;
            }

            internal long GetSumOfMemories()
            {
                long sum = 0;
                foreach(Memory memory in memories)
                {
                    sum += ConvertBinToLong(memory.BinaryValue);
                }
                return sum;
            }

            // translate the binary value to long
            // (damn you int36)
            internal long ConvertBinToLong(char[] binary)
            {
                long value = 0;
                char[] binaryRev = binary;
                Array.Reverse(binaryRev);
                for (int i = 0; i < binaryRev.Length; i++)
                {
                    value += (long)Math.Pow(2, i) * Int32.Parse(binaryRev[i].ToString());
                }
                return value;
            }
        }

        internal class MemoryListAdv : MemoryList
        {
            private string currentAddressBitmask;
            public MemoryListAdv(string firstInstruction) : base(firstInstruction)
            {

            }

            internal override void UpdateBitmask(string bitmask)
            {
                base.UpdateBitmask(bitmask);
                currentAddressBitmask = currentBitmask;
                currentBitmask = "";
            }

            public override void NewInstruction(string instruction)
            {
                if (instruction.Substring(0, 4) == "mask")
                {
                    UpdateBitmask(instruction);
                    return;
                }

                int memBaseAddress;
                string valueToAdd;
                SplitInstruction(instruction, out memBaseAddress, out valueToAdd);

                char[] baseAddress = ConvertLongToBin(memBaseAddress.ToString());
                // makes a address list starting with 1 empty address
                List<string> memAddressesBin = new List<string>
                {
                    "",
                };

                for (int i = 0; i < baseAddress.Length; i++)
                {
                    if (currentAddressBitmask[i] == '0')
                    {
                        UpdateListStrings(ref memAddressesBin, baseAddress[i]);
                    }
                    else if (currentAddressBitmask[i] == '1')
                    {
                        UpdateListStrings(ref memAddressesBin, '1');
                    }
                    // makes a copy of the current list
                    else
                    {
                        // makes a copy of the address list
                        List<string> tempList = new List<string>(memAddressesBin);
                        // address list gets added 1
                        UpdateListStrings(ref memAddressesBin, '1');
                        // copy gets added 0
                        UpdateListStrings(ref tempList, '0');
                        // copy is rolled into addresslist
                        memAddressesBin.AddRange(tempList);
                    }
                }

                long address;
                char[] reversedAddress;
                foreach (string binAddress in memAddressesBin)
                {
                    reversedAddress = binAddress.ToCharArray();
                    Array.Reverse(reversedAddress);
                    address = ConvertBinToLong(reversedAddress);
                    AddToMemory(address, valueToAdd);
                }
            }

            private void UpdateListStrings(ref List<string> listOfStrings, char c)
            {
                for (int i = 0; i < listOfStrings.Count; i++)
                {
                    listOfStrings[i] += c;
                }
            }
        }

        static class MemoryConstr
        {
            static public Memory NewMemory(long address, char[] addedValue, string currentBitmask = "")
            {
                if (currentBitmask == "")
                    return new Memory(address, addedValue);
                else
                    return new MemoryAdv(address, addedValue, currentBitmask);
            }
        }

        internal class Memory : IEquatable<Memory>
        {
            public long Address { get; init; }
            internal char[] _binaryValue;
            public char[] BinaryValue { get => _binaryValue; }

            public Memory(long address, char[] addedValue)
            {
                this.Address = address;
                string tempString = "";
                // initialises the binaryvalue at 0
                for (int i = 0; i < 36; i++)
                {
                    tempString += "0";
                }

                _binaryValue = tempString.ToCharArray();
                UpdateBinaryValue(addedValue);
            }

            // Updates the binary value based on the input and bitmask
            public virtual void UpdateBinaryValue(char[] addedValue, string currentBitMask = "")
            {           
                _binaryValue = addedValue;
            }

            // Used to find existing memories
            public bool Equals(Memory other)
            {
                if (other == null) return false;
                return (this.Address.Equals(other.Address));
            }
        }

        internal class MemoryAdv : Memory
        {
            public MemoryAdv(long address, char[] addedValue, string currentBitmask) : base(address, addedValue)
            {
                //this.Address = address;
                //string tempString = "";
                //// initialises the binaryvalue at 0
                //for (int i = 0; i < 36; i++)
                //{
                //    tempString += "0";
                //}

                //_reversedBinaryValue = tempString.ToCharArray();
                UpdateBinaryValue(addedValue, currentBitmask);
            }

            public override void UpdateBinaryValue(char[] addedValue, string currentBitmask)
            {
                if (currentBitmask == "")
                    return;

                char[] reversedBitmask = currentBitmask.ToCharArray();

                for (int i = 0; i < reversedBitmask.Length; i++)
                {
                    if (reversedBitmask[i] != 'X')
                    {
                        _binaryValue[i] = reversedBitmask[i];
                    }
                    else
                    {
                        _binaryValue[i] = addedValue[i];
                    }
                }
            }
        }
    }
}
