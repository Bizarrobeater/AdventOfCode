using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace AdventOfCode
{
    // abstract class for the different day
    // T = type of list the data will converted to
    // U = type of output
    public abstract class AdventCodeBase<T, U>
    {
        string _dataSourceFile;
        internal List<T> dataList;


        // For main data
        public AdventCodeBase(Func<string, List<T>> DataReader)
        {
            _dataSourceFile = $"AdventCode{this.GetType().Name}.txt";
            dataList = DataReader(_dataSourceFile);
        }

        // For testdata
        public AdventCodeBase(List<T> testData)
        {
            dataList = testData;
        }

        public abstract U Solution1();

        public abstract U Solution2();

        public void Timer(string methodName, Func<U> method)
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            U answer = method();
            watch.Stop();
            Console.Write($"{methodName} result = {answer}\n");
            Console.Write($"Milliseconds taken: {watch.ElapsedMilliseconds}\n\n");
        }
    }
}
