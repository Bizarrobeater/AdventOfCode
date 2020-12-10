using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    static class ReadDataFile
    {   
        static public List<string> FileToListSimple(string fileName)
        {
            List<string> dataStrList = File.ReadAllLines(@"DataSources\" + fileName).ToList();
            return dataStrList;
        }

        static public List<string> FileToListDoubleNewline(string fileName)
        {
            string allfile = File.ReadAllText(@"DataSources\" + fileName);
            List<string> dataStrList = allfile.Split("\n\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            return dataStrList;
        } 

        static public List<int> FileToListInt(string fileName)
        {
            List<string> pulledList = ReadDataFile.FileToListSimple(fileName);
            return pulledList.Select(int.Parse).ToList();
        }
    }
}
