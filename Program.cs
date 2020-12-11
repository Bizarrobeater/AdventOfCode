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
            //var stopwatch = new Stopwatch();
            //stopwatch.Start();

            //WithTestDataPart1();
            //WithTestDataPart2();

            //var test = new Dec11();
            //test.PrintTest();
            //test.Timer("Solution1", test.Solution1);
            //test.Timer("Solution2", test.Solution2);

            Visualise();
            

            //Console.WriteLine(test.Solution1());
            //Console.WriteLine(test.Solution2());

            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        static void Visualise()
        {
            Dec11 test = new Dec11(TestData.Dec11Part1);
            test.Visualise();
        }

        static void WithTestDataPart1()
        {
            Dec11 test = new Dec11(TestData.Dec11Part1);
            Console.WriteLine(test.Solution1());
            //test.test();
        }

        static void WithTestDataPart2()
        {
            Dec11 test = new Dec11(TestData.Dec11Part1);
            Console.WriteLine(test.Solution2());
            //test.test();
        }



    }
}
