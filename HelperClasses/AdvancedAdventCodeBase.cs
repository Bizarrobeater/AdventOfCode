using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventOfCode
{
    public abstract class AdvancedAdventCodeBase<T, U> : AdventCodeBase<T, U>
    {
        internal Dictionary<T, U> testDict = null;
        protected AdvancedAdventCodeBase(Func<string, List<T>> DataReader) : base(DataReader)
        {
        }

        protected AdvancedAdventCodeBase(List<T> testData) : base(testData)
        {
        }

        // for expanded testData
        public AdvancedAdventCodeBase(Dictionary<T, U> testDict) : base()
        {
            this.testDict = testDict;
        }

        // For converting a test dictionary to the datalist
        // will be case by case basis
        internal abstract void ConvertTestDataToUseful(T testDataKey);

        // Goes trough all the testcases with the given solution function
        public void TestAllCases(Func<U> solutionMethod)
        {
            Stopwatch watch = new Stopwatch();
            if (testDict == null)
            {
                Console.WriteLine("No test cases found for chosen solution");
                return;
            }

            foreach (KeyValuePair<T, U> kvp in testDict)
            {
                ConvertTestDataToUseful(kvp.Key);
                Console.WriteLine($"Testing {kvp.Key.GetType().Name}: {kvp.Key}");
                Console.WriteLine($"Expected result: {kvp.Value}");
                watch.Start();
                U result = solutionMethod();
                watch.Stop();
                Console.WriteLine($"Calculated answer: {result}");
                Console.WriteLine($"Milliseconds taken: {watch.ElapsedMilliseconds}");
                Console.WriteLine(Environment.NewLine);
                watch.Reset();
            }
        }
    }
}
