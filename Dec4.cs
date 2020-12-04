using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Dec4 : ISolution
    {
        List<string> dataList;
        PassportList passportList;
        public List<string> DataList { get => dataList; }

        public Dec4()
        {
            dataList = ReadDataFile.FileToListDoubleNewline("AdventCode4Dec.txt");
            passportList = new PassportList(DataList);
        }

        public int SolutionPart1()
        {
            return passportList.ValidPassports(); ;
        }

        public int SolutionPart2()
        {
            return -1;
        }

        private class PassportList
        {
            List<Passport> passports = new List<Passport>();

            public List<Passport> Passports { get => passports; }

            public PassportList(List<string> rawDataList)
            {
                foreach (string rawPassport in rawDataList)
                {
                    passports.Add(new Passport(rawPassport));
                }
            }

            public int ValidPassports()
            {
                int counter = 0;
                foreach(Passport passport in passports)
                {
                    if (passport.ValidPassportSimple)
                    {
                        counter++;
                    }
                }
                return counter;
            }
        }

        private class Passport
        {
            string ecl;
            string height;
            string pid;
            string cid;
            string hcl;

            int byr = -1;
            int iyr = -1;
            int eyr = -1;

            public string Ecl { get => ecl; set => ecl = value; }
            public string Height { get => height; set => height = value; }
            public string Pid { get => pid; set => pid = value; }
            public string Cid { get => cid; set => cid = value; }
            public string Hcl { get => hcl; set => hcl = value; }
            public int Byr { get => byr; set => byr = value; }
            public int Iyr { get => iyr; set => iyr = value; }
            public int Eyr { get => eyr; set => eyr = value; }
            public bool ValidPassportSimple { get => ValidatePassportSimple(); }            
            
            public Passport(string passportData)
            {
                List<string> splitData = dataSplitter(passportData);
                foreach (string data in splitData)
                {
                    string[] KeyValue = data.Split(":");
                    DataSorter(KeyValue[0], KeyValue[1]);
                }
            }

            private bool HeightValidator()
            {
                return true;
            }

            private bool ValidatePassportComplex()
            {

            }

            private bool ValidatePassportSimple()
            {
                string[] strings = new string[] { Ecl, Height, Pid, Hcl };
                int[] ints = new int[] { Byr, Iyr, Eyr };

                foreach (string str in strings)
                {
                    if (string.IsNullOrEmpty(str))
                    {
                        return false;
                    }
                }
                foreach (int i in ints)
                {
                    if (i == -1)
                    {
                        return false;
                    }
                }
                return true;
            }

            private void DataSorter(string key, string value)
            {
                switch (key)
                {
                    case "ecl":
                        Ecl = value;
                        break;
                    case "hgt":
                        Height = value;
                        break;
                    case "pid":
                        Pid = value;
                        break;
                    case "cid":
                        Cid = value;
                        break;
                    case "hcl":
                        Hcl = value;
                        break;
                    case "byr":
                        Byr = Int32.Parse(value);
                        break;
                    case "iyr":
                        Iyr = Int32.Parse(value);
                        break;
                    case "eyr":
                        Eyr = Int32.Parse(value);
                        break;
                    default:
                        break;       
                }

            }

            private List<string> dataSplitter(string passportData)
            {
                char[] delimeters = new char[] { ' ', '\n' };
                string[] parts = passportData.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);

                return parts.ToList();
            }



        }
    }

}
