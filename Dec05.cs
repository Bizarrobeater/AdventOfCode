using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    class Dec05 : AdventCodeBase<string, int>
    {
        List<Seat> seatsSorted = new List<Seat>();

        public Dec05() : base(ReadDataFile.FileToListSimple)
        {
            foreach (string rawSeat in dataList)
            {
                seatsSorted.Add(new Seat(rawSeat));
            }
            // Sorts the seats based on seatId
            seatsSorted.Sort((x, y) => x.SeatId.CompareTo(y.SeatId));
        }
        
        //
        // basic solution, returns the highest seat id.
        // Correct answer: 935
        public override int Solution1()
        {
            return seatsSorted[seatsSorted.Count - 1].SeatId;
        }
        
        //
        // Finds a seat with no boarding pass associated. 
        // Missing seats at the start and end, correct answer is a missing seat id
        // that has both it neighbours
        // Correct answer: 743
        public override int Solution2()
        {
            int neighborDown = seatsSorted[0].SeatId;
            int current;
            for (int i = 1; i < seatsSorted.Count; i++)
            {
                current = seatsSorted[i].SeatId;
                if (current - 2 == neighborDown)
                {
                    return current - 1;
                }
                neighborDown = current;
            }
            return -1;
        }
        
        //
        // class for seats, contains the binary information and the int information for rows and columns
        private class Seat
        {
            string binaryRow;
            string binaryCol;

            int row;
            int column;
            public int SeatId { get => GetSeatId(); }

            public Seat(string rawSeat)
            {
                string rawRow = rawSeat.Substring(0, 7);
                string rawCol = rawSeat.Substring(7, 3);

                // converts the letters in the code to binary
                binaryRow = rawRow.Replace("F", "0").Replace("B", "1");
                binaryCol = rawCol.Replace("L", "0").Replace("R", "1");

                row = Convert.ToInt32(binaryRow, 2);
                column = Convert.ToInt32(binaryCol, 2);
            }

            // converts seat information to a seat id
            private int GetSeatId()
            {
                return row * 8 + column;
            }

            // comparer that used for sorting
            public int CompareTo(Seat compareSeat)
            {
                return this.SeatId.CompareTo(compareSeat.SeatId);
            }
        }

    }
}
