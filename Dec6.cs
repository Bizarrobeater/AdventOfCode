using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AdventOfCode
{
    class Dec6 : ISolution
    {
        List<string> dataList;
        List<GroupsAnswer> GroupsAnswers = new List<GroupsAnswer>();

        public Dec6()
        {
            dataList = ReadDataFile.FileToListDoubleNewline("AdventCode6Dec.txt");
            foreach (string groupAnswer in dataList)
            {
                GroupsAnswers.Add(new GroupsAnswer(groupAnswer));
            }
        }

        public void test()
        {
            int answerSum = 0;
            foreach (GroupsAnswer groups in GroupsAnswers)
            {
                Console.WriteLine($"{groups.commonAnswers} - {groups.commonAnswers.Length}");
                answerSum += groups.commonAnswers.Length;
                Console.WriteLine("");
            }
            Console.WriteLine(answerSum);
            


        }

        public int SolutionPart1()
        {
            int answerSum = 0;
            foreach (GroupsAnswer group in GroupsAnswers)
            {
                answerSum += group.DistinctAnswers.Count();
            }
            return answerSum;
        }

        public int SolutionPart2()
        {
            //int answerSum = 0;
            //foreach (GroupsAnswer group in GroupsAnswers)
            //{
            //    answerSum += group.commonAnswers;
            //}
            //return answerSum;
            return -1;
        }


        private class GroupsAnswer
        {
            string allAnswers;
            public IEnumerable<char> DistinctAnswers { get; set; }
            List<string> personAnswers = new List<string>();
            public string commonAnswers { get => GetCommonAnswer(); }


            public GroupsAnswer(string rawGroupAnswers)
            {
                personAnswers = rawGroupAnswers.Split("\n").ToList();
                allAnswers = rawGroupAnswers.Replace("\n", "");
                DistinctAnswers = allAnswers.Distinct<char>();
            }

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

            public void Print()
            {
                foreach (char letter in DistinctAnswers)
                {
                    Console.Write(letter);
                }
                Console.Write("\n");
            }

        }
    }
}
