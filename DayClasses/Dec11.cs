using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace AdventOfCode
{
    class Dec11 : AdventCodeBase<string, int>
    {
        FloorPlan SeatingPlan;
        public Dec11() : base(ReadDataFile.FileToListSimple)
        {
            SeatingPlan = new FloorPlan(dataList);
        }

        public Dec11(List<string> testData) : base(testData)
        {
            SeatingPlan = new FloorPlan(dataList);
        }

        public void Visualise()
        {
            while (!SeatingPlan.StableSeating)
            {
                Console.Clear();
                SeatingPlan.PrintSeatState();
                SeatingPlan.UpdateSeating(false);
                Thread.Sleep(2000);
                
            }
        }

        public override int Solution1()
        {
            while (!SeatingPlan.StableSeating)
            {
                SeatingPlan.UpdateSeating();
            }
            return SeatingPlan.OccupiedSeats;
        }

        public override int Solution2()
        {
            while (!SeatingPlan.StableSeating)
            {
                SeatingPlan.UpdateSeating(part1: false);
            }
            return SeatingPlan.OccupiedSeats;
        }

        internal class FloorPlan
        {

            List<List<Seat>> Seating = new List<List<Seat>>();
            // size of seating area
            int maxRow;
            int maxCol;

            // bool for showing the seating has stabilised
            public bool StableSeating { get; set; }

            // number of occupied seats in the current plan
            public int OccupiedSeats { get => GetOccupiedSeats(); }
            public FloorPlan(List<string> rawData)
            {
                StableSeating = false;
                maxRow = rawData.Count - 1;
                maxCol = rawData[0].Length - 1;
                // First generates a list of list of seats
                foreach (string row in rawData)
                {
                    List<Seat> rowList = new List<Seat>();
                    foreach(char seat in row)
                    {
                        rowList.Add(new Seat(seat));
                    }
                    Seating.Add(rowList);
                }

                // Then adds all the specific neigbours to individual seats
                for (int row = 0; row <= maxRow; row++)
                {
                    for (int col = 0; col <= maxCol; col++)
                    {
                        AddNeighboursToSeat(row, col);
                    }
                }
            }

            // For fun and profit
            public void PrintSeatState()
            {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (List<Seat> rowSeats in Seating)
                {
                    foreach(Seat seat in rowSeats)
                    {
                        switch (seat.state)
                        {
                            case SeatState.Floor:
                                stringBuilder.Append('.');
                                break;
                            case SeatState.Free:
                                stringBuilder.Append('L');
                                break;
                            case SeatState.Occ:
                                stringBuilder.Append('#');
                                break;
                            default:
                                break;
                        }
                    }
                    stringBuilder.Append(Environment.NewLine);
                }
                Console.Write(stringBuilder.ToString() + Environment.NewLine);
            }

            // Counts the number of occupied seats on a floorplan
            private int GetOccupiedSeats()
            {
                int counter = 0;
                foreach (List<Seat> seatRow in Seating)
                {
                    foreach (Seat seat in seatRow)
                    {
                        if (seat.state == SeatState.Occ)
                            counter++;
                    }
                }
                return counter;
            }

            //
            // Checks each seat and updates it based on the set rules;
            public void UpdateSeating(bool part1 = true)
            {
                // First goes through all seats and checks if they should update
                foreach (List<Seat> seatRow in Seating)  
                {
                    foreach (Seat seat in seatRow)
                    {
                        seat.CheckForSwitch(part1);
                    }
                }

                bool unstableSeats = false;
                // switch the states of seating if applicable
                // if any seat changes, the seating is not stable and is set as such
                foreach (List<Seat> seatRow in Seating)
                {
                    foreach (Seat seat in seatRow)
                    {
                        if (seat.SwitchState())
                            unstableSeats = true;
                    }
                }
                //when no seats have changed after a switch - the seating is stable and does not change anymore
                StableSeating = !unstableSeats;
            }
            
            //
            // Add neighbours to specific seats, but only if they exist
            private void AddNeighboursToSeat(int row, int col)
            {
                // Adds direct neigbours to seat
                for (int i = - 1; i <=  1; i++)
                {
                    if (row + i < 0 || row + i > maxRow)
                        continue;
                    for (int j = - 1; j <= + 1; j++)
                    {
                        if (col + j < 0 || col + j > maxCol || (i == 0 && j == 0))
                            continue;
                        Seating[row][col].DirectNeighbours.Add(Seating[row + i][col + j]);
                        // Finds closes visual neighbour based on row and seat
                        VisualNeighbourFinder(new int[] { row, col }, new int[] { i, j });
                    }
                }
            }

            //
            // Finds closest visual neighbour (ignores floors)
            private void VisualNeighbourFinder(int[] origPos, int[] relPos)
            {
                Seat origSeat = Seating[origPos[0]][origPos[1]];
                int currRow = origPos[0] + relPos[0];
                int currCol = origPos[1] + relPos[1];
                Seat tempSeat = Seating[currRow][currCol];
                
                // add temp to visual neighbours if the seat is not floor, then leave
                if (tempSeat.state != SeatState.Floor)
                {
                    origSeat.VisualNeighbours.Add(tempSeat);
                    return;
                }

                // Loops run until a nonfloor seat is found
                while (tempSeat.state == SeatState.Floor)
                {
                    currRow += relPos[0];
                    currCol += relPos[1];
                    // if the index goes out of bounds set the temp as null and break the loop
                    if (currRow < 0 || currRow > maxRow 
                        || currCol < 0 || currCol > maxCol)
                    {
                        tempSeat = null;
                        break;
                    }
                    tempSeat = Seating[currRow][currCol];
                }
                // if the temp exist add it to the visual neigbours list
                if (tempSeat != null)
                    origSeat.VisualNeighbours.Add(tempSeat);
            }
        }


        // Simple enum for seatstate
        internal enum SeatState
        {
            Floor,
            Occ,
            Free,
        }
        internal class Seat
        {
            // State of the seat => free, occupied or floor
            public SeatState state { get; set; }
            // List of Neighbours for this seat
            public List<Seat> DirectNeighbours { get; set; }

            public List<Seat> VisualNeighbours { get; set; }
            // The number of occupied seats among the neighbours
            private int occDirectNeighbours { get => GetOccNeighbours(DirectNeighbours); }
            private int occVisualNeighbours { get => GetOccNeighbours(VisualNeighbours); }
            // bool if this seat will change state next run
            bool ToBeSwitched { get; set; }

            public Seat(char seatString)
            {
                ToBeSwitched = false;
                DirectNeighbours = new List<Seat>();
                VisualNeighbours = new List<Seat>();
                switch (seatString)
                {
                    case '.':
                        state = SeatState.Floor;
                        break;
                    case 'L':
                        state = SeatState.Free;
                        break;
                    default:
                        state = SeatState.Occ;
                        break;
                }
            }
            
            //
            // Sets if the seats is to be switched
            public void CheckForSwitch(bool part1 = true)
            {
                int occNeighbours;
                int maxOccSeats;
                //switch rules based on supplied bool
                if (part1)
                {
                    occNeighbours = occDirectNeighbours;
                    maxOccSeats = 4;
                }                   
                else
                {
                    occNeighbours = occVisualNeighbours;
                    maxOccSeats = 5;
                }
                   
                switch (this.state)
                {
                    case SeatState.Free:
                        if (occNeighbours == 0)
                            ToBeSwitched = true;
                        break;
                    case SeatState.Occ:
                        if (occNeighbours >= maxOccSeats)
                            ToBeSwitched = true;
                        break;
                    default:
                        break;
                }
            }
            
            //
            // if the seat is to be switched, switch it and return true, else false
            public bool SwitchState()
            {
                if (ToBeSwitched)
                {
                    if (state == SeatState.Occ)
                        state = SeatState.Free;
                    else if (state == SeatState.Free)
                        state = SeatState.Occ;
                    ToBeSwitched = false;
                    return true;
                }
                return false;
            }

            //
            // Counts the occupied Neighbours
            private int GetOccNeighbours(List<Seat> neighbourList)
            {
                int counter = 0;
                foreach (Seat neighbur in neighbourList)
                {
                    if (neighbur.state == SeatState.Occ)
                        counter++;
                }
                return counter;
            }
        }
    }
}
