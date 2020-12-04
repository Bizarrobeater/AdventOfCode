using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Text;

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
            return passportList.ValidPassportsSimple();
        }

        public int SolutionPart2()
        {
            return passportList.ValidPassportsComplex();
        }

        public void testc()
        {
            passportList.ValidPassportsComplex();
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

            public int ValidPassportsSimple()
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

            public int ValidPassportsComplex()
            {
                int counter = 0;
                foreach (Passport passport in passports)
                {
                    if (passport.ValidPassportComplex)
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
            public bool ValidPassportComplex { get => ValidatePassportComplex();  }
            
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
                Regex rx = new Regex(@"^(\d*) ?(cm|in)$");
                Match match = rx.Match(height);
                GroupCollection groups = match.Groups;

                string type = groups[2].ToString();

                int number;
                if (Int32.TryParse(groups[1].ToString(), out number))
                {
                    return ((type == "cm" && number >= 150 && number <= 193) ||
                        (type == "in" && number >= 59 && number <= 76));
                }
                else
                {
                    return false;
                }
            }


            private bool HairColorValidator()
            {
                Regex rx = new Regex(@"^#[0-9a-f]{6}$");
                Match match = rx.Match(Hcl);
                return match.Success;
            }

            private bool EyeColorValidator()
            {
                List<string> eyeColors = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                return eyeColors.Contains(Ecl);
            }

            private bool PassportIDValidator()
            {
                Regex rx = new Regex(@"^[0-9]{9}$");
                Match match = rx.Match(Pid);
                return match.Success;
            }

            private bool ValidatePassportComplex()
            {
                return
                    ValidPassportSimple &&
                    (Byr >= 1920 && Byr <= 2002) &&
                    (Iyr >= 2010 && Iyr <= 2020) &&
                    (Eyr >= 2020 && Eyr <= 2030) &&
                    HeightValidator() &&
                    HairColorValidator() &&
                    EyeColorValidator() &&
                    PassportIDValidator();
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

            //public void PrintPassport()
            //{
            //    StringBuilder topBuilder = new StringBuilder();
            //    StringBuilder botBuilder = new StringBuilder();

            //    string[] strings = new string[] { Ecl, Height, Pid, Hcl };
            //    string[] nameStr = new string[] { "ecl", "hgt", "pid", "hcl" };
            //    int[] ints = new int[] { Byr, Iyr, Eyr };
            //    string[] nameInt = new string[] { "byr", "iyr", "eyr" };

            //    for (int i = 0; i < strings.Length; i++)
            //    { 
            //        topBuilder.AppendFormat(@" {0}|", nameStr[i].PadRight(10));
            //        botBuilder.AppendFormat(@" {0}|", strings[i].PadRight(10));
            //    }
            //    for (int i = 0; i < ints.Length; i++)
            //    {
            //        topBuilder.AppendFormat(@" {0}|", nameInt[i].PadRight(10));
            //        botBuilder.AppendFormat(@" {0}|", ints[i].ToString().PadRight(10));
            //    }
            //    topBuilder.AppendFormat(@"{1}{0}", botBuilder, Environment.NewLine);
            //    Console.WriteLine(topBuilder.ToString());
            }
        }
    }

}
