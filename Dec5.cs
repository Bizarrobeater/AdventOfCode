using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    class Dec5 : ISolution
    {
        List<string> dataList;
        List<Seat> seatsSorted = new List<Seat>();
        List<Seat> seatSortedTest = new List<Seat>();

        public Dec5()
        {
            dataList = ReadDataFile.FileToListSimple("AdventCode5Dec.txt");
            foreach (string rawSeat in dataList)
            {
                seatsSorted.Add(new Seat(rawSeat));
                seatSortedTest.Add(new Seat(rawSeat));
            }
            // Sorts the seats based on seatId
            seatsSorted.Sort((x, y) => x.SeatId.CompareTo(y.SeatId));
            seatSortedTest.Sort((x, y) => x.CompareTo(y));
        }

        public void test()
        {
            List<int> intList = new List<int> { 1, 5, 10, 78, 750, 201 };

            foreach (int i in intList)
            {
                seatsSorted[i].PrintSeatData();
                seatSortedTest[i].PrintSeatData();
                Console.WriteLine("\n\n");
            }
            Console.Write(seatsSorted == seatSortedTest);
        }

        public int SolutionPart1()
        {
            return seatsSorted[seatsSorted.Count - 1].SeatId;
        }

        public int SolutionPart2()
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

                binaryRow = rawRow.Replace("F", "0").Replace("B", "1");
                binaryCol = rawCol.Replace("L", "0").Replace("R", "1");

                row = Convert.ToInt32(binaryRow, 2);
                column = Convert.ToInt32(binaryCol, 2);
            }

            public void PrintSeatData()
            {
                Console.WriteLine($"Row: Raw = {binaryRow}, int = {row}");
                Console.WriteLine($"Col: Raw = {binaryCol}, int = {column}");
                Console.WriteLine($"{SeatId}");
            }

            private int GetSeatId()
            {
                return row * 8 + column;
            }

            public int CompareTo(Seat compareSeat)
            {
                return this.SeatId.CompareTo(compareSeat.SeatId);
            }
        }

    }
}
