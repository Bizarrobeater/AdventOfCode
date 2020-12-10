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

            var test = new Dec09();
            //test.PrintTest();
            test.Timer("Solution1", test.Solution1);
            test.Timer("Solution2", test.Solution2);


            //Console.WriteLine(test.Solution1());
            //Console.WriteLine(test.Solution2());

            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        //static void WithTestDataPart1()
        //{
        //    tbremoved test = new tbremoved(TestData.Dec10);
        //    Console.WriteLine(test.SolutionPart1());
        //}

        //static void WithTestDataPart2()
        //{
        //    tbremoved test = new tbremoved(TestData.Dec10);
        //    Console.WriteLine(test.SolutionPart2Long());
        //}



    }
}
