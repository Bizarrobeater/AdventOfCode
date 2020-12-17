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

        public override long Solution1()
        {
            AllCubes pocketDimension = new AllCubes(dataList);
            while (pocketDimension.Cycle <= 6)
            {
                pocketDimension.NewCycle();
            }
            return pocketDimension.ActiveCubes;
        }

        public override long Solution2()
        {
            AllHyperCubes pocketDimension = new AllHyperCubes(dataList);
            while (pocketDimension.Cycle <= 6)
            {
                pocketDimension.NewCycle();
            }
            return pocketDimension.ActiveCubes;
        }
    }
}
