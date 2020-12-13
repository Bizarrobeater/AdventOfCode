using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //WithTestDataPart1();
            //WithTestDataPart2();

            //TestManyCases();


            var test = new Dec13();
            test.Timer("Solution1", test.Solution1);
            test.Timer("Solution2", test.Solution2);
        }

        static void WithTestDataPart1()
        {
            var test = new Dec13(TestData.Dec13Part1);
            Console.WriteLine(test.Solution1());
            //test.test();
        }

        static void WithTestDataPart2()
        {
            var test = new Dec12(TestData.Dec12Part1);
            Console.WriteLine(test.Solution2());
            //test.test();
        }

        static void TestManyCases()
        {
            var test = new Dec13(TestData.Dec13Part2);
            test.TestAllCases(test.Solution2);

        }



    }
}
