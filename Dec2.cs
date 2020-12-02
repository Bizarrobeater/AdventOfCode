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

        private bool validatePassword(string passwordInfo)
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

        public string SolutionPart1()
        {
            int validCount = 0;
            foreach (string passwordStr in DataList)
            {
                if (validatePassword(passwordStr))
                {
                    validCount++;
                }
            }
            return validCount.ToString();
        }
    }
}
