using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    public class Dec04 : AdventCodeBase<string, int>
    {
        List<Passport> passports = new List<Passport>();

        public Dec04() : base(ReadDataFile.FileToListDoubleNewline)
        {
            foreach (string rawPassport in dataList)
            { 
                passports.Add(new Passport(rawPassport));
            }

        }

        public override int Solution1()
        {
            int counter = 0;
            foreach (Passport passport in passports)
            {
                if (passport.ValidPassportSimple)
                {
                    counter++;
                }
            }
            return counter;
        }

        public override int Solution2()
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

        internal class Passport
        {
            string ecl;
            string hgt;
            string pid;
            //string cid;
            string hcl;

            int byr = -1;
            int iyr = -1;
            int eyr = -1;

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

            // height must be in a range, range depends on unit
            private bool HeightValidator()
            {
                Regex rx = new Regex(@"^(\d*) ?(cm|in)$");
                Match match = rx.Match(hgt);
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

            // haircolor must be a valid hexcode
            private bool HairColorValidator()
            {
                Regex rx = new Regex(@"^#[0-9a-f]{6}$");
                Match match = rx.Match(hcl);
                return match.Success;
            }

            // eye color must be on the list given, and only ones
            private bool EyeColorValidator()
            {
                List<string> eyeColors = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
                return eyeColors.Contains(ecl);
            }

            // pid must be exactly 9 numbers long 
            private bool PassportIDValidator()
            {
                Regex rx = new Regex(@"^[0-9]{9}$");
                Match match = rx.Match(pid);
                return match.Success;
            }

            // Complex validator based on varying rules per datapoint
            private bool ValidatePassportComplex()
            {
                return
                    ValidPassportSimple &&
                    (byr >= 1920 && byr <= 2002) &&
                    (iyr >= 2010 && iyr <= 2020) &&
                    (eyr >= 2020 && eyr <= 2030) &&
                    HeightValidator() &&
                    HairColorValidator() &&
                    EyeColorValidator() &&
                    PassportIDValidator();
            }

            // returns bool on wether or not a passport is valid in a simple way
            // meaning all values except ecl should exist - ecl is optional
            private bool ValidatePassportSimple()
            {
                string[] strings = new string[] { ecl, hgt, pid, hcl };
                int[] ints = new int[] { byr, iyr, eyr };

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

            // applies the different passport data as given
            private void DataSorter(string key, string value)
            {
                switch (key)
                {
                    case "ecl":
                        ecl = value;
                        break;
                    case "hgt":
                        hgt = value;
                        break;
                    case "pid":
                        pid = value;
                        break;
                    //case "cid":
                    //    cid = value;
                    //    break;
                    case "hcl":
                        hcl = value;
                        break;
                    case "byr":
                        byr = Int32.Parse(value);
                        break;
                    case "iyr":
                        iyr = Int32.Parse(value);
                        break;
                    case "eyr":
                        eyr = Int32.Parse(value);
                        break;
                    default:
                        break;       
                }
            }

            // split a given passport string using the possible delimers
            // returns list of strings
            private List<string> dataSplitter(string passportData)
            {
                char[] delimeters = new char[] { ' ', '\n' };
                string[] parts = passportData.Split(delimeters, StringSplitOptions.RemoveEmptyEntries);

                return parts.ToList();
            }
        }
    }
}
