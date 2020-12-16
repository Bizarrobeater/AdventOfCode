using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode
{
    static class ReadDataFile
    {   
        // splits datafile into list of string by every line
        static public List<string> FileToListSimple(string fileName)
        {
            List<string> dataStrList = File.ReadAllLines(@"DataSources\" + fileName).ToList();
            return dataStrList;
        }

        // splits datafile into a list of strings everytime there is a double newline (\r\n)
        static public List<string> FileToListDoubleNewline(string fileName)
        {
            string allfile = File.ReadAllText(@"DataSources\" + fileName);
            List<string> dataStrList = allfile.Split(Environment.NewLine + Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();
            return dataStrList;
        }

        // Splits datafile into a list of string everytime there's a double newline (\n\n)
        static public List<string> FileToListDoubleNewlineDiff(string fileName)
        {
            string allfile = File.ReadAllText(@"DataSources\" + fileName);
            List<string> dataStrList = allfile.Split("\n\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            return dataStrList;
        }

        // converts a simple list of string to a list of ints
        static public List<int> FileToListInt(string fileName)
        {
            List<string> pulledList = ReadDataFile.FileToListSimple(fileName);
            return pulledList.Select(int.Parse).ToList();
        }

        // converts a simple list of string to a list of long
        static public List<long> FileToListLong(string fileName)
        {
            List<string> pulledList = ReadDataFile.FileToListSimple(fileName);
            return pulledList.Select(long.Parse).ToList();
        }
    }
}
