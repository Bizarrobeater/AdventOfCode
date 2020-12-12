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


            var test = new Dec12();
            //test.Timer("Solution1", test.Solution1);
            test.Timer("Solution2", test.Solution2);
        }

        static void WithTestDataPart1()
        {
            Dec12 test = new Dec12(TestData.Dec12Part1);
            Console.WriteLine(test.Solution1());
            //test.test();
        }

        static void WithTestDataPart2()
        {
            Dec12 test = new Dec12(TestData.Dec12Part1);
            Console.WriteLine(test.Solution2());
            //test.test();
        }



    }
}
