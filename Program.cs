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

            Console.WriteLine("Correct answer: 5");
            Console.Write("Original Answer: ");
            WithTestData();
            Console.WriteLine();
            Console.Write("Test Answer: ");
            testwithTest();

            //var Dec8Orig = new Dec8();
            //var Dec8Test = new Dec8Test();

            //Console.WriteLine("Correct Answer: 1610");
            //Console.WriteLine($"Original Answer: {Dec8Orig.SolutionPart1()}");
            //Console.WriteLine($"Test Answer: {Dec8Test.SolutionPart1()}");

            //Console.WriteLine(test.SolutionPart1());
            //Console.WriteLine(test.SolutionPart2());



            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        static void WithTestData()
        {
            Dec8 test = new Dec8(TestData.Dec8Part1);
            Console.Write(test.SolutionPart1());
        }

        static void testwithTest()
        {
            Dec8Test test = new Dec8Test(TestData.Dec8Part1);
            Console.WriteLine(test.SolutionPart1());
        }



    }
}
