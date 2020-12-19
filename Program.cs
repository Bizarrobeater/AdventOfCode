using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //WithTestDataPart1();
            //WithTestDataPart2();

            //TestManyCases();
            

            var test = new Dec19();
            test.Timer("Solution1", test.Solution1);
            //test.Timer("Solution2", test.Solution2);
        }

        static void WithTestDataPart1()
        {
            var test = new Dec19(TestData.Dec19Part1);
            Console.WriteLine(test.Solution1());
            //test.test();
        }

        static void WithTestDataPart2()
        {
            var test = new Dec17(TestData.Dec17Part1);
            Console.WriteLine(test.Solution2());
            //test.test();
        }

        static void TestManyCases()
        {
            var test = new Dec18(TestData.Dec18Part2);
            test.TestAllCases(test.Solution2);
        }
    }
}
