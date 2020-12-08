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

            
            WithTestData();

            var test = new Dec8();
            //test.Timer();

            //Console.WriteLine(test.SolutionPart1());
            Console.WriteLine(test.SolutionPart2());



            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        static void WithTestData()
        {
            Dec8 test = new Dec8(TestData.Dec8Part1);
            Console.WriteLine(test.SolutionPart2());
        }



    }
}
