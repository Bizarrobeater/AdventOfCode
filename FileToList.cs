using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    class FileToList
    {
        List<string> dataStrList;
        public List<string> DataStrList { get => dataStrList;  }
        public FileToList(string fileName)
        {
            dataStrList = File.ReadAllLines(@"DataSources\" + fileName).ToList();
            //List<int> dataList = dataStrList.Select(s => Convert.ToInt32(s)).ToList();
        }

        
    }
}
