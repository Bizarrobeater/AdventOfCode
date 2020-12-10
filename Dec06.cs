using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    class Dec06 : AdventCodeBase<string, int>
    {
        List<GroupsAnswer> GroupsAnswers = new List<GroupsAnswer>();

        public Dec06() : base(ReadDataFile.FileToListDoubleNewline)
        {
            foreach (string groupAnswer in dataList)
            {
                GroupsAnswers.Add(new GroupsAnswer(groupAnswer));
            }
        }

        //
        // Count the number of distinc answer per group
        // Correct answer: 6585
        public override int Solution1()
        {
            int answerSum = 0;
            foreach (GroupsAnswer group in GroupsAnswers)
            {
                answerSum += group.DistinctAnswers.Count();
            }
            return answerSum;
        }

        //
        // Count the number of common answers for the entire group
        // Correct answer: 3276
        public override int Solution2()
        {
            int answerSum = 0;
            foreach (GroupsAnswer group in GroupsAnswers)
            {
                answerSum += group.commonAnswers.Length;
            }
            return answerSum;
        }

        // Class for each groups answers
        private class GroupsAnswer
        {
            string allAnswers;

            public List<char> DistinctAnswers { get; set; }
            List<string> personAnswers = new List<string>();
            public string commonAnswers { get => GetCommonAnswer(); }


            public GroupsAnswer(string rawGroupAnswers)
            {
                // each persons answers get split at newline
                personAnswers = rawGroupAnswers.Split(Environment.NewLine).ToList();
                // all persons answers in the same list
                allAnswers = rawGroupAnswers.Replace(Environment.NewLine, "");
                // only distinct answers from the group
                DistinctAnswers = allAnswers.Distinct<char>().ToList();
            }

            // Returns a string of questions that all members of the group answers
            private string GetCommonAnswer()
            {
                char[] commonAnswers = personAnswers[0].ToArray();
                if (personAnswers.Count > 1)
                {
                    for (int i = 1; i < personAnswers.Count; i++)
                    {
                        char[] tempArray = personAnswers[i].ToArray();
                        commonAnswers = commonAnswers.Intersect(tempArray).ToArray();
                    }
                }
                return new string(commonAnswers);
            }
        }
    }
}
