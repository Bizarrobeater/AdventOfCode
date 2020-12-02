using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace AdventOfCode
{
    class Dec2 : ISolution
    {
        List<string> dataList;

        public List<string> DataList { get => dataList; }

        public Dec2()
        {
            dataList = new FileToList("AdventCode2Dec1Input.txt").DataStrList;
        }

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

        private bool validatePasswordPart2(string passwordInfo)
        {
            Regex rx = new Regex(@"(\d+)-(\d+) ([a-z]{1}): (\w+)");
            Match match = rx.Match(passwordInfo);

            GroupCollection groups = match.Groups;
            int firstInd = Int32.Parse(groups[1].ToString());
            int lastInd = Int32.Parse(groups[2].ToString());
            char letter = groups[3].ToString().ToCharArray()[0];
            string password = groups[4].ToString();

            return ((password[firstInd - 1] == letter && password[lastInd - 1] != letter) ||
                (password[firstInd - 1] != letter && password[lastInd - 1] == letter));
        }

        public string SolutionPart1()
        {
            int validCount = 0;
            foreach (string passwordStr in DataList)
            {
                if (validatePasswordPart1(passwordStr))
                {
                    validCount++;
                }
            }
            return validCount.ToString();
        }

        public string SolutionPart2()
        {
            int validCount = 0;
            foreach (string passwordStr in DataList)
            {
                if (validatePasswordPart2(passwordStr))
                {
                    validCount++;
                }
            }
            return validCount.ToString();

        }
    }
}
