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
   
            //WithTestData();

            var test = new Dec9();
            //test.testQueue();

            //Console.WriteLine(test.SolutionPart1Long());
            Console.WriteLine(test.SolutionPart2Long());



            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        static void WithTestData()
        {
            Dec9 test = new Dec9(TestData.Dec9Part1);
            Console.WriteLine(test.SolutionPart2Long());
        }



    }
}
