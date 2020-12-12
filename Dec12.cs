using System;
using System.Collections.Generic;

namespace AdventOfCode
{
    class Dec12 : AdventCodeBase<string, int>
    {
        Ship ship = new Ship();
        public Dec12() : base(ReadDataFile.FileToListSimple)
        {

        }

        public Dec12(List<string> testData) : base(testData)
        {
        }

        // Ship follows instructions given - no funny business
        // Correct answer: 858
        public override int Solution1()
        {
            foreach(string instruct in dataList)
            {
                ship.MoveSimple(instruct);
            }

            return Math.Abs(ship.position[0]) + Math.Abs(ship.position[1]);

        }

        // Instructions changed to move waypoint relative to ship
        // Only ship moves with "F", and then it moves to the way x number of time
        // Correct answer: 39.140
        public override int Solution2()
        {
            foreach (string instruct in dataList)
            {
                ship.MoveWaypoint(instruct);
            }

            return Math.Abs(ship.position[0]) + Math.Abs(ship.position[1]);
        }
    }

    // Class for waypoints includes methods for moving it
    internal class Waypoint
    {
        public int[] position = { 10, -1 };

        public Waypoint()
        {
        }

        // Method for moving the waypoint based on action and value
        public void Move(string action, int value)
        {
            switch (action)
            {
                case "N":
                    position[1] -= value;
                    break;
                case "S":
                    position[1] += value;
                    break;
                case "E":
                    position[0] += value;
                    break;
                case "W":
                    position[0] -= value;
                    break;
                default:
                    RotateWaypoint(action, value);
                    break;
            }
        }

        // Rotating the waypoint relative to ship
        private void RotateWaypoint(string action, int value)
        {
            // if the waypoint needs to rotate 180 degrees
            // its positional datas sign will be reversed, regardless of direction
            if (value == 180)
            {
                position = new int[] { position[0] * -1, position[1] * -1 };
                return;
            }
            // if the value is 270 it is the same as rotating 90 degrees the other direction
            else if (value == 270)
            {
                if (action == "L")
                    action = "R";
                else
                    action = "L";
            }

            // left is how to rotate 90 degrees clockwise or counterclockwise
            if (action == "R")
                // switch positions and x reverses its sign
                position = new int[] { position[1] * -1, position[0] };
            else
                //switch position and y reverses its sign
                position = new int[] { position[1], position[0] * -1 };
        }
    }

    internal class Ship
    {
        int facingDeg { get; set; }

        public int[] position = { 0, 0 };

        Waypoint waypoint = new Waypoint();

        public Ship()
        {
            facingDeg = 0;
        }

        public void MoveWaypoint(string instruction)
        {
            string action = instruction.Substring(0, 1);
            int value = Int32.Parse(instruction[1..]);

            // if the action is F move the ships position towards the waypoint
            // a number of times equal to the value
            if (action == "F")
            {
                position[0] += waypoint.position[0] * value;
                position[1] += waypoint.position[1] * value;
            }
            else
            {
                waypoint.Move(action, value);
            }
        }
        
        //
        // Parses instruciton into the action and value
        public void MoveSimple(string instruction)
        {
            string action = instruction.Substring(0, 1);
            int value = Int32.Parse(instruction[1..]);

            ToAction(action, value);

        }

        // performs action
        private void ToAction(string action, int value)
        {
            switch (action)
            {
                case "N":
                    position[1] -= value;
                    break;
                case "S":
                    position[1] += value;
                    break;
                case "E":
                    position[0] += value;
                    break;
                case "W":
                    position[0] -= value;
                    break;
                case "F":
                    Forward(value);
                    break;
                case "R":
                    facingDeg += value;
                    break;
                case "L":
                    facingDeg -= value;
                    break;
            }

        }

        //
        // Forwards first translate current facing into a movable direction
        // then moves it into an action
        private void Forward(int value)
        {
            int absFacing = facingDeg - (360 * (int)(facingDeg / 360));
            string action = null;

            if (absFacing < 0)
                absFacing += 360;


            switch (absFacing)
            {
                case 0:
                    action = "E";
                    break;
                case 90:
                    action = "S";
                    break;
                case 180:
                    action = "W";
                    break;
                case 270:
                    action = "N";
                    break;
            }
            ToAction(action, value);
        }
    }
}
