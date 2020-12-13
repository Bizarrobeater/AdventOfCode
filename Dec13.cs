using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Dec13 : AdvancedAdventCodeBase<string, long>
    {
        public Dec13() : base(ReadDataFile.FileToListSimple)
        {
        }

        public Dec13(List<string> testData) : base(testData)
        {
        }

        public Dec13(Dictionary<string, long> testDict) : base(testDict)
        {
            this.testDict = testDict;
        }

        internal override void ConvertTestDataToUseful(string testDataKey)
        {
            dataList = new List<string>();
            dataList.Add("Thrown");
            dataList.Add(testDataKey);
        }
        
        //
        // finds the earliest time after a specified time that a bus can be taken
        // result is the bus number times the number of minutes waited
        // Correct answer: 370
        public override long Solution1()
        {
            // Finds earliest bus and relevant busses based on the provided data
            long EarliestTime = Int64.Parse(dataList[0]); ;
            List<int> BusSimple = new List<int>();
            List<string> temp = dataList[1].Split(',').ToList();
            int busNumb = 0;
            foreach (string bus in temp)
            {
                if (Int32.TryParse(bus, out busNumb))
                    BusSimple.Add(busNumb);
                busNumb = 0;
            }
            
            int fastestBus = 0;
            int lowestCount = -1;
            int counter;            
            foreach (int busNumber in BusSimple)
            {
                counter = 0;
                // loop runs while
                while ((EarliestTime + counter) % busNumber != 0)  // the busnumber will arrive the the current time
                {
                    counter++;
                }
                // if the counter is lower than the current lowest (that isn't -1)
                if (lowestCount == -1 || counter < lowestCount)
                {
                    // change fastest time and busnumber
                    lowestCount = counter;
                    fastestBus = busNumber;
                }
            }
            return lowestCount * fastestBus;
        }

        //
        // Finds the earliest timestamp t where busses with 1 minutes between each on the list
        // x's means no constraints, numbers mean that the specific bus must go there
        // Correct Answer: 894.954.360.381.385
        public override long Solution2()
        {
            // Splitting the relevent row to a list
            List<string> tempList = dataList[1].Split(',').ToList();

            // minimum timestamp must be equal to first bus number
            long currTimeStamp = Int32.Parse(tempList[0]);
            // this multiplier is the product of all busnumbers that has been passed
            long productMultiplier = currTimeStamp;

            // variable used in the loop
            long currMultiplier;
            int currBus;
            long tempTimeStamp;
            for (int i = 1; i < tempList.Count; i++)
            {
                // current bus, if not a number continue to the next bus
                if (!Int32.TryParse(tempList[i], out currBus))
                    continue;                

                // multiplier for current timestamp
                currMultiplier = 0;
                tempTimeStamp = currTimeStamp + i;

                // adding multipliers to the timestamp (plus the current bus' index) until the modulus is 0
                while (tempTimeStamp % currBus != 0)
                {
                    currMultiplier++;
                    tempTimeStamp = currTimeStamp + productMultiplier * currMultiplier + i;
                }

                // this multiplier is the product of the times of all preceding busses
                productMultiplier *= currBus;
                // current timestamp is updated
                currTimeStamp = tempTimeStamp - i;
            }
            return currTimeStamp;
        }
    }
}
