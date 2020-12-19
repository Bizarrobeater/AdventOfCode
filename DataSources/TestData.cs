using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode
{
    public static class TestData
    {
        public static List<string> Dec7Part1 = new List<string>
        {
                "light red bags contain 1 bright white bag, 2 muted yellow bags.",
                "dark orange bags contain 3 bright white bags, 4 muted yellow bags.",
                "bright white bags contain 1 shiny gold bag.",
                "muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.",
                "shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.",
                "dark olive bags contain 3 faded blue bags, 4 dotted black bags.",
                "vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.",
                "faded blue bags contain no other bags.",
                "dotted black bags contain no other bags.",
        };

        public static List<string> Dec7Part2 = new List<string>
        {
                "shiny gold bags contain 2 dark red bags.",
                "dark red bags contain 2 dark orange bags.",
                "dark orange bags contain 2 dark yellow bags.",
                "dark yellow bags contain 2 dark green bags.",
                "dark green bags contain 2 dark blue bags.",
                "dark blue bags contain 2 dark violet bags.",
                "dark violet bags contain no other bags.",
        };

        public static List<string> Dec8 = new List<string>
        {
            "nop +0",
            "acc +1",
            "jmp +4",
            "acc +3",
            "jmp -3",
            "acc -99",
            "acc +1",
            "jmp -4",
            "acc +6",
        };

        public static List<long> Dec9 = new List<long>
        {
            35,
            20,
            15,
            25,
            47,
            40,
            62,
            55,
            65,
            95,
            102,
            117,
            150,
            182,
            127,
            219,
            299,
            277,
            309,
            576,
        };


        // Solution for Part1 should be 220
        // Solution for Part2 should be 19208
        public static List<int> Dec10 = new List<int>
        {
            28,
            33,
            18,
            42,
            31,
            14,
            46,
            20,
            48,
            47,
            24,
            23,
            49,
            45,
            19,
            38,
            39,
            11,
            1,
            32,
            25,
            35,
            8,
            17,
            7,
            9,
            4,
            2,
            34,
            10,
            3,
        };

        public static List<string> Dec11Part1 = new List<string>
        {
            "L.LL.LL.LL",
            "LLLLLLL.LL",
            "L.L.L..L..",
            "LLLL.LL.LL",
            "L.LL.LL.LL",
            "L.LLLLL.LL",
            "..L.L.....",
            "LLLLLLLLLL",
            "L.LLLLLL.L",
            "L.LLLLL.LL",
        };

        public static List<string> Dec12Part1 = new List<string>
        {
            "F10",
            "N3",
            "F7",
            "R90",
            "F11",
        };

        // Solution for part 1: 295
        public static List<string> Dec13Part1 = new List<string>
        {
            "939",
            "7,13,x,x,59,x,31,19",
        };

        // Dictionary of example list and their correct data
        public static Dictionary<string, long> Dec13Part2 = new Dictionary<string, long>
        {
            {"3,x,7", 12 },
            {"7,x,3", 7 },
            {"6,x,x,x,17", 30 },
            {"7,x,x,4,x,11", 105 },
            { "7,13,x,x,59,x,31,19", 1_068_781 },
            { "17,x,13,19", 3_417},
            { "67,7,59,61", 754_018},
            { "67,x,7,59,61", 779_210},
            { "67,7,x,59,61", 1_261_476},
            { "1789,37,47,1889", 1_202_161_486},
        };

        // Solution: 165
        public static List<string> Dec14Part1 = new List<string>
        {
            "mask = XXXXXXXXXXXXXXXXXXXXXXXXXXXXX1XXXX0X",
            "mem[8] = 11",
            "mem[7] = 101",
            "mem[8] = 0",
        };

        // Solution: 208
        public static List<string> Dec14Part2 = new List<string>
        {
            "mask = 000000000000000000000000000000X1001X",
            "mem[42] = 100",
            "mask = 00000000000000000000000000000000X0XX",
            "mem[26] = 1",
        };

        // Testcases for solution1 (get the 2020th number)
        public static Dictionary<string, int> Dec15Part1 = new Dictionary<string, int>
        {
            {"0,3,6", 436 },
            {"1,3,2", 1 },
            {"2,1,3", 10 },
            {"1,2,3", 27 },
            {"2,3,1", 78 },
            {"3,2,1", 438 },
            {"3,1,2", 1836 },
        };


        // same as before but get the 30.000.000th number
        public static Dictionary<string, int> Dec15Part2 = new Dictionary<string, int>
        {
            {"1,3,2", 2_578 },
            {"2,1,3", 3_544_142 },
            {"1,2,3", 261_214 },
            {"2,3,1", 6_895_259 },
            {"3,2,1", 18 },
            {"3,1,2", 362 },
            {"0,3,6", 175_594 },
        };

        // Sum of numbers in nearby tickets not appearing in the first ranges
        // correct answer: 71
        public static List<string> Dec16Part1 = new List<string>
        {
            "class: 1-3 or 5-7\n" +
            "row: 6-11 or 33-44\n" +
            "seat: 13-40 or 45-50",

            "your ticket:\n" +
            "7,1,14",

            "nearby tickets:\n" +
            "7,3,47\n" +
            "40,4,50\n" +
            "55,2,20\n" +
            "38,6,12",
        };

        public static List<string> Dec16Part2 = new List<string>
        {
            "class: 0-1 or 4-19\n" +
            "row: 0-5 or 8-19\n" +
            "seat: 0-13 or 16-19",

            "your ticket:\n" +
            "11,12,13",

            "nearby tickets:\n" +
            "3,9,18\n" +
            "15,1,5\n" +
            "5,14,9",
        };

        public static List<string> Dec17Part1 = new List<string>()
        {
            ".#.",
            "..#",
            "###",
        };

        public static Dictionary<string, long> Dec18Part1 = new Dictionary<string, long>
        {
            { "1 + (2 * 3) + (4 * (5 + 6))", 51 },
            { "2 * 3 + (4 * 5)", 26 },
            { "5 + (8 * 3 + 9 + 3 * 4 * 3)", 437 },
            { "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 12_240 },
            { "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 13_632 },
        };

        public static Dictionary<string, long> Dec18Part2 = new Dictionary<string, long>
        {
            { "1 + (2 * 3) + (4 * (5 + 6))", 51 },
            { "2 * 3 + (4 * 5)", 46 },
            { "5 + (8 * 3 + 9 + 3 * 4 * 3)", 1_445 },
            { "5 * 9 * (7 * 3 * 3 + 9 * 3 + (8 + 6 * 4))", 669_060 },
            { "((2 + 4 * 9) * (6 + 9 * 8 + 6) + 6) + 2 + 4 * 2", 23_340 },
        };

        // Correct answers is 2
        public static List<string> Dec19Part1 = new List<string>
        {
            "0: 4 1 5\n" +
            "1: 2 3 | 3 2\n" +
            "2: 4 4 | 5 5\n" +
            "3: 4 5 | 5 4\n" +
            "4: \"a\"\n" +
            "5: \"b\"",

            "ababbb\n" +
            "bababa\n" +
            "abbbab\n" +
            "aaabbb\n" +
            "aaaabbb",
        };

        public static List<string> Dec19Part2 = new List<string>
        {





        };
    }
}
