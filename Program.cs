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
            //test.test();

            //Console.WriteLine(test.SolutionPart1());
            Console.WriteLine(test.SolutionPart2());



            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }

        static void WithTestDataDec7()
        {
            Dec7 dec7 = new Dec7(TestData.Dec7Part1);
            Console.WriteLine(dec7.SolutionPart1());
        }

        static void WithTestDataDec7Part2()
        {
            List<string> testList = new List<string>
            {
                "shiny gold bags contain 2 dark red bags.",
                "dark red bags contain 2 dark orange bags.",
                "dark orange bags contain 2 dark yellow bags.",
                "dark yellow bags contain 2 dark green bags.",
                "dark green bags contain 2 dark blue bags.",
                "dark blue bags contain 2 dark violet bags.",
                "dark violet bags contain no other bags.",
            };
            Dec7 dec7 = new Dec7(testList);
            Console.WriteLine(dec7.SolutionPart2());
        }
    }
}
