using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace AdventOfCode
{
    class Dec18 : AdvancedAdventCodeBase<string, long>
    {
        public Dec18() : base(ReadDataFile.FileToListSimple)
        {
        }

        public Dec18(List<string> testData) : base(testData)
        {
        }

        public Dec18(Dictionary<string, long> testDict) : base(testDict)
        {
        }

        // Get the sum of all calculations in the datalist
        // Special rules for calculations apply
        // brackets first, then do calculations left to right
        // Correct answer: 21.993.583.522.852, time 35ms
        public override long Solution1()
        {
            long result = 0;

            foreach (string data in dataList)
            {
                result += CalculateFullExpression(data, CalculatePartExpressionNormal);
            }

            return result;
        }

        // Sum of all calculations with different precedence rules applied
        // addition before multiplication
        // Correct answer: 122.438.593.522.757, time ~60 ms
        public override long Solution2()
        {
            long result = 0;

            foreach (string data in dataList)
            {
                result += CalculateFullExpression(data, CalculatePartExpressionAdvanced);
            }

            return result;
        }

        private long CalculateFullExpression(string sumString, Func<string, long> calculatorMethod)
        {
            string summedString = sumString;
            Regex bracketRx = new Regex(@"\([\d\+\* ]+\)");

            Match bracketMatch = bracketRx.Match(summedString);

            while (bracketMatch.Success)
            {
                int matchIndex = bracketMatch.Index;
                string foundGroup = bracketMatch.Groups[0].Value;
                string groupNoBrackets = RemoveBrackets(foundGroup);
                string newSum = calculatorMethod(groupNoBrackets).ToString();
                summedString = summedString.Remove(matchIndex, foundGroup.Length).Insert(matchIndex, newSum);
                
                bracketMatch = bracketRx.Match(summedString);
            }

            return calculatorMethod(summedString);
        }

        private long CalculatePartExpressionNormal(string expression)
        {
            expression = expression.Replace(" ", "");

            Regex calcRx = new Regex(@"((\d+)([\+\*])?)");
            MatchCollection calcMatches = calcRx.Matches(expression);


            long sum = 0;
            char currOp = '+';
            long tempNumber;

            foreach (Match match in calcMatches)
            {
                GroupCollection groups = match.Groups;
                tempNumber = long.Parse(groups[2].Value);

                if (currOp == '+')
                    sum += tempNumber;
                else
                    sum *= tempNumber;
                if (groups[3].Success)
                    currOp = groups[3].ToString()[0];
            }
            return sum;
        }

        private long CalculatePartExpressionAdvanced(string expression)
        {
            expression = expression.Replace(" ", "");

            Regex addRx = new Regex(@"(\d+\+\d+)");
            Match addMatch = addRx.Match(expression);

            while (addMatch.Success)
            {
                int indexFound = addMatch.Index;
                int matchLength = addMatch.Groups[0].Length;
                string newSum = CalculatePartExpressionNormal(addMatch.Groups[0].Value).ToString();
                expression = expression.Remove(indexFound, matchLength).Insert(indexFound, newSum);
                addMatch = addRx.Match(expression);
            }
            return CalculatePartExpressionNormal(expression);
        }

        

        private string RemoveBrackets(string expression)
        {
            char[] bracket = new char[] { '(', ')' };
            string[] temp = expression.Split(bracket, StringSplitOptions.RemoveEmptyEntries);
            return temp[0];

        }

        internal override void ConvertTestDataToUseful(string testDataKey)
        {
            dataList = new List<string>
            {
                testDataKey,
            };
        }
    }
}
