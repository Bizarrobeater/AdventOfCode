using System;
using System.Diagnostics;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            //var stopwatch = new Stopwatch();
            //stopwatch.Start();
            
            var test = new Dec3();
            Console.WriteLine(test.SolutionPart2Long());
            
            //stopwatch.Stop();
            //Console.WriteLine(stopwatch.ElapsedMilliseconds);
        }
    }
}
