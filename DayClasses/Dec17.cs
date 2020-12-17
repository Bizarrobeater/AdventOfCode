using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    // Conways game of life in 3 dimensions
    public class Dec17 : AdventCodeBase<string, long>
    {
        public Dec17() : base(ReadDataFile.FileToListSimple)
        {
        }

        public Dec17(List<string> testData) : base(testData)
        {
        }

        // Correct answer: 211 - about 440ms
        public override long Solution1()
        {
            AllCubes pocketDimension = new AllCubes(dataList);
            while (pocketDimension.Cycle <= 6)
            {
                pocketDimension.NewCycle();
            }
            return pocketDimension.ActiveCubes;
        }

        // Correct Answer: 1952 - about 1 min 30 seconds
        public override long Solution2()
        {
            AllHyperCubes pocketDimension = new AllHyperCubes(dataList);
            while (pocketDimension.Cycle <= 6)
            {
                pocketDimension.NewCycle();
            }
            return pocketDimension.ActiveCubes;
        }

        public long testSolution1()
        {
            HelperClassses.ConwayCubeManager pocketDimension = new HelperClassses.ConwayCubeManager(dataList);
            while (pocketDimension.Cycle <= 6)
            {
                pocketDimension.NewCycle();
            }
            return pocketDimension.ActiveCubes;
        }
    }
}
