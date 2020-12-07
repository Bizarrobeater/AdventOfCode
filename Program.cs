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

            //guess low 4758
            //guess high 6315

            //WithTestDataDec7Part2();

            var test = new Dec7();
            test.Timer();

            //Console.WriteLine(test.SolutionPart1());
            //Console.WriteLine(test.SolutionPart2());



            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        static void WithTestDataDec7()
        {
            Dec7 dec7 = new Dec7(TestData.Dec7Part1);
            Console.WriteLine(dec7.SolutionPart1());
        }

    }
}
