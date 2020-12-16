using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Dec16 : AdventCodeBase<string, long>
    {
        ValidRanges validRanges;
        List<int> myTicket; 

        public Dec16() : base(ReadDataFile.FileToListDoubleNewlineDiff)
        {
            CommonConstructor();
        }

        public Dec16(List<string> testData) : base(testData)
        {
            CommonConstructor();
        }

        private void CommonConstructor()
        {
            validRanges = new ValidRanges(dataList[0]);
            myTicket = dataList[1].Split('\n')[1].Split(',').Select(Int32.Parse).ToList();
        }
        
        //
        // Find the sum of invalid numbers in tickets 
        // meaning a number that does not appear in any number Range.
        // Correct answer: 26.988
        public override long Solution1()
        {
            string[] splitTickets = dataList[2].Split('\n');
            int result = 0;
            int[] ticketNumbers;

            for (int i = 1; i < splitTickets.Length; i++)
            {
                ticketNumbers = splitTickets[i].Split(',').Select(Int32.Parse).ToArray();

                foreach (int number in ticketNumbers)
                {
                    result += validRanges.ValidNumber(number) ? 0 : number;
                }
            }
            return result;
        }

        //
        // Find the positions in a ticket equivalent of the 6 classes starting with "departure".
        // Multiply those 6 numbers together
        // Correct answer: 426.362.917.709
        public override long Solution2()
        {
            TicketAnalyser analyser = new TicketAnalyser(validRanges, myTicket.Count);
            string[] splitTickets = dataList[2].Split('\n');
            int[] ticketNumbers;

            for (int i = 1; i < splitTickets.Length; i++)
            {
                ticketNumbers = splitTickets[i].Split(',').Select(Int32.Parse).ToArray();

                if (validRanges.ValidTicket(ticketNumbers))
                    analyser.AddTicket(ticketNumbers);
            }
            
            analyser.AnalyseRanges();
                       
            long result = 1;
            foreach (KeyValuePair<string, List<int>> kvp in analyser.PotentialPositions)
            {
                if (kvp.Key.StartsWith("departure"))
                    result *= myTicket[kvp.Value[0]];
            }
            return result;
        }

        internal class TicketAnalyser
        {
            ValidRanges validRanges;
            // dictionary of potential positions for keys
            Dictionary<string, List<int>> potentPos = new Dictionary<string, List<int>>();
            // Keys that has only 1 position
            List<string> lockedKeys = new List<string>();

            public Dictionary<string, List<int>> PotentialPositions { get => potentPos; }

            public TicketAnalyser(ValidRanges valid, int ticketLength)
            {
                validRanges = valid;
                foreach (string key in validRanges.namesAndRanges.Keys)
                {
                    // all classes are initialised with all potential positions on the ticket
                    potentPos[key] = Enumerable.Range(0, ticketLength).ToList();
                }
            }
            
            //
            // When a valid ticket is analysed
            // if a position doesn't fit a certain range, it is removed from that range.
            public void AddTicket(int[] ticketNumbers)
            {
                int number;
                // check each position on a ticket
                for (int i = 0; i < ticketNumbers.Length; i++)
                {
                    number = ticketNumbers[i];

                    foreach (string key in validRanges.namesAndRanges.Keys)
                    {
                        // if a number on a ticket position does not fit a specific class' ranges,
                        // then that position is removed as a potential position
                        if (!validRanges.ValidNumberInKey(number, key))
                            potentPos[key].Remove(i);
                    }
                }
            }
            
            //
            // Goes through the potential positions
            // if a class only has 1 (one) potential positions, removes this position from all other classes
            // this continues until all classes only have 1 potential position
            public void AnalyseRanges()
            {
                
                bool updated = true;

                while (updated)
                {
                    updated = false;
                    foreach (KeyValuePair<string, List<int>> kvp in potentPos)
                    {
                        if (kvp.Value.Count == 1 && !lockedKeys.Contains(kvp.Key))
                        {
                            lockedKeys.Add(kvp.Key);
                            updated = UpdatePotentialPositions(kvp.Key, kvp.Value[0]) || updated;
                        }
                    }
                }
            }
            
            //
            // removes potential positions from all other keys except the one it was found on.
            // returns bool based on whether or not it updated anything
            private bool UpdatePotentialPositions(string foundKey, int position)
            {
                bool updated = false;
                foreach (string key in potentPos.Keys)
                {
                    if (!lockedKeys.Contains(key))
                    {
                        // if a position is removed and updated == false, then updated changes to true,
                        // if a position is not removed but updated is already true, then updated stays true
                        updated = potentPos[key].Remove(position) || updated;
                    }   
                }
                return updated;
            }
        }


        internal class ValidRanges
        {
            // Dictionary of different classes and the ranges number has to appear in to be valid
            public Dictionary<string, int[][]> namesAndRanges = new Dictionary<string, int[][]>();

            public ValidRanges(string validRanges)
            {
                string[] individualRanges = validRanges.Split('\n');
                string[] splitRange;
                
                Regex rx = new Regex(@"(\d+)-(\d+) or (\d+)-(\d+)");
                Match match;
                GroupCollection groups;
                int[][] tempRange;
                foreach (string range in individualRanges)
                {
                    splitRange = range.Split(": ");
                    match = rx.Match(splitRange[1]);
                    groups= match.Groups;

                    tempRange = new int[][]
                    {
                        new int[]{Int32.Parse(groups[1].ToString()), Int32.Parse(groups[2].ToString()) },
                        new int[]{Int32.Parse(groups[3].ToString()), Int32.Parse(groups[4].ToString()) },
                    };
                    namesAndRanges[splitRange[0]] = tempRange;
                }
            }

            // checks if a number is valid for a specific class
            public bool ValidNumberInKey(int number, string key)
            {
                int[][] range = namesAndRanges[key];
                return ((number >= range[0][0] && number <= range[0][1]) ||
                    (number >= range[1][0] && number <= range[1][1]));
            }


            // Checks if a number appears in any class
            public bool ValidNumber(int number)
            {
                foreach(string key in namesAndRanges.Keys)
                {
                    // if not within the 2 ranges provided
                    if (ValidNumberInKey(number, key))
                        return true;
                }
                return false;
            }

            // Check if all numbers on a ticket appears in any class
            public bool ValidTicket(int[] ticketNumbers)
            {
                foreach (int number in ticketNumbers)
                {
                    if (!ValidNumber(number))
                        return false;
                }
                return true;
            }
        }
    }
}
