using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode
{
    class Dec19 : AdventCodeBase<string, long>
    {
        public Dec19() : base(ReadDataFile.FileToListDoubleNewlineDiff)
        {
        }

        public Dec19(List<string> testData) : base(testData)
        {
        }

        // Correct answer: 109
        public override long Solution1()
        {
            Rules rules = new Rules(dataList[0]);

            int validCount = 0;
            List<string> messages = dataList[1].Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (string message in messages)
            {
                if (rules.TestValidMessage(message))
                    validCount++;
            }
            return validCount;

        }

        public override long Solution2()
        {
            Rules rules = new Rules(dataList[0], true);

            int validCount = 0;
            List<string> messages = dataList[1].Split("\n", StringSplitOptions.RemoveEmptyEntries).ToList();
            foreach (string message in messages)
            {
                if (rules.TestValidMessage(message))
                    validCount++;
            }
            return validCount;
        }

        internal class Rules
        {
            HashSet<Rule> AllRules = new HashSet<Rule>(new RuleEqualityComparer());
            Rule RuleZero;
            public Rules(string listOfRules, bool sol2 = false)
            {
                List<string> primitiveList = listOfRules.Split("\n").ToList();
                // Creates the rules and adds them to the hashset
                foreach (string ruleString in primitiveList)
                {
                    AllRules.Add(new Rule(ruleString, sol2));
                }
                // adds the created rules to eachothers references
                foreach (Rule rule in AllRules.Where(a => !a.isBaseRule))
                {
                    rule.AddRulesToRules(AllRules);
                    if (rule.RuleId == 0)
                        RuleZero = rule;
                }
            }

            public bool TestValidMessage(string message)
            {
                int toIndex = 0;
                bool validToIndex = RuleZero.ValidateMessage(message, ref toIndex);
                return validToIndex && toIndex == message.Length;
            }
        }

        internal class Rule : IEquatable<Rule>, IEquatable<int>
        {
            public int RuleId { get; init; }
            public string stringRule { get; }
            public bool isBaseRule { get; init; }

            public Rule[][] RuleArray { get; private set; }

            private bool sol2 {get; init; }
 

            private Rule(int id)
            {
                RuleId = id;
            }

            public Rule(string ruleString, bool sol2 = false)
            {
                this.sol2 = sol2;
                string[] splitRuleString = ruleString.Split(": ");
                stringRule = splitRuleString[1];

                RuleId = Int32.Parse(splitRuleString[0]);
                if (RuleId == 8 && sol2)
                {
                    stringRule = "42 | 42 8";
                }
                else if (RuleId == 11 && sol2)
                {
                    stringRule = "42 31 | 42 11 31";
                }

                if (stringRule == "\"a\"" || stringRule == "\"b\"")
                {
                    isBaseRule = true;
                    stringRule = stringRule.Trim('"');
                }
                    
                else
                    isBaseRule = false;
            }

            public void AddRulesToRules(HashSet<Rule> allRules)
            {
                int[][] splitRule = stringRule.Split("| ")                            // splits the rules at the pip
                    .Select(a => a.Split(" ", StringSplitOptions.RemoveEmptyEntries)) // splits each subrule at spaces
                    .Select(b => b.Select(c => Int32.Parse(c)).ToArray()).ToArray();  // converts each of those splits to an int
                Rule foundRule;
                RuleArray = new Rule[splitRule.Length][];
                Rule[] newArray;


                for (int i = 0; i < splitRule.Length; i++)
                {
                    newArray = new Rule[splitRule[i].Length];
                    for (int j = 0; j < splitRule[i].Length; j++)
                    {
                        allRules.TryGetValue((Rule)splitRule[i][j], out foundRule);
                        newArray[j] = foundRule;
                    }
                    RuleArray[i] = newArray;
                }
            }

            //private bool TestRules(string message)
            //{
            //    int currIndex = 0;
            //    bool validMessage = false;
            //    for (int i = 0; i < RuleArray.Length; i++)
            //    {
            //        for (int j = 0; j < RuleArray[i].Length; j++)
            //        {
            //            if (RuleArray[i][j].ValidateMessage(messageArray[i]))
            //                validMessage = true;
            //            else
            //            {
            //                validMessage = false;
            //                break;
            //            }
            //        }
            //        if (validMessage)
            //            break;
            //    }
            //    return validMessage;
            //}

            public bool ValidateMessage(string message, ref int toIndex)
            {
               
                if (isBaseRule && stringRule == message[0].ToString())
                {
                    toIndex++;
                    return true;
                }   
                else if (isBaseRule && stringRule != message[0].ToString())
                    return false;

                int currIndex;
                int tempIndex;
                bool validMessage = false;
                for (int i = 0; i < RuleArray.Length; i++)
                {
                    currIndex = 0;
                    tempIndex = currIndex;
                    for (int j = 0; j < RuleArray[i].Length; j++)
                    {
                        if (RuleArray[i][j].ValidateMessage(message.Substring(tempIndex), ref tempIndex))
                            validMessage = true;
                        else
                        {
                            validMessage = false;
                            break;
                        }
                    }
                    if (validMessage)
                    {
                        toIndex += tempIndex;
                        break;
                    }
                        
                }
                return validMessage;
            }


            public bool Equals(Rule other)
            {
                return RuleId.Equals(other.RuleId);
            }

            public override bool Equals(object obj)
            {
                if (obj == null) return false;
                Rule objAsPart = obj as Rule;
                if (objAsPart == null) return false;
                else return Equals(objAsPart);
            }
            public override int GetHashCode()
            {
                return RuleId;
            }

            public bool Equals(int other)
            {
                return RuleId == other;
            }

            public static explicit operator Rule(int x)
            {
                return new Rule(x);
            }
        }

        internal class RuleEqualityComparer : IEqualityComparer<Rule>, IEqualityComparer<int>
        {
            public bool Equals(Rule x, Rule y)
            {
                return Equals(x.RuleId, y.RuleId);
            }

            public bool Equals(int x, int y)
            {
                return x == y;
            }

            public bool Equals(Rule x, int y)
            {
                return x.RuleId == y;
            }

            public bool Equals(int x, Rule y)
            {
                return x == y.RuleId;
            }

            public int GetHashCode([DisallowNull] Rule obj)
            {
                return obj.GetHashCode();
            }

            public int GetHashCode([DisallowNull] int obj)
            {
                return obj.GetHashCode();
            }
        }
    }
}
