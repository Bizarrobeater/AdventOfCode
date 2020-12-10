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
            
            // answer: 19208
            //WithTestDataPart2();

            var test = new Dec10();
            //test.PrintTest();
            test.Timer();


            //Console.WriteLine(test.SolutionPart1());
            Console.WriteLine(test.SolutionPart2Long());

            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        static void WithTestDataPart1()
        {
            Dec10 test = new Dec10(TestData.Dec10Part1);
            Console.WriteLine(test.SolutionPart1());
        }

        static void WithTestDataPart2()
        {
            Dec10 test = new Dec10(TestData.Dec10Part1);
            Console.WriteLine(test.SolutionPart2Long());
        }



    }
}
