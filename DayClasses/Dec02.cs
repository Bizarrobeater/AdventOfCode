using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode
{
    class Dec02 : AdventCodeBase<string, int>
    {
        public Dec02() : base(ReadDataFile.FileToListSimple)
        {
        }

        //
        // returns bool based on if a letter appear a number of times in a range
        private bool validatePasswordPart1(string passwordInfo)
        {
            Regex rx = new Regex(@"(\d+)-(\d+) ([a-z]{1}): (\w+)");
            Match match = rx.Match(passwordInfo);

            GroupCollection groups = match.Groups;
            int minAmount = Int32.Parse(groups[1].ToString());
            int maxAmount = Int32.Parse(groups[2].ToString());
            string letter = groups[3].ToString();
            string password = groups[4].ToString();

            int letterCount = password.Length - password.Replace(letter, string.Empty).Length;

            return (letterCount >= minAmount && letterCount <= maxAmount);
        }
        
        //
        // return bool based on a given character appears on one position but not the other
        private bool validatePasswordPart2(string passwordInfo)
        {
            Regex rx = new Regex(@"(\d+)-(\d+) ([a-z]{1}): (\w+)");
            Match match = rx.Match(passwordInfo);

            GroupCollection groups = match.Groups;
            int firstInd = Int32.Parse(groups[1].ToString());
            int lastInd = Int32.Parse(groups[2].ToString());
            char letter = groups[3].ToString().ToCharArray()[0];
            string password = groups[4].ToString();

            // return bool based on a given character appears on one position but not the other
            return ((password[firstInd - 1] == letter && password[lastInd - 1] != letter) ||
                (password[firstInd - 1] != letter && password[lastInd - 1] == letter));
        }

        // Find valid passports.
        // a given letter must appear a number of times provided
        // Correct answer: 398
        public override int Solution1()
        {
            int validCount = 0;
            foreach (string passwordStr in dataList)
            {
                if (validatePasswordPart1(passwordStr))
                {
                    validCount++;
                }
            }
            return validCount;
        }

        // Find valid passports
        // must appear on exactly one of the provided positions
        // Correct answer: 562
        public override int Solution2()
        {
            int validCount = 0;
            foreach (string passwordStr in dataList)
            {
                if (validatePasswordPart2(passwordStr))
                {
                    validCount++;
                }
            }
            return validCount;
        }
    }
}
